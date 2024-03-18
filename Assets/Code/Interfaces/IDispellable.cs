using System;

namespace CrazyWizardNotes.Interfaces
{
    public interface IDispellable
    {
        bool CanDispell { get; }
        void Dispell();
        event Action OnDispell;
    }
}
