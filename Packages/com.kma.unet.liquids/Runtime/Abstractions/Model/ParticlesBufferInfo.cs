using UNet.Calculations.Abstractions;
using float2 = Unity.Mathematics.float2;

namespace UNet.Liquids.Abstractions
{
    public class ParticlesBufferInfo
    {
        public int AllCount { get; set; }
        public int CurCount { get; set; }
        public ICalculationData<float2> Positions { get; set; }
        public ICalculationData<float2> Velocities { get; set; }
    }
}