using CrazyWizardNotes.Elements.Abstractions;
using System;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public class DischargeOnTouchWorker : IDisposable
    {
        private IElectricService ElectricService { get; }
        private ITouches Touches { get; }

        public DischargeOnTouchWorker(IElectricService electricService, ITouches touches, IElectricConductor electricConductor)
        {
            ElectricService = electricService;
            Touches = touches;

            Touches.OnTouch += Touches_OnTouch;
        }

        private void Touches_OnTouch(OnTouchEventArgs obj)
        {
            ElectricService.RedistributeCharge(obj.Touches.ConcatAdd(obj.InvokeActor));
        }

        public void Dispose()
        {
            Touches.OnTouch -= Touches_OnTouch;
        }
    }
}
