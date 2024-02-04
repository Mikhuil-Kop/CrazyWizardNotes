using System;

namespace CrazyWizardNotes.Common.Services
{
    public interface IDispatcherService
    {
        void Invoke(Action action);
    }
}