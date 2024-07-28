using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    public class DebugGizmosService : IGizmosService
    {
        const float PointSize = 0.5f;

        public void DrawLine(Vector2D start, Vector2D end)
        {
            Debug.DrawLine(start, end);
        }

        public void DrawPoint(Vector2D point)
        {
            Debug.DrawLine(point + new Vector2D(0, PointSize), point - new Vector2D(0, PointSize));
            Debug.DrawLine(point + new Vector2D(PointSize, 0), point - new Vector2D(PointSize, 0));
        }
    }
}