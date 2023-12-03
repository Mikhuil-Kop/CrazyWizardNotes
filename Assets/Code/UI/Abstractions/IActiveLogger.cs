using UNet.Core.Abstractions;

namespace CrazyWizardNotes.UI.Abstractions
{
    public interface IActiveLogger : ILogger
    {
        public void LogActive(float lifetime, string key, string message);
    }
}
