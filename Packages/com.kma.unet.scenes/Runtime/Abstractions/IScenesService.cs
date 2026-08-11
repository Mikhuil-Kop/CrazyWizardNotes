namespace UNet.Scenes.Abstractions
{
    public interface IScenesService
    {
        public ISceneInfo ParentScene { get; }
        public ISceneInfo LoadScene(string sceneName);
        public ISceneInfo LoadScene(string sceneName);
        public void OpenScene(ISceneInfo scene);
    }


    public interface ISceneInfo
    {
        public string SceneName { get; }
        public object SceneData { get; }
    }

    public interface ISceneLoadInfo
    {

    }

    public interface IScenesServiceAdapter
    {
        public void OnSceneLoaded();
    }
}