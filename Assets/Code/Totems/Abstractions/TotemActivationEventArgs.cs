using System;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Totems.Abstractions
{
    public delegate void TotemActivationHandler(TotemActivationEventArgs args);

    public class TotemActivationEventArgs : EventArgs
    {
        public IActor Actor { get; }
        public ITotem Totem { get; }

        public TotemActivationEventArgs(IActor actor, ITotem totem)
        {
            Actor = actor;
            Totem = totem;
        }
    }
}
