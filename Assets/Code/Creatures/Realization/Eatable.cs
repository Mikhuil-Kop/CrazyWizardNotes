using CrazyWizardNotes.Creatures.Abstractions;
using System;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class Eatable : IEatable
    {
        public float CaloriesMax { get; private set; }
        public float CaloriesLeft { get; private set; }
        public float EatingSpeed { get; private set; }

        public Eatable(EatableCaloriesInfo info)
        {
            CaloriesMax = info.CaloriesMax;
            CaloriesLeft = info.CaloriesLeft;
            EatingSpeed = info.EatingSpeed;
        }

        public float EatThis(ICanEat digester, float deltaTime)
        {
            if (digester == null)
            {
                throw new ArgumentNullException(nameof(digester));
            }

            if (CaloriesLeft <= 0)
            {
                return 0;
            }

            var eatAmount = Math.Min(CaloriesLeft, EatingSpeed * deltaTime * digester.EatingMultiplier);
            
            CaloriesLeft -= eatAmount;

            return eatAmount;
        }
    }
}