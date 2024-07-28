using System;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IUAppBuilder
    {
        void RunPerApp(Action<IUServiceProvider> func);
        [Obsolete]
        void RunPerApp(Func<IUServiceProvider, Task> func);
    }
}