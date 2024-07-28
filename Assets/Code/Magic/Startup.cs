using CrazyWizardNotes.Magic.Realization;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Magic
{
    class Startup : IStartup
    {
        public void ConfigureStartup(IStartupContext context)
        {
            context.ServiceBuilder.AddPerScene<SpellBendService>();
            context.ServiceBuilder.AddPerScene<SpellParticleService>();
            
            context.SceneBuilder.RunPerScene(x =>
            {
                _ = x.GetService<SpellBendService>();
                _ = x.GetService<SpellParticleService>();
            });
        }
    }
}
