using System;

namespace CrazyWizardNotes.Interfaces
{
    public interface IDestructible
    {
        bool CanDestruct { get; }
        void Destruct();
        event Action OnDestruct;
    }
}
