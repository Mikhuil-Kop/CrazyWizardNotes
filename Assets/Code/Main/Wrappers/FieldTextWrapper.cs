using System;
using System.Collections.Generic;
using TMPro;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class FieldTextWrapper : TypedWrapperBase
    {
        public TextMeshPro TextMeshPro;

        protected override IEnumerable<object> GetObjects()
        {
            return new[]
            {
                TextMeshPro
            };
        }

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(TextMeshProLocalize)
            };
        }
    }
}
