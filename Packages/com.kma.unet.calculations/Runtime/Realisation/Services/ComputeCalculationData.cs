using UNet.Calculations.Abstractions;
using UnityEngine;

namespace UNet.Calculations.Realisation
{
    public abstract class ComputeCalculationData : ICalculationData
    {
        public ComputeBuffer ComputeBuffer { get; private set; }
        public int Count => ComputeBuffer.count;

        public ComputeCalculationData(int count)
        {
            ComputeBuffer = CreateStructuredBuffer(count);
        }

        protected abstract ComputeBuffer CreateStructuredBuffer(int count);

        public abstract void Dispose();
    }

    public class ComputeCalculationData<T> : ComputeCalculationData, ICalculationData<T>
    {
        public ComputeCalculationData(int count) : base(count)
        {
        }

        public ComputeCalculationData(T[] values) : base(values.Length)
        {
            ComputeBuffer.SetData(values);
        }

        public T[] GetData()
        {
            var result = new T[ComputeBuffer.count];
            ComputeBuffer.GetData(result);

            return result;
        }

        public void SetData(T[] data)
        {
            ComputeBuffer.SetData(data);
        }

        public override void Dispose()
        {
            ComputeBuffer.Release();
            ComputeBuffer.Dispose();
        }

        protected override ComputeBuffer CreateStructuredBuffer(int count)
        {
            return new ComputeBuffer(count, GetStride());
        }

        protected int GetStride()
        {
            return System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
        }
    }
}