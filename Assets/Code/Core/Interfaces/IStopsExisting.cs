using System;

namespace CrazyWizardNotes.Interfaces
{
    public interface IStopsExisting
    {
        void StopExisting();
        event Action OnStopExisting;
    }
}
