namespace CrazyWizardNotes.Spells.Logic.SpellOperands
{
    public class SimpleConditionOperand : IConditionOperand
    {
        public bool Value { get; set; }

        public SimpleConditionOperand(bool value)
        {
            Value = value;
        }
    }
}