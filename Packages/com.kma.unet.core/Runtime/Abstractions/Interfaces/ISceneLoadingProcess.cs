namespace UNet.Core.Abstractions
{
    public interface ISceneLoadingProcess
    {
        ISceneInfo LoadingSceneInfo { get; }
        ISceneData LoadingSceneData { get; }

        float Progress { get; }

        void ForceOpen();
    }
}