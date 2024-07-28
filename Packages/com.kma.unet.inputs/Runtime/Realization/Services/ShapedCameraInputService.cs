using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNet.Core.Abstractions;
using UNet.Inputs.Abstractions;
using UnityEngine;

namespace UNet.Inputs.Realization
{
    public class ShapedCameraInputService : CameraInputService, IShapedPointerInputService, IHandlesUpdate
    {
        private IAsyncJobService AsyncJobService { get; }

        private DollarRecognizer PointersRecognizer { get; }

        private LinkedList<Vector2D> PointersHistoryList { get; }

        private LinkedList<Vector2D> ScreenPointersHistoryList { get; }
        
        public PointerShape PointerShape { get; set; }

        public PointerShape ScreenPointerShape { get; set; }

        public ShapedCameraInputService(ICameraService cameraService, IAsyncJobService asyncJobService) : base(cameraService)
        {
            AsyncJobService = asyncJobService;
            PointersRecognizer = new DollarRecognizer();
            PointersHistoryList = new LinkedList<Vector2D>();
            ScreenPointersHistoryList = new LinkedList<Vector2D>();
            PointerShape = PointerShape.Default;
            ScreenPointerShape = PointerShape.Default;

            PointersRecognizer.SavePattern(((byte)PointerShapeType.Circle).ToString(), CreateCircleTemplate());
            PointersRecognizer.SavePattern(((byte)PointerShapeType.Rectangle).ToString(), CreateRectangleTemplate());
            PointersRecognizer.SavePattern(((byte)PointerShapeType.Triangle).ToString(), CreateTriangleTemplate());
            PointersRecognizer.SavePattern(((byte)PointerShapeType.Snake).ToString(), CreateSTemplate());
            PointersRecognizer.SavePattern(((byte)PointerShapeType.Swirl).ToString(), CreateSwirlTemplate());

            AsyncJobService.Register(() => Task.Run(RecalculateHistories), 0.1f, null, AsyncJobWaitType.CancelBeforeNext);
        }

        public void OnUpdate(float deltaTime)
        {
            lock (PointersHistoryList)
            {
                PointersHistoryList.AddLast(Pointer);
            }

            lock (ScreenPointersHistoryList)
            {
                ScreenPointersHistoryList.AddLast(ScreenPointer);
            }
        }

        private void RecalculateHistories()
        {
            PointerShape = CalculateShapeInfo(PointersHistoryList, PointersRecognizer);
            ScreenPointerShape = CalculateShapeInfo(ScreenPointersHistoryList, PointersRecognizer);
        }

        private static PointerShape CalculateShapeInfo(LinkedList<Vector2D> pointsHistory, DollarRecognizer recognizer)
        {
            var points = new Vector2D[0];

            lock (pointsHistory)
            {
                points = pointsHistory.ToArray();
            }

            var recognized = recognizer.Recognize(points.Select(x => (Vector2)x));

            if (!byte.TryParse(recognized.Match.Name, out var shapeId))
            {
                return PointerShape.Default;
            }

            var minX = recognized.Match.Points.Min(v => v.x);
            var maxX = recognized.Match.Points.Max(v => v.x);
            var minY = recognized.Match.Points.Min(v => v.y);
            var maxY = recognized.Match.Points.Max(v => v.y);

            return new PointerShape
            {
                Points = points,
                ShapeType = (PointerShapeType)shapeId,
                Center = new Vector2D()
                {
                    X = (minX + maxX) / 2,
                    Y = (minY + maxY) / 2,
                },
                Size = new Vector2D()
                {
                    X = maxX - minX,
                    Y = maxY - minY
                },
                Rotation = recognized.Angle,
                Accuracy = recognized.Score,
            };
        }

        #region Распознавание и паттерны

        private List<Vector2> CreateCircleTemplate()
        {
            // Идеальный круг, 32 точки (чем больше — тем точнее)
            var points = new List<Vector2>();
            var steps = 32;
            var radius = 100f;
            var center = new Vector2(100, 100); // не важно где центр — нормализуется

            for (int i = 0; i < steps; i++)
            {
                var angle = i * Mathf.PI * 2f / steps;
                var x = center.x + radius * Mathf.Cos(angle);
                var y = center.y + radius * Mathf.Sin(angle);

                points.Add(new Vector2(x, y));
            }

            return points;
        }

