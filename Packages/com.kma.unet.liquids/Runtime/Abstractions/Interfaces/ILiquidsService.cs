using System.Collections.Generic;

namespace UNet.Liquids.Abstractions
{
    public interface ILiquidsService
    {
        ParticlesInfo GetParticlesInfo();
        ParticlesBufferInfo GetParticlesBufferInfo();
        void SpawnParticles(IEnumerable<ParticleInfo> particles);
    }
}