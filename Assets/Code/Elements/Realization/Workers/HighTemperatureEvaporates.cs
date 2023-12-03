using CrazyWizardNotes.Elements.Abstractions;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public class HighTemperatureEvaporates : IHandlesUpdate
    {
        private IHasMoisture HasMoisture { get; }
        private IHasTemperature HasTemperature { get; }

        public HighTemperatureEvaporates(IHasTemperature temperature, IHasMoisture moisture)
        {
            HasTemperature = temperature;
            HasMoisture = moisture;
        }

        public void OnUpdate(float deltaTime)
        {
            if (HasTemperature.Temperature <= 50f || HasMoisture.Moisture <= 10f)
            {
                return;
            }

            HasMoisture.Moisture -= deltaTime * HasTemperature.Temperature / 50f;
        }
    }
}
