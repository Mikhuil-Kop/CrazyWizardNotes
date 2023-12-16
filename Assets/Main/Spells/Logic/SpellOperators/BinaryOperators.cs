using System.Linq;
using CrazyWizardNotes.Spells.Logic.Exceptions;
using CrazyWizardNotes.Spells.Logic.SpellOperands;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class AndOperator : ISpellOperator
    {
        public int Priority => 59;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                var op1 = operator1?.Invoke();
                var op2 = operator2?.Invoke();

                if (op1 is IConditionOperand)
                {
                    var val1 = op1.ConditionValue() ?? throw new SpellSyntaxException();
                    var val2 = op2.ConditionValue() ?? throw new SpellSyntaxException();

                    return new SimpleConditionOperand(val1 & val2);
                }
                else if (op1 is IObjectOperand)
                {
                    var val1 = op1.ObjectsCollection() ?? throw new SpellSyntaxException();
                    var val2 = op2.ObjectsCollection() ?? throw new SpellSyntaxException();

                    return new SimpleObjectOperand(val1.Intersect(val2));
                }
                else
                {
                    throw new SpellSyntaxException();
                }
            };
        }
    }

    public class OrOperator : ISpellOperator
    {
        public int Priority => 55;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                var op1 = operator1?.Invoke();
                var op2 = operator2?.Invoke();

                if (op1 is IConditionOperand)
                {
                    var val1 = op1.ConditionValue() ?? throw new SpellSyntaxException();
                    var val2 = op2.ConditionValue() ?? throw new SpellSyntaxException();

                    return new SimpleConditionOperand(val1 || val2);
                }
                else if (op1 is IObjectOperand)
                {
                    var val1 = op1.ObjectsCollection() ?? throw new SpellSyntaxException();
                    var val2 = op2.ObjectsCollection() ?? throw new SpellSyntaxException();

                    return new SimpleObjectOperand(val1.Union(val2));
                }
                else
                {
                    throw new SpellSyntaxException();
                }
            };
        }
    }

    public class NotOperator : ISpellOperator
    {
        public int Priority => 50;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                operator1.MustBeNull();
                var op2 = operator2?.Invoke();

                if (op2 is IConditionOperand)
                {
                    var val2 = op2.ConditionValue() ?? throw new SpellSyntaxException();

                    return new SimpleConditionOperand(!val2);
                }
                else if (op2 is IObjectOperand)
                {
                    var val2 = op2.ObjectsCollection() ?? throw new SpellSyntaxException();

                    return new SimpleObjectOperand(context.Components.Except(val2));
                }
                else
                {
                    throw new SpellSyntaxException();
                }
            };
        }
    }
}
