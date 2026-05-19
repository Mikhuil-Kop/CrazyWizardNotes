using CrazyWizardNotes.Electricity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrazyWizardNotes.Electricity.Realization
{
    public class ElectricService : IElectricService
    {
        public event ElectricDischargeHandler OnElectricDischarge;

        public void RedistributeCharge(IEnumerable<IElectricConductor> conductors)
        {
            if (conductors == null)
            {
                throw new ArgumentNullException(nameof(conductors));
            }

            var conductorsArr = conductors.ToArray();

            if (conductorsArr.Length == 0)
            {
                return;
            }

            var totalCharge = conductorsArr.Sum(c => c.ElectricCharge);
            var totalCapacity = conductorsArr.Sum(c => c.ElectricCapacity);

            if (Math.Abs(totalCapacity) < 1e-12)
            {
                throw new InvalidOperationException("Общая ёмкость равна нулю. Делить нельзя.");
            }

            var finalVoltage = totalCharge / totalCapacity;

            foreach (var c in conductors)
            {
                c.SetCharge(c.ElectricCapacity * finalVoltage);
            }
        }
    }
}
