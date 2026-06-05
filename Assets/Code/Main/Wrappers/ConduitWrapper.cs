using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class ConduitWrapper : TypedWrapperBase
    {
        public float Charge;
        public float Capacity;

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Electricity.Realization.ElectricConductor),
            };
        }

        protected override IDictionary<string, object> GetKeyedObjects()
        {
            return new Dictionary<string, object>
            {
                { "charge", Charge },
                { "capacity", Capacity }
            };
        }
    }
}
