using CrazyWizardNotes.Elements.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public sealed class FlammableObject : IFlammable, IHandlesUpdate, IDisposable
    {
        private IActorsSpawnService SpawnService { get; }
        private IHasSpaceObject HasSpaceObject { get; }
        private IHasTemperature HasTemperature { get; }
        private IHasMoisture HasMoisture { get; }
        private MaterialConfig Config { get; }

        private IFlame Flame { get; set; }

        public event IgnitedEventHandler OnIgnited;
        public event ExtinguishedEventHandler OnExtinguished;

        public bool IsBurning { get; private set; }
        public float AutoignitionTemperature => Config.AutoignitionTemperature;
        public float FlashPointTemperature => Config.FlashPointTemperature;
        public float Flammability => Config.Flammability;

        public FlammableObject(IActorsSpawnService spawn, IHasSpaceObject space, IHasTemperature temperature, IHasMoisture moisture, MaterialConfig config)
        {
            SpawnService = spawn;
            HasSpaceObject = space;
            HasTemperature = temperature;
            HasMoisture = moisture;
            Config = config;
        }

        public void ApplySpark(float intensity)
        {
            if (IsBurning || HasMoisture.MoisturePercent > intensity)
            {
                return;
            }

            if (HasTemperature.Temperature >= FlashPointTemperature && intensity * Flammability >= 0.5f)
            {
                StartBurning();
            }
        }

        public void StartBurning()
        {
            if (IsBurning)
            {
                return;
            }

            IsBurning = true;

            if (Flame == null || !Flame.IsActive)
            {
                Flame = SpawnService.SpawnActor("Nature/Flame").As<IFlame>();
            }

            OnIgnited?.Invoke(new IgnitedEventArgs());
        }

        public void EndBurning()
        {
            if (!IsBurning)
            {
                return;
            }

            IsBurning = false;

            if (Flame != null && Flame.IsActive)
            {
                Flame.Extinguish();
            }

            OnExtinguished?.Invoke(new ExtinguishedEventArgs());
        }

        public void OnUpdate(float deltaTime)
        {
            if (Flame != null && Flame.IsActive)
            {
                Flame.SetPosition(HasSpaceObject.SpaceObject);
            }

            if (Flame != null && !Flame.IsActive)
            {
                EndBurning();
            }

            if (!IsBurning && HasTemperature.Temperature >= AutoignitionTemperature)
            {
                StartBurning();
            }

            else if (IsBurning && HasTemperature.Temperature <= FlashPointTemperature)
            {
                EndBurning();
            }
        }

        public void Dispose()
        {
            Flame?.Extinguish();
        }
    }
}