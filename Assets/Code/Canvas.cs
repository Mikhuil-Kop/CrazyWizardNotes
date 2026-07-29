using CrazyWizardNotes.UI.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using UNet.Core.Abstractions;

namespace Assets.Code
{
    public class Canvas : IHandlesStart, IHandlesUpdate
    {
        private IServiceProvider ServiceProvider { get; }

        public Canvas(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void OnStart()
        {
            var actionBarService = ServiceProvider.GetRequiredService<IActionBarService>();

            actionBarService.AddAction("Fireball");
            actionBarService.AddAction("LightningBolt");
            actionBarService.AddAction("BlackHole");
        }

        public void OnUpdate(float deltaTime)
        {
        }
    }
}
