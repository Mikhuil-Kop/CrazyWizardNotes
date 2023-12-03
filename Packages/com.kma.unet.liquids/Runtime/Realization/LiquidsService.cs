using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Calculations.Abstractions;
using UNet.Core.Abstractions;
using UNet.Liquids.Abstractions;
using float2 = Unity.Mathematics.float2;
using uint3 = Unity.Mathematics.uint3;

namespace UNet.Liquids.Realization
{
    public class LiquidsService : ILiquidsService, IHandlesFixedUpdate, IDisposable
    {
        #region Сервисы

        private ICalculationsService CalculationsService { get; }
        private ILogger Logger { get; }

        #endregion

        #region Вычисления

        private ICalculationUnit LiquidCalculationUnit { get; set; }
        private ICalculationData<float2> PositionsData { get; set; }
        private ICalculationData<float2> PredictedPositionsData { get; set; }
        private ICalculationData<float2> VelocitiesData { get; set; }
        private ICalculationData<float2> DensitiesData { get; set; }
        private ICalculationData<uint3> SpatialIndicesDatas { get; set; }
        private ICalculationData<uint> SpatialOffsetsData { get; set; }

        private ICalculationUnitAction ExternalForcesKernel { get; set; }
        private ICalculationUnitAction SpatialHashKernel { get; set; }
        private ICalculationUnitAction DensityKernel { get; set; }
        private ICalculationUnitAction PressureKernel { get; set; }
        private ICalculationUnitAction ViscosityKernel { get; set; }
        private ICalculationUnitAction UpdatePositionKernel { get; set; }

        private int NumParticles { get; set; }

        private int MaxParticles { get; set; }

        #endregion

        #region Настройки

        public float gravity;
        public float collisionDamping;
        public float smoothingRadius;
        public float targetDensity;
        public float pressureMultiplier;
        public float nearPressureMultiplier;
        public float viscosityStrength;
        public Vector2D boundsSize;
        public Vector2D obstacleSize;
        public Vector2D obstacleCentre;

        #endregion

        #region Основной функционал

        public LiquidsService(ICalculationsService calculationsService, ILogger logger)
        {
            CalculationsService = calculationsService;
            Logger = logger;

            NumParticles = 0;
            MaxParticles = 20_000;

            // Params

            gravity = -13;
            collisionDamping = 0.5f;
            smoothingRadius = 0.2f;
            targetDensity = 234;
            pressureMultiplier = 0.255f;
            nearPressureMultiplier = 18;
            viscosityStrength = 0.03f;
            boundsSize = new Vector2D(20f, 10f);
            obstacleSize = new Vector2D();
            obstacleCentre = new Vector2D(-100f, -100f);

            // Calculations prepare

            LiquidCalculationUnit = CalculationsService.CreateCalculationUnit("Shaders/FluidSim2D");

            // Create buffers

            PositionsData = CalculationsService.CreateCalculationData<float2>(MaxParticles);
            PredictedPositionsData = CalculationsService.CreateCalculationData<float2>(MaxParticles);
            VelocitiesData = CalculationsService.CreateCalculationData<float2>(MaxParticles);
            DensitiesData = CalculationsService.CreateCalculationData<float2>(MaxParticles);
            SpatialIndicesDatas = CalculationsService.CreateCalculationData<uint3>(MaxParticles);
            SpatialOffsetsData = CalculationsService.CreateCalculationData<uint>(MaxParticles);

            PositionsData.SetData(Enumerable.Range(0, MaxParticles).Select(x => new float2(-1, -1)).ToArray());
            PredictedPositionsData.SetData(Enumerable.Range(0, MaxParticles).Select(x => new float2(-1, -1)).ToArray());
            VelocitiesData.SetData(Enumerable.Range(0, MaxParticles).Select(x => new float2(0, 0)).ToArray());
            DensitiesData.SetData(Enumerable.Range(0, MaxParticles).Select(x => new float2(0, 0)).ToArray());
            SpatialIndicesDatas.SetData(Enumerable.Range(0, MaxParticles).Select(x => new uint3()).ToArray());
            SpatialOffsetsData.SetData(Enumerable.Range(0, MaxParticles).Select(x => (uint)0).ToArray());

            // Init compute

            ExternalForcesKernel = LiquidCalculationUnit.Action("ExternalForces")
                .Set("Positions", PositionsData)
                .Set("PredictedPositions", PredictedPositionsData)
                .Set("Velocities", VelocitiesData);

            UpdatePositionKernel = LiquidCalculationUnit.Action("UpdatePositions")
                .Set("Positions", PositionsData)
                .Set("Velocities", VelocitiesData);

            SpatialHashKernel = LiquidCalculationUnit.Action("CalculateSpatialHash")
                .Set("PredictedPositions", PredictedPositionsData)
                .Set("SpatialIndices", SpatialIndicesDatas)
                .Set("SpatialOffsets", SpatialOffsetsData);

            DensityKernel = LiquidCalculationUnit.Action("CalculateDensities")
                .Set("PredictedPositions", PredictedPositionsData)
                .Set("SpatialIndices", SpatialIndicesDatas)
                .Set("SpatialOffsets", SpatialOffsetsData)
                .Set("Densities", DensitiesData);

            PressureKernel = LiquidCalculationUnit.Action("CalculatePressureForce")
                .Set("PredictedPositions", PredictedPositionsData)
                .Set("SpatialIndices", SpatialIndicesDatas)
                .Set("SpatialOffsets", SpatialOffsetsData)
                .Set("Densities", DensitiesData)
                .Set("Velocities", VelocitiesData);

            ViscosityKernel = LiquidCalculationUnit.Action("CalculateViscosity")
                .Set("PredictedPositions", PredictedPositionsData)
                .Set("SpatialIndices", SpatialIndicesDatas)
                .Set("SpatialOffsets", SpatialOffsetsData)
                .Set("Densities", DensitiesData)
                .Set("Velocities", VelocitiesData);
        }

