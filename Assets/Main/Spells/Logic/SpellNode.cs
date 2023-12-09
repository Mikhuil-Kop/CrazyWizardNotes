using System;

namespace CrazyWizardNotes.Spells.Logic
{
    public class SpellNode : ISpellNode
    {
        public int Priority => Operator.Priority;

        public ISpellOperator CreateSpellOperator()
        {
            return Operator;
        }


        private ISpellOperator Operator { get; set; }
        public SpellNode(ISpellOperator @operator)
        {
            Operator = @operator;
        }
    }
}