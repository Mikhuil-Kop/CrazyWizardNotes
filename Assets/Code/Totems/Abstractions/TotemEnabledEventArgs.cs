using System;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Totems.Abstractions
{
    public delegate void TotemEnabledHandler(TotemEnabledEventArgs args);

    public class TotemEnabledEventArgs : EventArgs
    {
        public IActor Actor { get; }
        public ITotem Totem { get; }

        public TotemEnabledEventArgs(IActor actor, ITotem totem)
        {
            Actor = actor;
            Totem = totem;
        }
    }
}
