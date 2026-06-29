namespace CrazyWizardNotes.Creatures.Abstractions
{
    /// <summary>
    /// Интерфейс, определяющий задачу, которую существо должно выполнять в данный момент.
    /// </summary>
    public interface ICreatureTask
    {
        public void OnTaskRareUpdate();
        public void OnTaskUpdate(float deltaTime);
        public void OnTaskFixedUpdate(float deltaTime);
    }
}