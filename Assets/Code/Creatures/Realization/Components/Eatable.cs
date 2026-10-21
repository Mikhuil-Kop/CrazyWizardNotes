using CrazyWizardNotes.Creatures.Abstractions;
using System;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class Eatable : IEatable
    {
        public float CaloriesMax { get; private set; }
        public float CaloriesLeft { get; private set; }

        public Eatable(EatableCaloriesInfo info)
        {
            CaloriesMax = info.CaloriesMax;
            CaloriesLeft = info.CaloriesLeft;
        }

        public float EatThis(ICanEat eater)
        {
            if (eater == null)
            {
                throw new ArgumentNullException(nameof(eater));
            }

            if (CaloriesLeft <= 0)
            {
                return 0;
            }

            var eatAmount = Math.Min(CaloriesLeft, eater.CaloriesPerBite);
            
            CaloriesLeft -= eatAmount;

            return eatAmount;
        }
    }
}