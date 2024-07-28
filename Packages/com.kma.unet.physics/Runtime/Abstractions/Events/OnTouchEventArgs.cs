using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public class OnTouchEventArgs
    {
        public IActor Subject { get; set; }
    }
}