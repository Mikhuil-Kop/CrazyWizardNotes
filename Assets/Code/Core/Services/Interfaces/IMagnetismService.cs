using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Common.Services
{
    public interface IMagnetismService
    {
        void Register<T>(T target) where T : IMagnetic, ISpacePoint;
        void Unregister<T>(T target) where T : IMagnetic, ISpacePoint;
    }
}