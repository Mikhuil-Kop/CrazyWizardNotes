using System;

namespace CrazyWizardNotes.Creatures.Realization
{
    [Serializable]
    public class EatableCaloriesInfo
    {
        public float CaloriesMax { get; set; }
        public float CaloriesLeft { get; set; }
    }
}