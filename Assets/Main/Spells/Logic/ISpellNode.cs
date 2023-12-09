namespace CrazyWizardNotes.Spells.Logic
{
    /// <summary>
    /// Часть заклинания. Отвечает за иерархию частей в заклинании. Сущестует все, пока заклинание собрано.
    /// </summary>
    public interface ISpellNode
    {
        /// <summary>
        /// Приоритет этого элемента в дереве вызова. 100 - вызывается первым. 0 - последним.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Создать оператор заклинания.
        /// </summary>
        ISpellOperator CreateSpellOperator();
    }
}
