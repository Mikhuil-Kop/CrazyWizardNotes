using System;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IUAppBuilder
    {
        void RunPerApp(Action<IApplicationCreateContext> func);
        [Obsolete]
        void RunPerApp(Func<IApplicationCreateContext, Task> func);
    }
}