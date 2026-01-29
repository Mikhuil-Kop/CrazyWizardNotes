using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class BookWrapper : TypedWrapperBase
    {
        public override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Reading.Realization.ResourcesReadSource),
            };
        }
    }
}
