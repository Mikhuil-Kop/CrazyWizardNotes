using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class UNetAppBuilder : IUAppBuilder
    {
        private Stack<Action<IUServiceProvider>> Actions { get; set; }
        private Stack<Func<IUServiceProvider, Task>> AsyncActions { get; set; }

        public UNetAppBuilder()
        {
            Actions = new Stack<Action<IUServiceProvider>>();
            AsyncActions = new Stack<Func<IUServiceProvider, Task>>();
        }

        public Action<IUServiceProvider>[] GetActions()
        {
            return Actions.ToArray();
        }

        public Func<IUServiceProvider, Task>[] GetAsyncActions()
        {
            return AsyncActions.ToArray();
        }

        public void RunPerApp(Action<IUServiceProvider> func)
        {
            Actions.Push(func);
        }

        public void RunPerApp(Func<IUServiceProvider, Task> func)
        {
            AsyncActions.Push(func);
        }
    }
}