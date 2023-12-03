using System;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IUSceneBuilder
    {
        void RunPerScene(Action<ISceneCreateContext> func);
        [Obsolete]
        void RunPerScene(Func<ISceneCreateContext, Task> func);
    }
}