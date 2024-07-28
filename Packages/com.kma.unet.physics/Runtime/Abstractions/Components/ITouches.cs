using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public interface ITouches
    {
        IEnumerable<IActor> TouchingSubjects { get; }
        event Action<OnTouchEventArgs> OnTouch;
    }
}
