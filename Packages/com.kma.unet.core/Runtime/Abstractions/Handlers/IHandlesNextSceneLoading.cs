namespace UNet.Core.Abstractions
{
    public interface IHandlesNextSceneLoading
    {
        void OnNextSceneLoading(ISceneLoadingContext context);
    }
}
