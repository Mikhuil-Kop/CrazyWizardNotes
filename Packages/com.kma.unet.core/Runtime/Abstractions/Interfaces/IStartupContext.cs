namespace UNet.Core.Abstractions
{
    public interface IStartupContext
	{
		public IUAppBuilder AppBuilder { get; }
		public IUSceneBuilder SceneBuilder { get; }
		public IUServiceBuilder ServiceBuilder { get; }
    }
}