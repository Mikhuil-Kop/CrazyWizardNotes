using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class FoodWrapper : TypedWrapperBase
    {
        public float Calories = 1000f;

        protected override IEnumerable<object> GetObjects()
        {
            Calories = 400f;
            return new[]
            {
                new Creatures.Realization.EatableCaloriesInfo()
                {
                    CaloriesMax = Calories,
                    CaloriesLeft = Calories,
                    EatingSpeed = 400f
                }
            };
        }

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Creatures.Realization.SimpleClaimable),
                typeof(Creatures.Realization.FoodTest),
                typeof(Creatures.Realization.Eatable),
            };
        }
    }
}
