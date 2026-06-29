namespace CrazyWizardNotes.Creatures.Abstractions
{
    /// <summary>
    /// Интерфейс, определяющий постоянное поведение существа.
    /// </summary>
    public interface ICreatureBehavior
    {
        public ICreatureTask GetNewTask();
    }
}