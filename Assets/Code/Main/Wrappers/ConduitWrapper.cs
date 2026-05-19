using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class ConduitWrapper : TypedWrapperBase
    {
        public string BookName;

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Electricity.Realization.ElectricConductor),
            };
        }
    }
}
