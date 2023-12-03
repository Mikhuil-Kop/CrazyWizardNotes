using CrazyWizardNotes.Spells.Logic.SpellOperands;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class AndOperator : ISpellOperator
    {
        public ISpellOperand Execute(ISpellOperator operator1, ISpellOperator operator2, SpellContext context)
        {
            return new SimpleConditionOperand(operator1.);
        }
    }

    public class OrOperator : ISpellOperator
    {
        public ISpellOperand Execute(ISpellOperator operator1, ISpellOperator operator2, SpellContext context)
        {
            var value1 = operator1.Execute().ConditionValue();
        }
    }

    public class NotOperator : ISpellOperator
    {
        public ISpellOperand Execute(ISpellOperator operator1, ISpellOperator operator2, SpellContext context)
        {

        }
    }
}
