using System;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IUSceneBuilder
    {
        void RunPerScene(Action<IUServiceProvider> func);
        [Obsolete]
        void RunPerScene(Func<IUServiceProvider, Task> func);
    }
}