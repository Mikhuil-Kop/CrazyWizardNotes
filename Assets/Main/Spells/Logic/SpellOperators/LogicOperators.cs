using CrazyWizardNotes.Spells.Logic.Exceptions;
using CrazyWizardNotes.Spells.Logic.SpellOperands;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class AndOperator : ISpellOperator
    {
        public SpellOperatorDelegate Execute(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                var val1 = operator1?.Invoke().ConditionValue() ?? throw new WrondSpellSyntaxException();
                var val2 = operator2?.Invoke().ConditionValue() ?? throw new WrondSpellSyntaxException();

                return new SimpleConditionOperand(val1 & val2);
            };
        }
    }

    public class OrOperator : ISpellOperator
    {
        public SpellOperatorDelegate Execute(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                var val1 = operator1?.Invoke().ConditionValue() ?? throw new WrondSpellSyntaxException();
                var val2 = operator2?.Invoke().ConditionValue() ?? throw new WrondSpellSyntaxException();

                return new SimpleConditionOperand(val1 | val2);
            };
        }
    }

    public class NotOperator : ISpellOperator
    {
        public SpellOperatorDelegate Execute(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                operator1.MustBeNull();

                var val2 = operator2?.Invoke().ConditionValue() ?? throw new WrondSpellSyntaxException();

                return new SimpleConditionOperand(!val2);
            };
        }
    }
}
