using CrazyWizardNotes.Elements.Realization;
using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class FlameWrapper : TypedWrapperBase
    {
        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(Flame),
            };
        }
    }
}