        public ParticlesInfo GetParticlesInfo()
        {
            var positions = PositionsData.GetData();
            var velocities = VelocitiesData.GetData();

            return new ParticlesInfo()
            {
                Particles = positions.Select((x, i) => new ParticleInfo()
                {
                    Position = Convert(positions[i]),
                    Velocity = Convert(velocities[i])
                }).ToList()
            };
        }

        public ParticlesBufferInfo GetParticlesBufferInfo()
        {
            return new ParticlesBufferInfo()
            {
                Positions = PositionsData,
                Velocities = VelocitiesData,
                AllCount = PositionsData.Count,
                CurCount = NumParticles
            };
        }

        public void SpawnParticles(IEnumerable<ParticleInfo> particles)
        {
            if (NumParticles == MaxParticles)
            {
                Logger.LogError("Unable to add more particles. Max amount is filled.");
                return;
            }

            var parts = particles.ToArray();
            var partsCount = parts.Length;

            if (NumParticles + partsCount > MaxParticles)
            {
                partsCount = MaxParticles - NumParticles;
                Logger.LogError("Unable to add all particles. Max amount is filled.");
            }

            var newPositions = particles.Take(partsCount).Select(p => Convert(p.Position)).ToArray();
            var newVelocities = particles.Take(partsCount).Select(p => Convert(p.Velocity)).ToArray();

            AddData(PositionsData, NumParticles, newPositions);
            AddData(PredictedPositionsData, NumParticles, newPositions);
            AddData(VelocitiesData, NumParticles, newVelocities);

            NumParticles += partsCount;
            LiquidCalculationUnit.Set("numParticles", NumParticles);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            RunSimulationFrame(fixedDeltaTime);
        }

        public void Dispose()
        {
            LiquidCalculationUnit?.Dispose();

            PositionsData?.Dispose();
            VelocitiesData?.Dispose();
            DensitiesData?.Dispose();
            PredictedPositionsData?.Dispose();
            SpatialIndicesDatas?.Dispose();
            SpatialOffsetsData?.Dispose();
        }

        #endregion

        #region Симуляция

