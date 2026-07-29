using System;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    [Obsolete]
    public class ActorsSpawnService : IActorsSpawnService
    {
        public IActor SpawnActor(string actorId, Action<IActorCreateContext> onCreate)
        {
            throw new NotImplementedException();
        }
    }
}