using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class BookWrapper : TypedWrapperBase
    {
        public string BookName;

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Reading.Realization.ResourcesReadSource),
            };
        }

        protected override IDictionary<string, object> GetKeyedObjects()
        {
            return new Dictionary<string, object>
            {
                { "ReadResourceName", $"Books/{BookName}" }
            };
        }
    }
}
