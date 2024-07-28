namespace UNet.Core.Abstractions
{
    public interface ISceneManagerService
    {
        SceneParams GetCurrentSceneParams();
        void OpenScene(string sceneName, SceneParams sceneParams);
    }
}