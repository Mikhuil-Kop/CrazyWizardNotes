using System;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IUActorBuilder
    {
        void RunPerActor(Action<IActorCreateContext> func);
        [Obsolete]
        void RunPerActor(Func<IActorCreateContext, Task> func);
    }
}