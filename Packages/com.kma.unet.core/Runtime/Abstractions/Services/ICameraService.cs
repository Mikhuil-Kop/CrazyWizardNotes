using System;

namespace UNet.Core.Abstractions
{
    public interface ICameraService
    {
        Vector2D CameraPosition { get; }
        Vector2D Pointer { get; }
        Vector2D ScreenPointer { get; }
        IActor[] PointedTargets { get; }
        [Obsolete("Еще не реализовано")]
        IActor[] ScreenPointedTargets { get; }

        Vector2D WorldToScreen(Vector2D origin);
        Vector2D ScreenToWorld(Vector2D origin);
    }
}