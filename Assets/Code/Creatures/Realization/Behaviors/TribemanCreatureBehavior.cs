using CrazyWizardNotes.Creatures.Abstractions;
using System;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class TribemanCreatureBehavior : ICreatureBehavior, IHandlesUpdate
    {
        public TribemanCreatureBehavior(ILogger logger, IPhysicsService physicsService, IUServiceProvider serviceProvider, IActorsCollectionService actorsCollectionService, IActor creatureActor, IHasSpacePoint hasSpacePoint, Creature creature)
        {
            Logger = logger;
            PhysicsService = physicsService;
            ServiceProvider = serviceProvider;
            ActorsCollectionService = actorsCollectionService;
            CreatureActor = creatureActor;
            HasSpacePoint = hasSpacePoint;
            Creature = creature;
        }

        private ILogger Logger { get; }
        private IPhysicsService PhysicsService { get; }
        private IUServiceProvider ServiceProvider { get; }
        private IActorsCollectionService ActorsCollectionService { get; }
        private IActor CreatureActor { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private Creature Creature { get; }
        private float Anger { get; set; } = 0f;


        public void OnUpdate(float deltaTime)
        {
            Creature.CaloriesAccumulated -= deltaTime * 100f * UnityEngine.Random.value;

            if (Creature.CaloriesAccumulated < Creature.CaloriesToSaturation / 4f)
            {
                Anger += 0.2f * deltaTime;
            }

            if (Creature.CaloriesAccumulated > Creature.CaloriesToSaturation)
            {
                Anger -= 0.1f * deltaTime;
            }

            Anger = Math.Clamp(Anger, 0f, 1f);
        }

        public ICreatureTask GetNewTask()
        {
            Logger.LogInfo("Думаю.");

            if (Creature.CaloriesAccumulated < Creature.CaloriesToSaturation / 4f)
            {
                var closestFood = FindClosest(
                    x => x.WithComponents<IEatable, ICreature, IClaimable>(),
                    new()
                    {
                        Predicate = x =>
                               x.Component1 != null
                            && x.Component1.CaloriesLeft > 0.1f
                            && x.Component2 == null
                            && x.Component3?.IsClaimed("", CreatureActor) != true
                    }
                );

                Logger.LogInfo("Хочу есть.");

                if (closestFood != null)
                {
                    Logger.LogInfo("Пойду поем.");
                    return new GoingForFoodCreatureTask(CreatureActor, closestFood.Actor, ServiceProvider);
                }
                else
                {
                    Logger.LogInfo("Нечего есть(");
                }
            }

            return null;
        }

        #region Поиск

        private T FindClosest<T>(Func<IActor, T> selector, FindParams<T> param) where T : class
        {
            return ActorsCollectionService.GetAll()
                .Where(x => param.IncludeSelf || x != Creature)
                .Select(x => new
                {
                    Actor = x,
                    Value = selector(x)
                })
                .Where(x => param.Predicate?.Invoke(x.Value) ?? true)
                .OrderBy(x => PhysicsService.GetDistance(x.Actor.As<IHasSpacePoint>(), HasSpacePoint).Magnitude)
                .FirstOrDefault()?
                .Value;
        }

        private struct FindParams<T> where T : class
        {
            public Func<T, bool> Predicate { get; set; }
            public bool IncludeSelf { get; set; }
        }

        #endregion
    }
}
