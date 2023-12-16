namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class LogicOperators : ISpellOperator
    {
        public int Priority => 100;

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                UnityEngine.Debug.Log("Лог " + context.TimeFromStart);
                return null;
            };
        }
    }
}
