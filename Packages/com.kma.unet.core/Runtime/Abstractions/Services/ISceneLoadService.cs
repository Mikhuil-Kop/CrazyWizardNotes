namespace UNet.Core.Abstractions
{
    public interface ISceneLoadService
	{
        ISceneInfo ParentSceneInfo { get; }
        ISceneInfo CurrentSceneInfo { get; }
        ISceneData CurrentSceneData { get; }

        ISceneLoadingProcess LoadScene(string nextSceneName, object sceneData1, object sceneData2);
    }
}