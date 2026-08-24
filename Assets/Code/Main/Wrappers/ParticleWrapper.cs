using CrazyWizardNotes.Magic.Abstractions;
using System;
using System.Collections.Generic;
using Particles = CrazyWizardNotes.Magic.Realization.Particles;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class ParticleWrapper : TypedWrapperBase
    {
        public enum ParticleType : byte
        {
            Fireball,
            BlackHole,
            Magnetize,
            SpawnAnvil
        }

        public ParticleType SelectedParticleType;
        public SpellParamsScriptable SpellParams;

        public static Dictionary<ParticleType, Type> ParticleTypes = new()
        {
            { ParticleType.Fireball, typeof(Particles.FireballBehavior) },
            { ParticleType.BlackHole, typeof(Particles.BlackHoleBehavior) },
            { ParticleType.Magnetize, typeof(Particles.MagnetizeBehavior) },
            { ParticleType.SpawnAnvil, typeof(Particles.SpawnAnvilBehavior) }
        };

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
