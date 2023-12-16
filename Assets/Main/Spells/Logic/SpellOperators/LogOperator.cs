using CrazyWizardNotes.Spells.Logic.Exceptions;
using CrazyWizardNotes.Spells.Logic.SpellOperands;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class LogOperator : ISpellOperator
    {
        public int Priority => 100;
        private string LogText;

        public LogOperator()
        {
            LogText = "";
        }

        public LogOperator(string logText)
        {
            LogText = logText;
        }

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                UnityEngine.Debug.Log("Лог (" + LogText + ") " + context.TimeFromStart);
                return null;
            };
        }
    }
}
