namespace UNet.Core.Abstractions
{
    public interface IHandlesSceneLoaded
    {
        /// <summary>
        /// Выполняется при загрузке текущей сцены. Вызывается после OnAwake, OnStart и OnEnable.
        /// </summary>
        void OnSceneLoaded(ISceneInfo sceneInfo, ISceneData sceneData);
    }
}
