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
        ISpellOperand Execute(ISpellOperator operator1, ISpellOperator operator2, SpellContext context);
    }
}