using CrazyWizardNotes.Elements.Abstractions;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public class FlameRaisesTemperatureWorker : IHandlesUpdate
    {
        private IHasTemperature HasTemperature { get; }
        private IFlammable Flammable { get; }

        public FlameRaisesTemperatureWorker(IHasTemperature hasTemperature, IFlammable flammable)
        {
            HasTemperature = hasTemperature;
            Flammable = flammable;
        }

        public void OnUpdate(float deltaTime)
        {
            if (Flammable.IsBurning && HasTemperature.Temperature <= 500f)
            {
                HasTemperature.Temperature += 20f * deltaTime;
            }
        }
    }
}
