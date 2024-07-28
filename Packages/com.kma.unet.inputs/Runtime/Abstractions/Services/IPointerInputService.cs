using System;
using UNet.Core.Abstractions;

namespace UNet.Inputs.Abstractions
{
    public interface IPointerInputService : IInputService
    {
        Vector2D CameraPosition { get; }
        Vector2D Pointer { get; }
        Vector2D ScreenPointer { get; }

        IActor[] PointedTargets { get; }
        [Obsolete("Еще не реализовано")]
        IActor[] ScreenPointedTargets { get; }
    }
}