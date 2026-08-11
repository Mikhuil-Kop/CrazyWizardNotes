using UNet.Core.Abstractions;
using UNet.Core.Realization;
using UnityEngine;

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

            context.SceneBuilder.RunPerScene(x =>
            {
                x.ServiceProvider.Add<DebugLogger>();
            });

            context.ActorBuilder.RunPerActorCreated(x =>
            {
                var s = x.ServiceProvider.GetService<IActorsCollectionService>();
                s.AddActor(x.Actor);
            });

            context.ActorBuilder.RunPerActorDestroyed(x =>
            {
                x.Logger.LogInfo($"Актор {x.Actor} уничтожен.");
                var s = x.ServiceProvider.GetService<IActorsCollectionService>();
                s.RemoveActor(x.Actor);
            });

            #endregion

            #region Дебаг

            UnityEngine.Application.targetFrameRate = 60;

            #endregion
        }
    }
}
