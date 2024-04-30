using System;

namespace CrazyWizardNotes.Common.Services
{
    public interface ISchedulerService
    {
        void Shedule(float delay, Action action);
    }
}