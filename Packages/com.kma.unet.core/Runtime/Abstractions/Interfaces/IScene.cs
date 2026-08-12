using UNet.Common;

namespace UNet.Core.Abstractions
{
	public interface IScene
	{
        IReadonlyTags Tags { get; }
        IUServiceProvider GetServiceProvider();
    }
}