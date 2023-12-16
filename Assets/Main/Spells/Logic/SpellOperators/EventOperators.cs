using CrazyWizardNotes.Spells.Logic.Exceptions;
using CrazyWizardNotes.Spells.Logic.SpellOperands;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class OnStartOperator : ISpellOperator
    {
        public int Priority => 0;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            operator1.MustBeNull();

            context.Spell.OnStartCust += operator2;

            return () =>
            {
                throw new System.Exception("Нельзя вызывать делегаты высших операторов");
            };
        }
    }

    public class OnTickOperator : ISpellOperator
    {
        public int Priority => 0;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            operator1.MustBeNull();

            context.Spell.OnTickCust += operator2;

            return () =>
            {
                throw new System.Exception("Нельзя вызывать делегаты высших операторов");
            };
        }
    }

    public class OnEndOperator : ISpellOperator
    {
        public int Priority => 0;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            operator1.MustBeNull();

            context.Spell.OnEndCust += operator2;

            return () =>
            {
                throw new System.Exception("Нельзя вызывать делегаты высших операторов");
            };
        }
    }
}
