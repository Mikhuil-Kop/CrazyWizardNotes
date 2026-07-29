using CrazyWizardNotes.Magic.Realization;
using System;
using System.Collections.Generic;
using Particles = CrazyWizardNotes.Magic.Realization.Particles;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class ParticleWrapper : TypedWrapperBase
    {
        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new Type[]
            {
                typeof(UNet.Core.Realization.Destroyable),
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(UNet.Sound.Realization.SoundReceiver),
                typeof(UNet.Sound.Realization.SoundSource),

                typeof(CustomSpellParticle),
            };
        }

        protected override IDictionary<string, Type> GetKeyedWrappedTypes()
        {
            return new Dictionary<string, Type>()
            {
                { "Fireball", typeof(Particles.FireballBehaviour) },
                { "GravityPush", typeof(Particles.GravityPushBehaviour) },
                { "FireStorm", typeof(Particles.FireStormBehaviour) },
                { "Crash", typeof(Particles.CrashBehaviour) }
            };
        }
    }
}
