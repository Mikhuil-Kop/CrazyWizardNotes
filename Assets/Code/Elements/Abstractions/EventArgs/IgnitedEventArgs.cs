using System;

namespace CrazyWizardNotes.Elements.Abstractions
{
    public class IgnitedEventArgs : EventArgs
    {
        public IgnitedEventArgs() { }
    }

    public delegate void IgnitedEventHandler(object sender, IgnitedEventArgs e);
}
