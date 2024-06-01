using CrazyWizardNotes.Environment.Components;
using CrazyWizardNotes.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Elements
{
    public class FlammableComponent : ObjectComponentBase, IFlammable
    {
        [SerializeField]
        private bool Burning;
        [SerializeField]
        private float MaxFuelEnergy;
        [SerializeField]
        private float CurrentFuelEnergy;
        [SerializeField]
        private float FuelEnergyBurnoutSpeed;
        [SerializeField]
        private float EnergyToStartFire;

        [SerializeField]
        private SpriteRenderer Sprite;

        private void Update()
        {
            if (Sprite == null)
            {
                return;
            }

            var x = CurrentFuelEnergy / MaxFuelEnergy;
            Sprite.color = new Color(x, x, x, 1);
        }

        #region Interface

        bool IFlammable.Burning
        {
            get { return Burning; }
            set { Burning = value; }
        }

        float IFlammable.MaxFuelEnergy
        {
            get { return MaxFuelEnergy; }
            set { MaxFuelEnergy = value; }
        }

        float IFlammable.CurrentFuelEnergy
        {
            get { return CurrentFuelEnergy; }
            set { CurrentFuelEnergy = value; }
        }

        float IFlammable.FuelEnergyBurnoutSpeed
        {
            get { return FuelEnergyBurnoutSpeed; }
            set { FuelEnergyBurnoutSpeed = value; }
        }

        float IFlammable.EnergyToStartBurning
        {
            get { return EnergyToStartFire; }
            set { EnergyToStartFire = value; }
        }

        #endregion
    }
}