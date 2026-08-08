using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public sealed class BlackHoleBehavior : CustomParticleBehaviourBase
    {
        private Animator Animator { get; }
        private float BendTime { get; set; }

        public BlackHoleBehavior(Animator animator)
        {
            Animator = animator;
            BendTime = 0f;
        }

        public override void OnBendUpdate(CustomParticleStateContext context)
        {
            BendTime += context.DeltaTime;
        }

        public override void OnEndByAny(CustomParticleStateContext context)
        {
            BendTime = 0f;
        }
    }
}
