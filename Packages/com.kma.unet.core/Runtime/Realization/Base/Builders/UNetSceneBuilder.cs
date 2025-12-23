using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public class UNetSceneBuilder : IUSceneBuilder
    {
        private Queue<Action<ISceneCreateContext>> Actions { get; set; }
        private Queue<Func<ISceneCreateContext, Task>> AsyncActions { get; set; }

        public UNetSceneBuilder()
        {
            Actions = new Queue<Action<ISceneCreateContext>>();
            AsyncActions = new Queue<Func<ISceneCreateContext, Task>>();
        }

        public Action<ISceneCreateContext>[] GetActions()
        {
            return Actions.ToArray();
        }

        public Func<ISceneCreateContext, Task>[] GetAsyncActions()
        {
            return AsyncActions.ToArray();
        }

        public void RunPerScene(Action<ISceneCreateContext> func)
        {
            Actions.Enqueue(func);
        }

        public void RunPerScene(Func<ISceneCreateContext, Task> func)
        {
            AsyncActions.Enqueue(func);
        }
    }
}