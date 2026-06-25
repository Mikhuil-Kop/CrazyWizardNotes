using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class FoodWrapper : TypedWrapperBase
    {
        public float Calories = 100f;

        protected override IEnumerable<object> GetObjects()
        {
            return new[]
            {
                new Creatures.Realization.EatableCaloriesInfo()
                {
                    CaloriesMax = Calories,
                    CaloriesLeft = Calories,
                    EatingSpeed = 40f
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
