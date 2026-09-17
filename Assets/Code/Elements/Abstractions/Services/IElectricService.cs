using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Elements.Abstractions
{
    public interface IElectricService
    {
        event ElectricDischargeEventHandler OnElectricDischarge;
        void RedistributeCharge(IEnumerable<IActor> conductors);
    }
}
