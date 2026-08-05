using System;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IUActorBuilder
    {
        void RunPerActorCreate(Action<IActorCreateContext> func);
        void RunPerActorCreated(Action<IActorCreatedContext> func);
        void RunPerActorDestroyed(Action<IActorDestroyedContext> func);

        [Obsolete]
        void RunPerActorCreate(Func<IActorCreateContext, Task> func);
        [Obsolete]
        void RunPerActorCreated(Func<IActorCreatedContext, Task> func);
        [Obsolete]
        void RunPerActorDestroyed(Func<IActorDestroyedContext, Task> func);
    }
}