using CrazyWizardNotes.Elements.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public class MonolithElementMaterial : IHasTemperature, IHasMoisture, IFlammable, IElectricConductor, IHandlesUpdate, IDisposable
    {
        #region Поля

        private IActorsSpawnService SpawnService { get; }
        private IElectricService ElectricService { get; }
        private IActor Actor { get; }
        private ITouches Touches { get; }
        private IPhysical Physical { get; }
        private IHasSpaceObject SpaceObject { get; }
        private MaterialConfig Config { get; }

        private bool _isBurning;
        private float _temperature;
        private float _temperatureEnergy;
        private float _moisture;
        private float _moisturePercent;
        private float _electricCharge;
        private IFlame _attachedFlame;

        public MonolithElementMaterial(IActorsSpawnService spawnService, IElectricService electricService, IActor actor, ITouches touches, IPhysical physical, IHasSpaceObject spaceObject, MaterialConfig config)
        {
            SpawnService = spawnService;
            ElectricService = electricService;
            Actor = actor;
            Touches = touches;
            Physical = physical;
            SpaceObject = spaceObject;
            Config = config;

            Touches.OnTouch += OnTouch;
        }

        #endregion

        #region События

        public event IgnitedEventHandler OnIgnited;
        public event ExtinguishedEventHandler OnExtinguished;
        public event TemperatureChangedEventHandler OnTemperatureChanged;
        public event MoistureChangedEventHandler OnMoistureChanged;
        public event ElectricDischargeEventHandler OnElectricDischarge;

        #endregion

        #region Публичные поля

        public float Temperature { get => _temperature; set => SetTemperature(value); }
        public float ThermalEnergy { get => _temperatureEnergy; set => SetThermalEnergy(value); }
        public float ThermalCapacity => Config.ThermalCapacity * Physical.Mass;
        public float ThermalConductivity => Config.ThermalConductivity;
        public float ThermalEmissivity => Config.ThermalEmissivity * Physical.Mass;

        public float Moisture { get => _moisture; set => SetMoisture(value); }
        public float MoisturePercent { get => _moisturePercent; set => SetMoisturePercent(value); }
        public float MoistureCapacity => Config.MoistureCapacity * Physical.Mass;

        public float ElectricCharge { get => _electricCharge; set => SetElectricCharge(value); }
        public float ElectricCapacity => Config.ElectricCapacity * Physical.Mass;

        public bool IsBurning => _isBurning;
        public float AutoignitionTemperature => Config.AutoignitionTemperature;
        public float FlashPointTemperature => Config.FlashPointTemperature;
        public float Flammability => Config.Flammability;

        #endregion

        #region Функции изменения

        private void SetThermalEnergy(float energy)
        {
            SetTemperature(energy / ThermalCapacity);
        }

        private void SetTemperature(float temperature)
        {
            var temperatureEnergy = temperature * ThermalCapacity;

            OnTemperatureChanged?.Invoke(new TemperatureChangedEventArgs(Actor, _temperature, temperature, _temperatureEnergy, temperatureEnergy));

            _temperature = temperature;
            _temperatureEnergy = temperatureEnergy;
        }

        private void SetMoisturePercent(float moisturePercent)
        {
            SetMoisture(moisturePercent * MoistureCapacity);
        }

        private void SetMoisture(float moisture)
        {
            var moisturePercent = moisture / MoistureCapacity;

            if (moisturePercent > 1f)
            {
                moisturePercent = 1f;
                moisture = MoistureCapacity;
            }

            OnMoistureChanged?.Invoke(new MoistureChangedEventArgs(Actor, _moisture, moisture, _moisturePercent, moisturePercent));

            _moisture = moisture;
            _moisturePercent = moisturePercent;
        }

        private void SetElectricCharge(float chargeValue)
        {
            OnElectricDischarge?.Invoke(new ElectricDischargeEventArgs(Actor, _electricCharge, chargeValue));

            _electricCharge = chargeValue;
        }

        public void ApplySpark(float intensity)
        {
            //if (_isBurning || intensity <= 0f) return;
            //if (_moisture != null && _moisture.Moisture > 0.05f) return;

            //var temp = _temperature != null ? _temperature.Temperature : 0f;

            //if (temp >= _config.AutoignitionTemperature)
            //{
            //    StartBurning();
            //    return;
            //}

            //if (temp >= _config.FlashPoint)
            //{
            //    var chance = intensity * _config.Flammability;
            //    if (chance >= 0.5f)
            //        StartBurning();
            //}
        }

        public void StartBurning()
        {
            if (_isBurning)
            {
                return;
            }

            _isBurning = true;

            if (_attachedFlame == null || !_attachedFlame.IsActive)
            {
                _attachedFlame = SpawnFlame();
            }

            //OnIgnited?.Invoke(this, new IgnitedEventArgs());
        }

        public void EndBurning()
        {
            if (!_isBurning)
            {
                return;
            }

            _isBurning = false;

            if (_attachedFlame != null && _attachedFlame.IsActive)
            {
                _attachedFlame.Extinguish();
            }

            //OnExtinguished?.Invoke(this, new ExtinguishedEventArgs());
        }

        private IFlame SpawnFlame()
        {
            var flame = SpawnService.SpawnActor("Nature/Flame");
            return flame.As<IFlame>();
        }

        public void Dispose()
        {
            _attachedFlame?.Extinguish();
        }

        #endregion Функции изменения

        #region Функции динамики

        public void OnUpdate(float deltaTime)
        {
            if (_attachedFlame != null && !_attachedFlame.IsActive)
            {
                EndBurning();
            }

            if (_attachedFlame != null && _attachedFlame.IsActive)
            {
                _attachedFlame.SetPosition(SpaceObject.SpaceObject);
            }

            // При высоких температурах вода исчезает.
            if (Temperature > 50 && Moisture > 10f)
            {
                Moisture -= Temperature * deltaTime / 50f;
                Temperature -= deltaTime;
            }

            // Горящий объект нагревается.
            if (IsBurning && Temperature <= 500f)
            {
                Temperature += 20f * deltaTime;
            }

            // Горячий объект возгорает
            if (!IsBurning && Temperature >= AutoignitionTemperature)
            {
                StartBurning();
            }

            // Холодный объект тухнет
            if (IsBurning && Temperature <= FlashPointTemperature)
            {
                EndBurning();
            }
        }

        private void OnTouch(OnTouchEventArgs x)
        {
            var actors = x.Touches.ConcatAdd(x.InvokeActor).ActorsAs<IElectricConductor>();
            ElectricService.RedistributeCharge(actors);
        }

        #endregion Функции динамики
    }
}
