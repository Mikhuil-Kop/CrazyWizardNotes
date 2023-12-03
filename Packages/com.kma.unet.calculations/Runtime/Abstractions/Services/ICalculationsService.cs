using System;

namespace UNet.Calculations.Abstractions
{
    public interface ICalculationsService : IDisposable
    {
        ICalculationUnit CreateCalculationUnit(string name);
        ICalculationRenderUnit CreateCalculationRenderUnit(string name);
        ICalculationData<T> CreateCalculationData<T>(int count);
        ICalculationData<T> CreateCalculationData<T>(T[] values);
    }
}