namespace CrazyWizardNotes.Spells.Logic
{
    /// <summary>
    /// Эффект заклинания. Существует время каста одного заклинания.
    /// </summary>
    public interface ISpellOperator
    {
        /// <summary>
        /// Эффект заклинания. Обычно вызывается каждый тик применения заклинания.
        /// </summary>
        SpellOperatorDelegate Execute(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context);
    }

    public delegate ISpellOperand SpellOperatorDelegate();
}