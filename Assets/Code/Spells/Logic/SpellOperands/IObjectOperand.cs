using System.Collections.Generic;

namespace CrazyWizardNotes.Spells.Logic.SpellOperands
{
    public interface IObjectOperand : ISpellOperand
    {
        public IEnumerable<object> Objects { get; }
    }
}