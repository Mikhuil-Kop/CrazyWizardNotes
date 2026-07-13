using CrazyWizardNotes.Creatures.Abstractions;
using System;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class CreatureBehaviorHandler : IHandlesStart, IHandlesUpdate, IHandlesFixedUpdate, IHandlesDestroy, IDisposable
    {
        private IAsyncJobService AsyncJobService { get; }
        private ICreatureBehavior CreatureBehavior { get; set; }
        private ICreatureTask CurrentTask { get; set; }
        private IRegistredJob Job { get; set; }

        public CreatureBehaviorHandler(IAsyncJobService asyncJobService, ICreatureBehavior creatureBehavior)
        {
            AsyncJobService = asyncJobService;
            CreatureBehavior = creatureBehavior;
        }

        public virtual void OnStart()
        {
            Job = AsyncJobService.Register(OnRareUpdate, 1f);

            Job = AsyncJobService.Register(new AsyncJobParams()
            {
                NormalTask = OnRareUpdate,
                RepeatTime = 1f,
                InitialDelay = UnityEngine.Random.value, // Нужно равномерно распределять множество задач.
                WaitType = AsyncJobWaitType.None,
            });
        }

        public void OnRareUpdate()
        {
            var newTask = CreatureBehavior.GetNewTask();

            if (newTask != null)
            {
                CurrentTask?.OnTaskEnd();
                CurrentTask = newTask;
                CurrentTask.OnTaskStart();
            }

            CurrentTask?.OnTaskRareUpdate();

            if (CurrentTask?.IsCompleted ?? false)
            {
                CurrentTask.OnTaskEnd();
                CurrentTask = null;
            }
        }

        public virtual void OnUpdate(float deltaTime)
        {
            CurrentTask?.OnTaskUpdate(deltaTime);

            if (CurrentTask?.IsCompleted ?? false)
            {
                CurrentTask.OnTaskEnd();
                CurrentTask = null;
            }
        }

        public virtual void OnFixedUpdate(float deltaTime)
        {
            CurrentTask?.OnTaskFixedUpdate(deltaTime);

            if (CurrentTask?.IsCompleted ?? false)
            {
                CurrentTask.OnTaskEnd();
                CurrentTask = null;
            }
        }

        public virtual void OnDestroy()
        {
            Job?.Cancel();
        }

        public virtual void Dispose()
        {
            Job?.Dispose();
        }
    }
}