using CrazyWizardNotes.Magic.Realization;
using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class ParticleWrapper : TypedWrapperBase
    {
        protected override IEnumerable<Type> GetWrappedTypes()
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
