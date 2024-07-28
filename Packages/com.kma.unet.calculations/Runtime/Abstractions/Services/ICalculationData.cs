using System;

namespace UNet.Calculations.Abstractions
{
    public interface ICalculationData : IDisposable
    {
        int Count { get; }
    }

    public interface ICalculationData<T> : ICalculationData
    {
        T[] GetData();
        void SetData(T[] data);
    }
}