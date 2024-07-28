using System;

namespace UNet.Calculations.Abstractions
{
    public interface ICalculationRenderUnit : IDisposable
    {
        ICalculationRenderUnit Set(string key, bool data);
        ICalculationRenderUnit Set(string key, float data);
        ICalculationRenderUnit Set(string key, Vector2D vector);
        ICalculationRenderUnit Set(string key, ICalculationData data);

        UnityEngine.Material GetMaterial();
    }
}