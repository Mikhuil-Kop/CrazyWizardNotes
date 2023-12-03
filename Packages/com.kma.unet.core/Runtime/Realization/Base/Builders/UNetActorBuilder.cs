using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public class UNetActorBuilder : IUActorBuilder
    {
        private Queue<Action<IActorCreateContext>> Actions { get; }
        private Queue<Func<IActorCreateContext, Task>> AsyncActions { get; }

        private Queue<Action<IActorCreatedContext>> ActionsCreated { get; }
        private Queue<Func<IActorCreatedContext, Task>> AsyncActionsCreated { get; }

        private Queue<Action<IActorDestroyedContext>> ActionsDestroy { get; }
        private Queue<Func<IActorDestroyedContext, Task>> AsyncActionsDestroy { get; }

        public UNetActorBuilder()
        {
            Actions = new Queue<Action<IActorCreateContext>>();
            AsyncActions = new Queue<Func<IActorCreateContext, Task>>();

            ActionsCreated = new Queue<Action<IActorCreatedContext>>();
            AsyncActionsCreated = new Queue<Func<IActorCreatedContext, Task>>();

            ActionsDestroy = new Queue<Action<IActorDestroyedContext>>();
            AsyncActionsDestroy = new Queue<Func<IActorDestroyedContext, Task>>();
        }

        public Action<IActorCreateContext>[] GetActions()
        {
            return Actions.ToArray();
        }

        public Func<IActorCreateContext, Task>[] GetAsyncActions()
        {
            return AsyncActions.ToArray();
        }

        public Action<IActorCreatedContext>[] GetCreatedActions()
        {
            return ActionsCreated.ToArray();
        }

        public Func<IActorCreatedContext, Task>[] GetAsyncCreatedActions()
        {
            return AsyncActionsCreated.ToArray();
        }

        public Action<IActorDestroyedContext>[] GetDestroyActions()
        {
            return ActionsDestroy.ToArray();
        }

        public Func<IActorDestroyedContext, Task>[] GetAsyncDestroyActions()
        {
            return AsyncActionsDestroy.ToArray();
        }

        public void RunPerActorCreate(Action<IActorCreateContext> func)
        {
            Actions.Enqueue(func);
        }

        public void RunPerActorCreate(Func<IActorCreateContext, Task> func)
        {
            AsyncActions.Enqueue(func);
        }

        public void RunPerActorCreated(Action<IActorCreatedContext> func)
        {
            ActionsCreated.Enqueue(func);
        }

        public void RunPerActorCreated(Func<IActorCreatedContext, Task> func)
        {
            AsyncActionsCreated.Enqueue(func);
        }

        public void RunPerActorDestroyed(Action<IActorDestroyedContext> func)
        {
            ActionsDestroy.Enqueue(func);
        }

        public void RunPerActorDestroyed(Func<IActorDestroyedContext, Task> func)
        {
            AsyncActionsDestroy.Enqueue(func);
        }
    }
}