using System;

namespace UNet.Animations.Abstractions
{
    public interface IAnimationHandler
    {
        event Action<IAnimationContext> OnAnimationStart;
        event Action<IAnimationContext> OnAnimationUpdate;
        event Action<IAnimationContext> OnAnimationEnd;
    }
}