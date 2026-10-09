namespace CrazyWizardNotes.CreaturesBehaviors.Abstractions
{
    /// <summary>
    /// Интерфейс, определяющий задачу, которую существо должно выполнять в данный момент.
    /// </summary>
    public interface ICreatureChoiceTask
    {
        public bool IsCompleted { get; }
        public void OnTaskStart();
        public void OnTaskRareUpdate();
        public void OnTaskUpdate(float deltaTime);
        public void OnTaskFixedUpdate(float deltaTime);
        public void OnTaskEnd();
    }
}