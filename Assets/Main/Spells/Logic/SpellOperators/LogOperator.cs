using CrazyWizardNotes.Spells.Logic.Exceptions;
using CrazyWizardNotes.Spells.Logic.SpellOperands;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class LogOperator : ISpellOperator
    {
        public int Priority => 100;

        public SpellOperatorDelegate Execute(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                UnityEngine.Debug.Log("Лог " + context.TimeFromStart);
                return null;
            };
        }
    }
}
