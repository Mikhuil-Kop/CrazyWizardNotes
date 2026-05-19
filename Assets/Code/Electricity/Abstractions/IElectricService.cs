using System.Collections.Generic;
using System.Linq;

namespace CrazyWizardNotes.Electricity.Abstractions
{
    public interface IElectricService
    {
        event ElectricDischargeHandler OnElectricDischarge;
        void RedistributeCharge(IEnumerable<IElectricConductor> conductors);
    }

    public static class ElectricServiceExtensions
    {
        public static void RedistributeCharge(this IElectricService service, params IElectricConductor[] conductors)
        {
            service.RedistributeCharge(conductors.AsEnumerable());
        }

        public static void RedistributeCharge<T>(this IElectricService service, params T[] conductors) where T : IElectricConductor
        {
            service.RedistributeCharge(conductors.Select(x => x as IElectricConductor));
        }

        public static void RedistributeCharge<T>(this IElectricService service, IEnumerable<T> conductors) where T : IElectricConductor
        {
            service.RedistributeCharge(conductors.Select(x => x as IElectricConductor));
        }
    }
}
