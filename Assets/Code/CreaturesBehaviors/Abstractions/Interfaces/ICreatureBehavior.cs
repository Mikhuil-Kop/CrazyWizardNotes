namespace CrazyWizardNotes.CreaturesBehaviors.Abstractions
{
    /// <summary>
    /// Интерфейс, определяющий постоянное поведение существа.
    /// </summary>
    public interface ICreatureBehavior
    {
        public ICreatureChoiceTask GetNewTask();
    }
}