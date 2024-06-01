using CrazyWizardNotes.Elements;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components
{
    public class EnergyComponent : ObjectComponentBase, IContainsEnergy
    {
        [SerializeField]
        private float EnergyTransfer;
        [SerializeField]
        private ElementsValues ContainedEnergy;

        private event OnEnergyChangedEventHandler OnEnergyChanged;


        public void AddEnergy(ElementsValues value)
        {
            var before = ContainedEnergy;
            var after = ContainedEnergy + value;

            var args = new OnEnergyChangedEventArgs()
            {
                Before = before,
                After = after
            };

            ContainedEnergy = after;

            OnEnergyChanged?.Invoke(args);
        }

        #region Интерфейс

        ElementsValues IContainsEnergy.ContainedEnergy => ContainedEnergy;

        float IContainsEnergy.EnergyTransfer => EnergyTransfer;

        void IContainsEnergy.AddEnergy(ElementsValues value) => AddEnergy(value);

        event OnEnergyChangedEventHandler IContainsEnergy.OnEnergyChanged
        {
            add => OnEnergyChanged += value;
            remove => OnEnergyChanged -= value;
        }

        #endregion
    }
}