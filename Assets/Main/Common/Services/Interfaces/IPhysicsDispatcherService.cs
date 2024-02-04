using System;

namespace CrazyWizardNotes.Common.Services
{
    public interface IPhysicsDispatcherService
    {
        void Invoke(Action action);
    }
}