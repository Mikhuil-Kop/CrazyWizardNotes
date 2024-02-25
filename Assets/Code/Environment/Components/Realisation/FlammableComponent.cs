using CrazyWizardNotes.Environment.Components.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components.Realisation
{
    public class FlammableComponent : ObjectComponentBase, IFlammableComponent
    {
        [SerializeField]
        private float FuelEnergy;
        [SerializeField]
        private float FuelEnergyBurnoutSpeed;
        [SerializeField]
        private float EnergyToStartFire;


        float IFlammableComponent.FuelEnergy
        {
            get { return FuelEnergy; }
            set { FuelEnergy = value; }
        }

        float IFlammableComponent.FuelEnergyBurnoutSpeed
        {
            get { return FuelEnergyBurnoutSpeed; }
            set { FuelEnergyBurnoutSpeed = value; }
        }

        float IFlammableComponent.EnergyToStartFire
        {
            get { return EnergyToStartFire; }
            set { EnergyToStartFire = value; }
        }
    }
}