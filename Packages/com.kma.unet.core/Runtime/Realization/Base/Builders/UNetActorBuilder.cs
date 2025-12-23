using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public class UNetActorBuilder : IUActorBuilder
    {
        private Queue<Action<IActorCreateContext>> Actions { get; set; }
        private Queue<Func<IActorCreateContext, Task>> AsyncActions { get; set; }

        public UNetActorBuilder()
        {
            Actions = new Queue<Action<IActorCreateContext>>();
            AsyncActions = new Queue<Func<IActorCreateContext, Task>>();
        }

        public Action<IActorCreateContext>[] GetActions()
        {
            return Actions.ToArray();
        }

        public Func<IActorCreateContext, Task>[] GetAsyncActions()
        {
            return AsyncActions.ToArray();
        }

        public void RunPerActor(Action<IActorCreateContext> func)
        {
            Actions.Enqueue(func);
        }

        public void RunPerActor(Func<IActorCreateContext, Task> func)
        {
            AsyncActions.Enqueue(func);
        }
    }
}