using UNet.Core.Abstractions;
using UNet.Core.Realization;

namespace Assets.Code
{
    internal class Startup : IStartup
    {
        public void ConfigureStartup(IStartupContext context)
        {
            #region Объекты

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

            #region Взаимодействие Стихий



            #endregion

            #region Дебаг

            UnityEngine.Application.targetFrameRate = 240;

            #endregion
        }
    }
}
