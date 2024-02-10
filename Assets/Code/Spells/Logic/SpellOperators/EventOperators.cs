using System;
using System.Linq;
using CrazyWizardNotes.Common.Services;
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

    public class OnNamedEventOperator : ISpellOperator
    {
        public int Priority => 0;

        private string EventName { get; set; }

        public OnNamedEventOperator(string eventName)
        {
            EventName = eventName;
        }

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            var op1 = operator1.Invoke();
            Func<object, bool> selector = null;

            if (op1 is IQueryObjectsOperand)
            {
                selector = (op1 as IQueryObjectsOperand).Selector;
            }
            else if (op1 is IObjectOperand)
            {
                var collection = (op1 as IObjectOperand).Objects;
                selector = (x) => collection.Contains(x);
            }

            if (selector != null)
            {
                
            }

            return () =>
            {
                throw new System.Exception("Нельзя вызывать делегаты высших операторов");
            };
        }
    }
}
