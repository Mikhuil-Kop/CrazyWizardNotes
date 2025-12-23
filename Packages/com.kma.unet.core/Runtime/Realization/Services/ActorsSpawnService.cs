using System;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class ActorsSpawnService : IActorsSpawnService
    {
        public void SpawnActor(string actorId, Action<IActorCreateContext> onCreate)
        {
            throw new NotImplementedException();
        }
    }
}