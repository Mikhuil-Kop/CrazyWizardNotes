namespace UNet.Core.Abstractions
{
    public interface IStartupContext
	{
		IUAppBuilder AppBuilder { get; }
		IUSceneBuilder SceneBuilder { get; }
		IUActorBuilder ActorBuilder { get; }
        IUServiceBuilder ServiceBuilder { get; }
    }
}