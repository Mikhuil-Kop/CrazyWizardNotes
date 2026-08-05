using System;
using System.Collections.Generic;
using Particles = CrazyWizardNotes.Magic.Realization.Particles;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class ParticleWrapper : TypedWrapperBase
    {
        public enum ParticleType : byte
        {
            Fireball
        }

        public static Dictionary<ParticleType, Type> ParticleTypes = new()
        {
            { ParticleType.Fireball, typeof(Particles.FireballBehavior) }
        };

        public ParticleType SelectedParticleType;

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new Type[]
            {
                typeof(UNet.Core.Realization.Destroyable),
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Magic.Realization.CustomSpellParticle),
                ParticleTypes[SelectedParticleType]
            };
        }
    }
}
