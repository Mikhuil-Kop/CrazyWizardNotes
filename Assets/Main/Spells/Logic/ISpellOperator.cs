
/// <summary>
/// Эффект заклинания. Существует время каста одного заклинания.
/// </summary>
public interface ISpellOperator
{
    /// <summary>
    /// Эффект заклинания.
    /// </summary>
    ISpellOperand Execute(ISpellOperator operator1, ISpellOperator operator2);
}
