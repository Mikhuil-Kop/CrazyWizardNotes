using CrazyWizardNotes.Elements.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public sealed class TemperatureObject : IHasTemperature
    {
        private IActor Actor { get; }
        private IPhysical Physical { get; }
        private MaterialConfig Config { get; }

        private float _temperature;

        public event TemperatureChangedEventHandler OnTemperatureChanged;

        public TemperatureObject(IActor actor, IPhysical physical, MaterialConfig config)
        {
            Actor = actor;
            Physical = physical;
            Config = config;
        }

        public float Temperature
        {
            get => _temperature;
            set => SetTemperature(value);
        }

        public float ThermalEnergy
        {
            get => _temperature * ThermalCapacity;
            set => SetTemperature(value / ThermalCapacity);
        }

        public float ThermalEmissivity => Config.ThermalEmissivity * Physical.Mass;
        public float ThermalCapacity => Config.ThermalCapacity * Physical.Mass;
        public float ThermalConductivity => Config.ThermalConductivity;

        private void SetTemperature(float newTemperature)
        {
            var oldTemperature = _temperature;

            var newEnergy = newTemperature * ThermalCapacity;
            var oldEnergy = oldTemperature * ThermalCapacity;

            _temperature = newTemperature;

            OnTemperatureChanged?.Invoke(new TemperatureChangedEventArgs(Actor, oldTemperature, newTemperature, oldEnergy, newEnergy));
        }
    }
}
