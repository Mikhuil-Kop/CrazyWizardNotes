using CrazyWizardNotes.Creatures.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class CreatureBase : ICreature, ICanWalk, ICanCarry, ICanAttack, IHasName, IHasEmotion, ICanEat, ICarrieble, IHandlesStart, IHandlesFixedUpdate, IHandlesDestroy, IDisposable
    {
        #region Сервисы

        private ILogger Logger { get; }
        private IAsyncJobService AsyncJobService { get; }
        private IPhysicsService PhysicsService { get; }

        #endregion

        #region Компоненты

        private IActor Actor { get; }
        private IPhysical Physical { get; }
        private IHasSpacePoint HasSpacePoint { get; }

        #endregion

        #region Свойства

        public string Name { get; private set; }
        public float CaloriesToSaturation { get; set; }
        public float CaloriesAccumulated { get; set; }
        public float EatingMultiplier { get; }
        public Emotion Emotion { get; set; }

        private IRegistredJob Job { get; set; }
        private SpacePoint TargetWalkPoint { get; set; }
        private float Anger { get; set; } // 0 - спокойствие, 1 - ярость

        #endregion

        #region Функционал

        public CreatureBase(ILogger logger, IAsyncJobService asyncJobService, IPhysicsService physicsService, IActor actor, IPhysical physical, IHasSpacePoint hasSpacePoint)
        {
            Logger = logger;
            AsyncJobService = asyncJobService;
            PhysicsService = physicsService;
            Actor = actor;
            Physical = physical;
            HasSpacePoint = hasSpacePoint;

            Name = "123";
            CaloriesToSaturation = 2000f;
            CaloriesAccumulated = 1000f;
            EatingMultiplier = 1f;
            Emotion = Emotion.Neutral;
        }

        public void OnStart()
        {
            Job = AsyncJobService.Register(OnCharacterMindEvent, 1f);
            TargetWalkPoint = new SpacePoint(HasSpacePoint.SpacePoint.ParentSpace, new UNet.Vector2D(10, 10));
        }

        public void OnDestroy()
        {
            Job?.Cancel();
        }

        public void Dispose()
        {
            Job?.Dispose();
        }

        public void WalkTo(SpacePoint spacePoint)
        {
            TargetWalkPoint = spacePoint;
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Carry(ICarrieble carry)
        {
            throw new System.NotImplementedException();
        }

        public void Eat(IEatable eatable, float deltaTime)
        {
            CaloriesAccumulated += eatable.EatThis(this, deltaTime);
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------------------------- */

        public void OnCharacterMindEvent()
        {
            Logger.LogInfo("Думаю.");
        }

        public void OnFixedUpdate(float deltaTime)
        {
            var distance = PhysicsService.GetDistance(TargetWalkPoint, HasSpacePoint);
            var direction = distance.Normalized;
            var acceleration = 0.5f;
            var maxSpeed = 1f;

            Physical.ApplyImpulseWithLimit(direction * acceleration, maxSpeed);
        }

        #endregion
    }
}