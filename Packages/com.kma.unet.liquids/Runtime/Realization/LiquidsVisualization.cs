using System;
using UNet.Calculations.Abstractions;
using UNet.Core.Abstractions;
using UNet.Liquids.Abstractions;
using UnityEngine;
using float2 = Unity.Mathematics.float2;
using ILogger = UNet.Core.Abstractions.ILogger;

namespace UNet.Liquids.Realization
{
    public class LiquidsVisualization : ILiquidsVisualization, IHandlesStart, IHandlesLateUpdate, IDisposable
    {
        #region Сервисы

        private ICalculationsService CalculationsService { get; }
        private ILiquidsService LiquidsService { get; }
        private ILogger Logger { get; }

        #endregion

        #region Вычисления

        private ICalculationRenderUnit LiquidCalculationUnit { get; set; }
        private ICalculationData<float2> PositionsData { get; set; }
        //private ICalculationData<float> RadiusesData { get; set; }

        #endregion

        #region Отображение

        public Mesh Mesh { get; set; }
        public Material Material { get; set; }
        public ComputeBuffer ArgsBuffer { get; set; }
        public Bounds? Bounds { get; set; }

        #endregion

        public LiquidsVisualization(ICalculationsService calculationsService, ILiquidsService liquidsService, ILogger logger, Mesh mesh)
        {
            CalculationsService = calculationsService;
            LiquidsService = liquidsService;
            Logger = logger;
            Mesh = mesh;
        }

        #region Функционал

        public void OnStart()
        {
            var bufferInfo = LiquidsService.GetParticlesBufferInfo();

            LiquidCalculationUnit = CalculationsService.CreateCalculationRenderUnit("Shaders/LiquidVisualisation");
            PositionsData = bufferInfo.Positions;
            //RadiusesData = bufferInfo.Radiuses;

            LiquidCalculationUnit.Set("Positions2D", PositionsData);
            //LiquidCalculationUnit.Set("Radiuses", RadiusesData);

            Material = LiquidCalculationUnit.GetMaterial();
            ArgsBuffer = CreateArgsBuffer(Mesh, bufferInfo.AllCount);
            Bounds = new Bounds(Vector3.zero, Vector3.one * 10000);
            Material.SetFloat("scale", 0.1f);
        }

        public void OnLateUpdate(float deltaTime)
        {
            //Logger.LogInfo("DrawMesh");
            DrawMesh();
        }

        public void Dispose()
        {
            ArgsBuffer.Release();
            ArgsBuffer.Dispose();
        }

        private void DrawMesh()
        {
            Graphics.DrawMeshInstancedIndirect(Mesh, 0, Material, Bounds.Value, ArgsBuffer);
        }

        private ComputeBuffer CreateArgsBuffer(Mesh mesh, int particlesCount)
        {
            var args = new uint[]
            {
                mesh.GetIndexCount(0),
                (uint)particlesCount,
                mesh.GetIndexStart(0),
                mesh.GetBaseVertex(0),
                0
            };

            var argsBuffer = new ComputeBuffer(1, 5 * sizeof(uint), ComputeBufferType.IndirectArguments);
            argsBuffer.SetData(args);
            return argsBuffer;
        }

        #endregion
    }
}