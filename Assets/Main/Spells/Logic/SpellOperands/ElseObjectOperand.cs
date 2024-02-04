using System.Collections.Generic;

namespace CrazyWizardNotes.Spells.Logic.SpellOperands
{
    public class ElseObjectOperand : ISpellOperand
    {
        public SpellOperatorDelegate IfDelegate;
        public SpellOperatorDelegate ElseDelegate;

        public ElseObjectOperand(SpellOperatorDelegate ifDelegate, SpellOperatorDelegate elseDelegate)
        {
            IfDelegate = ifDelegate;
            ElseDelegate = elseDelegate;
        }
    }
}