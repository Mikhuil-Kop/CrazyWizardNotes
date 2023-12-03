using System;

namespace UNet.Core.Abstractions
{
    public interface ISpawnableActor : IActor
    {
        void AddOnCreateAction(params Action<IActorCreateContext>[] context);
        void AddOnCreatedAction(params Action<IActorCreatedContext>[] context);
        void AddOnDestroyAction(params Action<IActorDestroyedContext>[] context);
        void Initialize();
    }
}