        private void UpdateSettings(float deltaTime)
        {
            LiquidCalculationUnit.Set("deltaTime", deltaTime);
            LiquidCalculationUnit.Set("numParticles", NumParticles);
            LiquidCalculationUnit.Set("gravity", gravity);
            LiquidCalculationUnit.Set("collisionDamping", collisionDamping);
            LiquidCalculationUnit.Set("smoothingRadius", smoothingRadius);
            LiquidCalculationUnit.Set("targetDensity", targetDensity);
            LiquidCalculationUnit.Set("pressureMultiplier", pressureMultiplier);
            LiquidCalculationUnit.Set("nearPressureMultiplier", nearPressureMultiplier);
            LiquidCalculationUnit.Set("viscosityStrength", viscosityStrength);
            LiquidCalculationUnit.Set("boundsSize", boundsSize);
            LiquidCalculationUnit.Set("obstacleSize", obstacleSize);
            LiquidCalculationUnit.Set("obstacleCentre", obstacleCentre);

            LiquidCalculationUnit.Set("Poly6ScalingFactor", 4 / (MathF.PI * MathF.Pow(smoothingRadius, 8)));
            LiquidCalculationUnit.Set("SpikyPow3ScalingFactor", 10 / (MathF.PI * MathF.Pow(smoothingRadius, 5)));
            LiquidCalculationUnit.Set("SpikyPow2ScalingFactor", 6 / (MathF.PI * MathF.Pow(smoothingRadius, 4)));
            LiquidCalculationUnit.Set("SpikyPow3DerivativeScalingFactor", 30 / (MathF.Pow(smoothingRadius, 5) * MathF.PI));
            LiquidCalculationUnit.Set("SpikyPow2DerivativeScalingFactor", 12 / (MathF.Pow(smoothingRadius, 4) * MathF.PI));

            LiquidCalculationUnit.Set("interactionInputPoint", new Vector2D());
            LiquidCalculationUnit.Set("interactionInputStrength", 0f);
            LiquidCalculationUnit.Set("interactionInputRadius", 0f);
        }

        private void RunSimulationFrame(float deltaTime)
        {
            UpdateSettings(deltaTime);
            RunSimulationStep();
        }

        private void RunSimulationStep()
        {
            if (NumParticles == 0)
            {
                return;
            }

            Logger.LogInfo("RunSimulationStep");

            ExternalForcesKernel.Execute(NumParticles);
            SpatialHashKernel.Execute(NumParticles);

            var sort = new LiquidSort(CalculationsService);
            sort.SetBuffers(SpatialIndicesDatas, SpatialOffsetsData);
            sort.SortAndCalculateOffsets();

            DensityKernel.Execute(NumParticles);
            PressureKernel.Execute(NumParticles);
            ViscosityKernel.Execute(NumParticles);
            UpdatePositionKernel.Execute(NumParticles);

            Logger.LogInfo("P: " + PositionsData.GetData()[1]);
            Logger.LogInfo("V: " + VelocitiesData.GetData()[1]);
            Logger.LogInfo("D: " + DensitiesData.GetData()[1]);
            Logger.LogInfo("S1: " + SpatialIndicesDatas.GetData()[1]);
            Logger.LogInfo("S2: " + SpatialOffsetsData.GetData()[1]);
        }

        #endregion

        #region Взаимодействие

