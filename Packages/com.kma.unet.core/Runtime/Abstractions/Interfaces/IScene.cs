using UNet.Common;

namespace UNet.Core.Abstractions
{
	public interface IScene
	{
		IUServiceProvider GetServiceProvider();
        IReadonlyTags Tags { get; }
    }
}