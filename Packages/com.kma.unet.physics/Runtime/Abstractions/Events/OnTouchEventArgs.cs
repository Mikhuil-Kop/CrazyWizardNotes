using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public class OnTouchEventArgs
    {
        public IActor InvokeActor { get; set; }
        public IEnumerable<IActor> Touched { get; set; }
        public IEnumerable<IActor> Touches { get; set; }
    }
}