using System;

namespace CrazyWizardNotes.Creatures.Abstractions
{
    /// <summary>
    /// Интерфейс, определяющий задачу, которую существо должно выполнять в данный момент.
    /// </summary>
    public interface ICreatureTask
    {
        public bool IsCompleted { get; }
        public void OnTaskStart();
        public void OnTaskRareUpdate();
        public void OnTaskUpdate(float deltaTime);
        public void OnTaskFixedUpdate(float deltaTime);
        public void OnTaskEnd();
    }
}