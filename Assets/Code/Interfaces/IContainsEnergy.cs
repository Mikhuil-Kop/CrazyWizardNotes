using System;
using static CrazyWizardNotes.Interfaces.IContainsEnergy;

namespace CrazyWizardNotes.Interfaces
{
    public interface IContainsEnergy
    {
        public delegate void OnChangeHandler(ElementsValues before, ElementsValues after);
        ElementsValues ContainedEnergy { get; }
        ElementsValues ChangeEnergy(ElementsValues value);
        event OnChangeHandler OnChange;
    }
}
