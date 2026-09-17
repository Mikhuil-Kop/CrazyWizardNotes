using System;

namespace CrazyWizardNotes.Elements.Abstractions
{
    public class ExtinguishedEventArgs : EventArgs
    {
        public ExtinguishedEventArgs() { }
    }

    public delegate void ExtinguishedEventHandler(ExtinguishedEventArgs e);
}
