using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class WizardWrapper : TypedWrapperBase
    {
        public override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(UNet.Sound.Realization.SoundReceiver),
                typeof(UNet.Sound.Realization.SoundSource),

                typeof(Reading.Realization.ReadHandler),
                typeof(Avatar.Realization.WizardAvatar),
            };
        }
    }
}
