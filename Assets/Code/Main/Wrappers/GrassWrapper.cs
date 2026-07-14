using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class GrassWrapper : TypedWrapperBase
    {
        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Creatures.Realization.SimpleClaimable),
            };
        }
    }
}
