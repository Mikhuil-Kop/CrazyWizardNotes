using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class AntimagicFieldWrapper : TypedWrapperBase
    {
        public Magic.Realization.AntimagicFieldConfig AntimagicFieldConfig;

        protected override IEnumerable<object> GetObjects()
        {
            return new[]
            {
                AntimagicFieldConfig
            };
        }

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Core.Realization.Destroyable),
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Magic.Realization.AntimagicField),
            };
        }
    }
}