using System.Linq;
using CrazyWizardNotes.Spells.Logic.Exceptions;
using CrazyWizardNotes.Spells.Logic.SpellOperands;

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
