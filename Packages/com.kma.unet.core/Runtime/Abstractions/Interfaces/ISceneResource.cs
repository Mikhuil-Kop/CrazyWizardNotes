namespace UNet.Core.Abstractions
{
    public interface ISceneResource
    {
        string SceneName { get; }
        object SceneData { get; }
    }
}