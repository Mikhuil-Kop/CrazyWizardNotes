using CrazyWizardNotes.Core;

namespace CrazyWizardNotes.Common.Services
{
    public interface IPhysicsService
    {
        void AddForce(PhysicsForceParams forceParams);
    }
}