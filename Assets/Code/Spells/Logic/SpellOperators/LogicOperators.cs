using System.Linq;
using CrazyWizardNotes.Spells.Logic.Exceptions;
using CrazyWizardNotes.Spells.Logic.SpellOperands;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class ThenOperator : ISpellOperator
    {
        public int Priority => 100;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                var op1 = operator1.Invoke();

                if (op1.ConditionValue() ?? false)
                {
                    return operator2.Invoke();
                }

                return new SimpleObjectOperand();
            };
        }
    }

    public class ElseOperator : ISpellOperator
    {
        public int Priority => 100;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                return new ElseObjectOperand(operator1, operator2);
            };
        }
    }

    public class TriggersOperator : ISpellOperator
    {
        public int Priority => 100;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                return new ElseObjectOperand(operator1, operator2);
            };
        }
    }

    public class SpawnOperator : ISpellOperator
    {
        public int Priority => 100;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                operator1.MustBeNull();

                var prefab = operator2?.Invoke()
                    .ObjectsCollection()
                    .As<ISpawnable>()
                    .FirstOrDefault();

                return new SimpleObjectOperand(prefab?.Spawn());
            };
        }
    }
}
