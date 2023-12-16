namespace CrazyWizardNotes.Spells.Logic
{
    /// <summary>
    /// Эффект заклинания. Существует время каста одного заклинания.
    /// </summary>
    public interface ISpellOperator
    {
        /// <summary>
        /// Приоритет этого элемента в дереве вызова. 100 - вызывается первым, 0 - последним (в корне).
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Эффект заклинания. Вызывается при создании заклинания.
        /// </summary>
        SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context);
    }

    public delegate ISpellOperand SpellOperatorDelegate();
}