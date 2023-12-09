using System.Collections.Generic;

namespace CrazyWizardNotes.Spells.Logic.SpellOperands
{
    public class SimpleObjectOperand : IObjectOperand
    {
        public IEnumerable<object> Objects { get; set; }

        public SimpleObjectOperand(IEnumerable<object> objects)
        {
            Objects = objects;
        }
    }
}