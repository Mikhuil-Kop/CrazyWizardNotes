using CrazyWizardNotes.Spells.Logic.Exceptions;
using CrazyWizardNotes.Spells.Logic.SpellOperands;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class EventOperators : ISpellOperator
    {
        public SpellOperatorDelegate Execute(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                operator1.MustBeNull();

                return operator2.Invoke();
            };
        }
    }
}
