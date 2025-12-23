using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public class UNetAppBuilder : IUAppBuilder
    {
        private Stack<Action<IApplicationCreateContext>> Actions { get; set; }
        private Stack<Func<IApplicationCreateContext, Task>> AsyncActions { get; set; }

        public UNetAppBuilder()
        {
            Actions = new Stack<Action<IApplicationCreateContext>>();
            AsyncActions = new Stack<Func<IApplicationCreateContext, Task>>();
        }

        public Action<IApplicationCreateContext>[] GetActions()
        {
            return Actions.ToArray();
        }

        public Func<IApplicationCreateContext, Task>[] GetAsyncActions()
        {
            return AsyncActions.ToArray();
        }

        public void RunPerApp(Action<IApplicationCreateContext> func)
        {
            Actions.Push(func);
        }

        public void RunPerApp(Func<IApplicationCreateContext, Task> func)
        {
            AsyncActions.Push(func);
        }
    }
}