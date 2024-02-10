using System.Linq;
using CrazyWizardNotes.Spells.Logic.SpellOperands;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public class PointerOperator : ISpellOperator
    {
        public int Priority => 100;

        private string Tag { get; }

        public PointerOperator(string tag)
        {
            Tag = tag;
        }

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            return () =>
            {
                operator1.MustBeNull();
                operator2.MustBeNull();

                return new QueryObjectOperand((x) => x is ITaggable && (x as ITaggable).Tags.Contains(Tag), () => context.Components);
            };
        }
    }
}
