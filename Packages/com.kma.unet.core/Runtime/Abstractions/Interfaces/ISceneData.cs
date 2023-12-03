namespace UNet.Core.Abstractions
{
	public interface ISceneData
	{
		T GetResourceData<T>() where T : class;
		T GetPreloadData<T>() where T : class;
	}
}