using System;
using UnityEngine;
using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Environment.Components
{
    public class EnergyComponent : ObjectComponentBase, IContainsEnergy
    {
        [SerializeField]
        private ElementsValues ContainedEnergy;

        ElementsValues IContainsEnergy.ContainedEnergy => ContainedEnergy;

        public event IContainsEnergy.OnChangeHandler OnChange;


        public ElementsValues ChangeEnergy(ElementsValues value)
        {
            var before = ContainedEnergy;
            var after = ContainedEnergy - value;

            OnChange.Invoke(before, after);

            return after;
        }
    }
}