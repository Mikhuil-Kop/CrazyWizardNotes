using CrazyWizardNotes.Creatures.Abstractions;
using UNet.Core.Abstractions;
using UNet.Core.Realization;
using UNet.Physics.Abstractions;
using UNet.Physics.Realization;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class TribemanCreatureBehavior : ICreatureBehavior, IHandlesUpdate
    {
        private ILogger Logger { get; }
        private BehaviorCreature Creature { get; } // TODO: Протекшая зависимость.

        public TribemanCreatureBehavior(ILogger logger, BehaviorCreature creature)
        {
            Logger = logger;
            Creature = creature;
        }

        public ICreatureTask GetNewTask()
        {
            Logger.LogInfo("Думаю.");
            Logger.LogInfo("CaloriesAccumulated: " + Creature.CaloriesAccumulated);
            Logger.LogInfo("CaloriesToSaturation: " + Creature.CaloriesToSaturation);

            if (Creature.CaloriesAccumulated < Creature.CaloriesToSaturation / 4f)
            {
                Logger.LogInfo("Хочу есть.");

                //var closestFood = ActorsCollectionService.GetAll()
                //    .Where(x => x.Is<IEatable>() && x.Is<IHasSpacePoint>() && !x.Is<ICreature>())
                //    .Select(x => new { food = x.As<IEatable>(), point = x.As<IHasSpacePoint>() })
                //    .Where(x => x.food.CaloriesLeft > 0.1f)
                //    .OrderBy(x => PhysicsService.GetDistance(x.point, Creature.HasSpacePoint).Magnitude)
                //    .FirstOrDefault();

                //TargetWalkPoint = closestFood?.point.SpacePoint;
                //TargetFoodPoint = closestFood?.point;
                //TargetFood = closestFood?.food;
            }

            return null;
        }

        public void OnUpdate(float deltaTime)
        {
            CanEat.CaloriesAccumulated -= deltaTime * 200f * UnityEngine.Random.value;
        }
    }
}