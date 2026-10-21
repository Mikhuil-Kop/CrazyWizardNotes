using System;

namespace CrazyWizardNotes.CreaturesBehaviors.Abstractions
{
    /// <summary>
    /// Интерфейс, определяющий задачу, которую существо должно выполнять в данный момент.
    /// </summary>
    public interface ICreatureChoiceTask : IDisposable
    {
        public bool IsCompleted { get; }
        public void OnTaskStart();
        public void OnTaskUpdate(float deltaTime);
        public void OnTaskFixedUpdate(float deltaTime);
        public void OnTaskEnd();
    }
}