using CrazyWizardNotes.Creatures.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class BehaviorCreature : ICreature, ICanWalk, ICanCarry, ICanAttack, ICanEat, IHasName, IHasEmotion, IHandlesStart, IHandlesUpdate, IHandlesFixedUpdate, IHandlesDestroy, IDisposable
    {
        #region Сервисы

        protected ILogger Logger { get; }
        private IAsyncJobService AsyncJobService { get; }

        #endregion

        #region Свойства

        public string Name { get; set; }
        public float CaloriesToSaturation { get; set; }
        public float CaloriesAccumulated { get; set; }
        public float EatingMultiplier { get; set; }
        public Emotion Emotion { get; set; }

        private ICreatureBehavior CreatureBehavior { get; }
        private ICreatureTask CurrentTask { get; set; }
        private IRegistredJob Job { get; set; }

        #endregion

        #region Функционал

        public BehaviorCreature(ILogger logger, ICreatureBehavior creatureBehavior, IAsyncJobService asyncJobService)
        {
            Logger = logger;
            AsyncJobService = asyncJobService;
            CreatureBehavior = creatureBehavior;

            Name = "123";
            CaloriesToSaturation = 2000f;
            CaloriesAccumulated = 1000f;
            EatingMultiplier = 1f;
            Emotion = Emotion.Neutral;
        }

        public virtual void OnStart()
        {
            Job = AsyncJobService.Register(OnRareUpdate, 1f);
        }

        protected virtual void OnRareUpdate()
        {
            var newTask = CreatureBehavior.GetNewTask();

            if (newTask != null)
            {
                CurrentTask = newTask;
            }

            CurrentTask?.OnTaskRareUpdate();
        }

        public virtual void OnUpdate(float deltaTime)
        {
            CurrentTask?.OnTaskUpdate(deltaTime);
        }

        public virtual void OnFixedUpdate(float deltaTime)
        {
            CurrentTask?.OnTaskFixedUpdate(deltaTime);
        }

        public virtual void OnDestroy()
        {
            Job?.Cancel();
        }

        public virtual void Dispose()
        {
            Job?.Dispose();
        }

        public void WalkTo(SpacePoint spacePoint)
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Carry(ICarrieble carry)
        {
            throw new NotImplementedException();
        }

        public void Eat(IEatable eatable, float deltaTime)
        {
            CaloriesAccumulated += eatable.EatThis(this, deltaTime);
        }

        #endregion
    }
}