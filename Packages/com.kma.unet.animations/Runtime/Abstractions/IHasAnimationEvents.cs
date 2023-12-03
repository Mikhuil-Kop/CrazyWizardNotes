using System;

namespace UNet.Animations.Abstractions
{
    public interface IHasAnimationEvents
    {
        event Action<string> OnAnimationEvent;
    }
}
