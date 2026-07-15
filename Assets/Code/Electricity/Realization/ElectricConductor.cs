using CrazyWizardNotes.Electricity.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Electricity.Realization
{
    public class ElectricConductor : IElectricConductor
    {
        private IElectricService ElectricService { get; }
        private SpriteRenderer SpriteRenderer { get; }

        public float ElectricCapacity { get; private set; }
        public float ElectricCharge { get; private set; }

        public event ElectricDischargeHandler OnElectricDischarge;


        public ElectricConductor(IElectricService electricService, ITouches touches, SpriteRenderer spriteRenderer, [FromKey("charge")] float charge, [FromKey("capacity")] float capacity)
        {
            ElectricService = electricService;
            SpriteRenderer = spriteRenderer;

            ElectricCharge = charge;
            ElectricCapacity = capacity;

            SetCharge(charge);

            touches.OnTouch += OnTouch;
        }

        public void SetCharge(float chargeValue)
        {
            var chargeDelta = ElectricCharge - chargeValue;

            ElectricCharge = chargeValue;
            SpriteRenderer.color = Color.Lerp(Color.red, Color.blue, (ElectricCharge + 10f) / 20f);
            Debug.Log($"Electric Charge: ({GetHashCode()}: {ElectricCharge})");

            OnElectricDischarge?.Invoke(chargeDelta);
        }

        private void OnTouch(OnTouchEventArgs x)
        {
            var actors = x.Touches.ConcatAdd(x.InvokeActor).ActorsAs<IElectricConductor>();
            ElectricService.RedistributeCharge(actors);
        }
    }
}
