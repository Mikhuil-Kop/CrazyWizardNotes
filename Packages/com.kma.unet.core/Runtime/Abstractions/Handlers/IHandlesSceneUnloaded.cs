namespace UNet.Core.Abstractions
{
    public interface IHandlesSceneUnloaded
    {
        /// <summary>
        /// Выполняется при выгрузке текущей сцены.
        /// </summary>
        void OnSceneUnloaded(ISceneInfo nextSceneInfo, ISceneData nextSceneData);
    }
}
