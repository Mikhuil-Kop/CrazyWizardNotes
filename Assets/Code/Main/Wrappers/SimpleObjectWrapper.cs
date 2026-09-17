using System;
using System.Collections.Generic;
using System.Linq;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class SimpleObjectWrapper : TypedWrapperBase
    {
        public Elements.Realization.MaterialConfigScriptable MaterialConfigScript;

        protected override IEnumerable<object> GetObjects()
        {
            return new[]
            {
                MaterialConfigScript.Value
            };
        }

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Core.Realization.Destroyable),
                typeof(UNet.Physics.Realization.PhysicalObject),
            }
            .Concat(Elements.Realization.ElementsWrapping.GetComponentsForPhysicalObject())
            .Concat(Elements.Realization.ElementsWrapping.GetWorkersForPhysicalObject());
        }
    }
}
