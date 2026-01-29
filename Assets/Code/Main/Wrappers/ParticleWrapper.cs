using CrazyWizardNotes.Magic.Realization;
using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    internal class ParticleWrapper : TypedWrapperBase
    {
        public override IEnumerable<Type> GetWrappedTypes()
        {
            return new Type[]
            {
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(UNet.Sound.Realization.SoundReceiver),
                typeof(UNet.Sound.Realization.SoundSource),

                typeof(SpellParticle)
            };
        }
    }
}