        /*
        #region Каркас

        public Vector2D[] GetConvexHull(Vector2D[] positions)
        {
            if (positions.Length < 3)
            {
                return positions;
            }

            // Находим самую нижнюю точку
            var start = positions.OrderBy(p => p.Y).ThenBy(p => p.X).First();
            var hull = new List<Vector2D>();

            // Сортируем точки по полярному углу относительно start
            var sortedPoints = positions
                .Where(p => p != start)
                .OrderBy(p => Math.Atan2(p.Y - start.Y, p.X - start.X))
                .ToList();

            hull.Add(start);
            hull.Add(sortedPoints[0]);

            for (int i = 1; i < sortedPoints.Count; i++)
            {
                while (hull.Count > 1 && !IsCounterClockwise(hull[^2], hull[^1], sortedPoints[i]))
                {
                    hull.RemoveAt(hull.Count - 1);
                }
                hull.Add(sortedPoints[i]);
            }

            return hull.ToArray();
        }

        // Проверяем, является ли поворот против часовой стрелки
        private bool IsCounterClockwise(Vector2D a, Vector2D b, Vector2D c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X) > 0;
        }

        public Vector2D[] GetInnerSkeleton(Vector2D[] positions, int targetCount)
        {
            if (positions.Length <= targetCount) return positions;

            List<Vector2D> centroids = new List<Vector2D>();
            // Инициализация случайных центроидов
            for (int i = 0; i < targetCount; i++)
                centroids.Add(positions[Random.Range(0, positions.Length)]);

            for (int iter = 0; iter < 10; iter++) // Количество итераций
            {
                // Назначение точек ближайшим центроидам
                List<List<Vector2D>> clusters = Enumerable.Range(0, targetCount).Select(_ => new List<Vector2D>()).ToList();
                foreach (var pos in positions)
                {
                    int closest = 0;
                    float minDist = Vector2D.Distance(pos, centroids[0]);
                    for (int i = 1; i < targetCount; i++)
                    {
                        float dist = Vector2D.Distance(pos, centroids[i]);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            closest = i;
                        }
                    }
                    clusters[closest].Add(pos);
                }

                // Пересчет центроидов
                for (int i = 0; i < targetCount; i++)
                {
                    if (clusters[i].Count > 0)
                    {
                        Vector2D avg = Vector2D.zero;
                        foreach (var p in clusters[i]) avg += p;
                        centroids[i] = avg / clusters[i].Count;
                    }
                }
            }

            return centroids.ToArray();
        }

        public (Vector2D[] newPositions, Vector2D[] newSpeeds, Vector2D[] newMasses) ApproximateParticles(
    Vector2D[] positions, Vector2D[] speeds, Vector2D[] masses, int skeletonCount)
        {
            // Получаем внешнюю границу
            Vector2D[] hull = GetConvexHull(positions);

            // Получаем внутренний каркас
            Vector2D[] skeleton = GetInnerSkeleton(positions, skeletonCount);

            // Объединяем точки
            Vector2D[] newPositions = hull.Concat(skeleton).Distinct().ToArray();

            // Интерполируем скорости и массы для новых точек
            Vector2D[] newSpeeds = new Vector2D[newPositions.Length];
            Vector2D[] newMasses = new Vector2D[newPositions.Length];

            for (int i = 0; i < newPositions.Length; i++)
            {
                Vector2D pos = newPositions[i];
                float totalWeight = 0;
                Vector2D speedSum = Vector2D.zero;
                Vector2D massSum = Vector2D.zero;

                // Ищем ближайшие оригинальные точки для интерполяции
                for (int j = 0; j < positions.Length; j++)
                {
                    float dist = Vector2D.Distance(pos, positions[j]);
                    if (dist < 1e-6) // Если точка совпадает
                    {
                        newSpeeds[i] = speeds[j];
                        newMasses[i] = masses[j];
                        break;
                    }
                    float weight = 1 / (dist * dist); // Инверсный вес по расстоянию
                    speedSum += speeds[j] * weight;
                    massSum += masses[j] * weight;
                    totalWeight += weight;
                }

                newSpeeds[i] = speedSum / totalWeight;
                newMasses[i] = massSum / totalWeight;
            }

            return (newPositions, newSpeeds, newMasses);
        }

        #endregion

        */

        #endregion

        #region Вспомогательное

        private void AddData<T>(ICalculationData<T> dataBuffer, int startPosition, T[] newData)
        {
            var oldData = dataBuffer.GetData();

            Array.Copy(newData, 0, oldData, startPosition, newData.Length);

            dataBuffer.SetData(oldData);
        }

        private float2 Convert(Vector2D v)
        {
            return new float2(v.X, v.Y);
        }

        private Vector2D Convert(float2 f)
        {
            return new Vector2D(f.x, f.y);
        }

        #endregion
    }
}