using CrazyWizardNotes.Creatures.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UNet.Physics.Realization;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class CreatureTask : ICreatureTask
    {
        public IActor CreatureActor { get; set; }

        public CreatureTask(IActor creatureActor)
        {
            CreatureActor = creatureActor;
        }

        public void OnTaskRareUpdate()
        {
        }

        public void OnTaskUpdate(float deltaTime)
        {
            CaloriesAccumulated -= deltaTime * 200f * UnityEngine.Random.value;

            if (TargetFoodPoint != null && PhysicsService.GetDistance(TargetFoodPoint, HasSpacePoint).Magnitude < 0.5f)
            {
                Logger.LogInfo("Ням");

                var toEat = TargetFood.EatThis(this, deltaTime);

                Logger.LogInfo("toEat : " + toEat);

                CaloriesAccumulated += toEat;

                if (toEat == 0f)
                {
                    Logger.LogInfo("Кончилось");

                    TargetWalkPoint = null;
                    TargetFoodPoint = null;
                    TargetFood = null;
                }

                if (CaloriesAccumulated > CaloriesToSaturation / 2)
                {
                    Logger.LogInfo("Наелся");

                    TargetWalkPoint = null;
                    TargetFoodPoint = null;
                    TargetFood = null;
                }
            }
        }

        public void OnTaskFixedUpdate(float deltaTime)
        {
            if (TargetWalkPoint == null)
            {
                return;
            }

            var distance = PhysicsService.GetDistance(TargetWalkPoint.Value, HasSpacePoint);
            var direction = distance.Normalized;
            var acceleration = 0.5f;
            var maxSpeed = 1f;

            Physical.ApplyImpulseWithLimit(direction * acceleration, maxSpeed);
        }
    }
}