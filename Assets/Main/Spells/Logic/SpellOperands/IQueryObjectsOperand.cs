using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Spells.Logic.SpellOperands
{
    public interface IQueryObjectsOperand : IObjectOperand
    {
        Func<object, bool> Selector { get; set; }
    }
}