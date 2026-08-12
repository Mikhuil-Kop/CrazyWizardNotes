using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    internal class UNetSceneData : ISceneData
    {
        public object ResourceRawData { get; set; }
        public object PreloadRawData { get; set; }

        public T GetResourceData<T>() where T : class
        {
            return ResourceRawData as T;
        }

        public T GetPreloadData<T>() where T : class
        {
            return PreloadRawData as T;
        }
    }
}
