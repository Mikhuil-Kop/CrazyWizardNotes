using CrazyWizardNotes.Reading.Abstractions;
using UNet.Core.Abstractions;

namespace Assets.Code
{
    internal class Startup : IStartup
    {
        public void ConfigureStartup(IStartupContext context)
        {
            #region Объекты

            /*
            context.ActorBuilder.RunPerActor(x =>
            {
                if (x.Actor.Tags.Has("PhysicObject"))
                {
                    x.ServiceProvider.Add<UNet.Physics.Realization.PhysicalObject>();
                    x.ServiceProvider.Add<UNet.Sound.Realization.SoundSource>();
                }

                if (x.Actor.Tags.Has("Person"))
                {
                    x.ServiceProvider.Add<UNet.Sound.Realization.SoundReceiver>();
                    x.ServiceProvider.Add<CrazyWizardNotes.Reading.Realization.ReadHandler>();
                }

                if (x.Actor.Tags.Has("WizardAvatar"))
                {
                    x.ServiceProvider.Add<CrazyWizardNotes.Avatar.Realization.WizardAvatar>();
                }
            });

            context.ActorBuilder.RunPerActor(x =>
            {
                if (x.Actor.Tags.Has("PhysicObject"))
                {
                    x.ServiceProvider.GetService<UNet.Physics.Realization.PhysicalObject>();
                    x.ServiceProvider.GetService<UNet.Sound.Realization.SoundSource>();
                }

                if (x.Actor.Tags.Has("Person"))
                {
                    x.ServiceProvider.GetService<UNet.Sound.Realization.SoundReceiver>();
                    x.ServiceProvider.GetService<CrazyWizardNotes.Reading.Realization.ReadHandler>();
                }

                if (x.Actor.Tags.Has("WizardAvatar"))
                {
                    x.ServiceProvider.GetService<CrazyWizardNotes.Avatar.Realization.WizardAvatar>();
                }

                x.Logger.LogInfo($"Актор {x.Actor} инициализирован.");
            });
            */

            context.ActorBuilder.RunPerActor(x =>
            {
                var s = x.ServiceProvider.GetService<IActorsCollectionService>();
                s.AddActor(x.Actor);
            });

            #endregion

            #region Дебаг

            UnityEngine.Application.targetFrameRate = 60;

            #endregion
        }
    }
}
