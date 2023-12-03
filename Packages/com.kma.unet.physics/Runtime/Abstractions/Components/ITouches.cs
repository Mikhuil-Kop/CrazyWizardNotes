using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public interface ITouches
    {
        IEnumerable<IActor> TouchingActors { get; }
        event Action<OnTouchEventArgs> OnTouch;
    }
}
