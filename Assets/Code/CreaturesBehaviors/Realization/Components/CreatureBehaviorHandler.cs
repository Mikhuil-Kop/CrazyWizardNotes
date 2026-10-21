using CrazyWizardNotes.CreaturesBehaviors.Abstractions;
using System;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public class CreatureBehaviorHandler : IHandlesStart, IHandlesUpdate, IHandlesFixedUpdate, IHandlesDestroy, IDisposable
    {
        private IAsyncJobService AsyncJobService { get; }
        private ICreatureBehavior CreatureBehavior { get; set; }
        private ICreatureChoiceTask CurrentTask { get; set; }
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
                CurrentTask?.Dispose();
                CurrentTask = newTask;
                CurrentTask.OnTaskStart();
            }
        }

        public virtual void OnUpdate(float deltaTime)
        {
            CheckCompleted();
            CurrentTask?.OnTaskUpdate(deltaTime);
            CheckCompleted();
        }

        public virtual void OnFixedUpdate(float deltaTime)
        {
            CheckCompleted();
            CurrentTask?.OnTaskFixedUpdate(deltaTime);
            CheckCompleted();
        }

        public virtual void OnDestroy()
        {
            Job?.Cancel();
        }

        public virtual void Dispose()
        {
            Job?.Dispose();
            CurrentTask?.Dispose();
        }

        private void CheckCompleted()
        {
            if (CurrentTask != null && CurrentTask.IsCompleted)
            {
                CurrentTask.OnTaskEnd();
                CurrentTask.Dispose();
                CurrentTask = null;
            }
        }
    }
}