using System.Collections.Generic;

namespace CrazyWizardNotes.CreaturesBehaviors.Abstractions
{
    /// <summary>
    /// Интерфейс, определяющий цель существа, которая может быть выбрана в качестве задачи для выполнения.
    /// </summary>
    public interface ICreatureChoiceProvider
    {
        public IEnumerable<ICreatureChoiceInfo> GetChoices();
    }
}