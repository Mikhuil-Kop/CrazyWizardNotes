using UNet;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public class CustomParticleStateContext
    {
        public IActor Actor { get; set; }

        public float DeltaTime { get; set; }
        public float ParticleLifeTime { get; set; }
        public float BehaviourTime { get; set; }

        public Vector2D Velocity { get; set; }

        public float VelocityEnergy { get; set; }
        public float VelocityEnergyMult { get; set; }
        public float VelocityEnergyFade { get; set; }

        public float TorsionEnergy { get; set; }
        public float TorsionEnergyMult { get; set; }
        public float TorsionEnergyFade { get; set; }
    }
}
