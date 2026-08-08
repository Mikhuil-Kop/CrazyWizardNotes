using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class SpellTotemWrapper : TypedWrapperBase
    {
        public Buildings.Realization.SpellParticleTotemConfig TotemConfig;

        protected override IEnumerable<object> GetObjects()
        {
            return new[]
            {
                TotemConfig
            };
        }

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Core.Realization.Destroyable),
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Buildings.Realization.SpellParticleTotem),
            };
        }
    }
}
