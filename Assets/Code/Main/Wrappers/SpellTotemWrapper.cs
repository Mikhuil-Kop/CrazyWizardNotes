using CrazyWizardNotes.Magic.Abstractions;
using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class SpellTotemWrapper : TypedWrapperBase
    {
        public SpellParamsScriptable SpellToSpawn;
        public float ActivationTimeGap = 3f;
        public bool IsEnabled = false;
        public bool IsActive = false;

        protected override IEnumerable<object> GetObjects()
        {
            return new[]
            {
                new Buildings.Realization.SpellParticleTotemConfig()
                {
                    SpellId = SpellToSpawn.DefaultSpellName,
                    ActivationTimeGap = ActivationTimeGap,
                    IsEnabled = IsEnabled,
                    IsActive = IsActive,
                }
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
