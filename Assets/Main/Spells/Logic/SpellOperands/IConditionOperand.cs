namespace CrazyWizardNotes.Spells.Logic.SpellOperands
{
    public interface IConditionOperand : ISpellOperand
    {
        bool Value { get; }
    }
}