using CrazyWizardNotes.Elements.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public sealed class MagnetizeBehavior : CustomParticleBehaviourBase, IMagnetic
    {
        private Animator Animator { get; }
        private float ChargeValue { get; }
        private bool IsActive { get; set; }
        private bool IsChargePositive { get; set; }

        public float MagneticCharge
        {
            get
            {
                return IsActive
            ? IsChargePositive
                ? ChargeValue
                : -ChargeValue
            : 0;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public float MagneticSusceptibility => 0f;
        public bool IsAffectedByMagnetic => false;

        public MagnetizeBehavior(Animator animator)
        {
            Animator = animator;
            ChargeValue = 100f;
            IsActive = false;
            IsChargePositive = true;
        }

        public override void OnBendStart(ICustomParticleBehaviorContext context)
        {
            IsActive = true;
        }

        public override void OnUpdate(ICustomParticleBehaviorContext context)
        {
            if (context.State.CurFreeTime > 3f)
            {
                context.IsVanishRequested = true;
            }
        }

        public override void OnBendEnd(ICustomParticleBehaviorContext context)
        {
            IsActive = false;

            IsChargePositive = !IsChargePositive;
        }
    }
}