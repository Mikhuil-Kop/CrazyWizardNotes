using CrazyWizardNotes.Avatar.Realization;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Avatar
{
    class Startup : IStartup
    {
        public void ConfigureStartup(IStartupContext context)
        {
            context.ServiceBuilder.AddPerScene<WizardAvatar>();
        }
    }
}
