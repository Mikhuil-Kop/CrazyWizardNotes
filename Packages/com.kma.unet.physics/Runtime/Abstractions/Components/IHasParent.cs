using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public interface IHasParent
    {
        public IActor Parent { get; set; }
    }
}
