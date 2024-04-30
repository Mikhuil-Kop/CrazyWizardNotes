using System;

namespace CrazyWizardNotes.Interfaces
{
    public interface IVanishable
    {
        bool CanVanish { get; }
        void Vanish();
        event Action OnVanish;
    }
}