        private List<Vector2> CreateRectangleTemplate()
        {
            // Квадрат против часовой стрелки, начиная с левого верхнего угла
            var size = 150f;
            var half = size / 2f;
            var center = new Vector2(100, 100);

            var points = new List<Vector2>
            {
                new(center.x - half, center.y - half), // левый верх
                new(center.x + half, center.y - half), // правый верх
                new(center.x + half, center.y + half), // правый низ
                new(center.x - half, center.y + half), // левый низ
                new(center.x - half, center.y - half)  // обратно в начало (важно!)
            };

            // Добавляем промежуточные точки по сторонам (для лучшего распознавания)
            return SubdividePoints(points, 8); // по 8 точек на сторону
        }

        private List<Vector2> CreateTriangleTemplate()
        {
            // Треугольник: вершина сверху, основание снизу
            // Размер не важен — будет нормализовано
            var size = 160f;
            var half = size / 2f;
            var center = new Vector2(100, 100);

            var points = new List<Vector2>
            {
                new (center.x, center.y - half),           // верхняя вершина (угол вверх)
                new (center.x + half, center.y + half),    // правый нижний
                new (center.x - half, center.y + half),    // левый нижний
                new (center.x, center.y - half)            // обратно в начало — обязательно!
            };

            // Делим каждую сторону на 10–12 точек — чем больше, тем точнее распознавание
            return SubdividePoints(points, 12);
        }

        private List<Vector2> CreateSTemplate()
        {
            var w = 120f;
            var h = 180f;
            var c = new Vector2(100, 100);

            var points = new List<Vector2>
            {
                // Рисуем S сверху вниз, плавно, как буква S
                new (c.x + w * 0.5f, c.y - h * 0.5f),  // верхняя правая точка
                new (c.x + w * 0.3f, c.y - h * 0.3f),
                new (c.x + w * 0.1f, c.y - h * 0.1f),
                new (c.x - w * 0.1f, c.y + 0f),
                new (c.x - w * 0.3f, c.y + h * 0.1f),
                new (c.x - w * 0.5f, c.y + h * 0.3f),  // средняя левая
                new (c.x - w * 0.3f, c.y + h * 0.4f),
                new (c.x - w * 0.1f, c.y + h * 0.45f),
                new (c.x + w * 0.1f, c.y + h * 0.48f),
                new (c.x + w * 0.3f, c.y + h * 0.5f),
                new (c.x + w * 0.5f, c.y + h * 0.5f)  // нижняя правая
            };

            // Замыкаем (необязательно, но помогает)
            points.Add(points[0]);

            return SubdividePointsSmooth(points, 12); // плавное распределение
        }

        private List<Vector2> CreateSwirlTemplate()
        {
            var center = new Vector2(100, 100);
            var maxRadius = 100f;
            var turns = 2;           // 2 полных оборота
            var stepsPerTurn = 24;   // чем больше — тем плавнее
            var totalSteps = turns * stepsPerTurn;

            var points = new List<Vector2>();

            for (var i = 0; i <= totalSteps; i++)
            {
                float t = i / (float)totalSteps;
                float angle = t * turns * Mathf.PI * 2f;
                float radius = maxRadius * (1f - t); // сужается к центру

                float x = center.x + radius * Mathf.Cos(angle);
                float y = center.y + radius * Mathf.Sin(angle);

                points.Add(new Vector2(x, y));
            }

            // Финальная точка в центре
            points.Add(center);

            return points;
        }

        private List<Vector2> SubdividePoints(List<Vector2> corners, int pointsPerSide)
        {
            var result = new List<Vector2>();

            for (int i = 0; i < corners.Count; i++)
            {
                var start = corners[i];
                var end = corners[(i + 1) % corners.Count];

                for (int j = 0; j < pointsPerSide; j++)
                {
                    float t = j / (float)pointsPerSide;
                    result.Add(Vector2.Lerp(start, end, t));
                }
            }

            // Последняя точка дублирует первую — Dollar любит замкнутые жесты
            result.Add(result[0]);
            return result;
        }

        private List<Vector2> SubdividePointsSmooth(List<Vector2> source, int pointsPerSegment)
        {
            var result = new List<Vector2>();

            for (var i = 0; i < source.Count - 1; i++)
            {
                var a = source[i];
                var b = source[(i + 1) % source.Count];

                for (var j = 0; j < pointsPerSegment; j++)
                {
                    var t = j / (float)pointsPerSegment;
                    // Плавная интерполяция (можно заменить на Bezier если хочешь супер-S)
                    result.Add(Vector2.Lerp(a, b, t));
                }
            }

            result.Add(source[0]); // замыкаем
            return result;
        }

        #endregion
    }
}