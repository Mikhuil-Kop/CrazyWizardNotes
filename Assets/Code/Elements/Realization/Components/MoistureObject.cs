using CrazyWizardNotes.Elements.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public sealed class MoistureObject : IHasMoisture
    {
        private IActor Actor { get; }
        private IPhysical Physical { get; }
        private MaterialConfig Config { get; }

        private float _moisture;

        public event MoistureChangedEventHandler OnMoistureChanged;

        public MoistureObject(IHasTemperature temperature, IPhysical physical, MaterialConfig config)
        {
            Physical = physical;
            Config = config;
        }

        public float Moisture
        {
            get => _moisture;
            set => SetMoisture(value);
        }

        public float MoisturePercent
        {
            get => _moisture / MoistureCapacity;
            set => SetMoisture(value * MoistureCapacity);
        }

        public float MoistureCapacity => Config.MoistureCapacity * Physical.Mass;

        private void SetMoisture(float newMoisture)
        {
            newMoisture = Math.Clamp(newMoisture, 0f, MoistureCapacity);

            var oldMoisture = _moisture;

            var newPercent = newMoisture / MoistureCapacity;
            var oldPercent = oldMoisture / MoistureCapacity;

            _moisture = newMoisture;

            OnMoistureChanged?.Invoke(new MoistureChangedEventArgs(Actor, oldMoisture, newMoisture, oldPercent, newPercent));
        }
    }
}
