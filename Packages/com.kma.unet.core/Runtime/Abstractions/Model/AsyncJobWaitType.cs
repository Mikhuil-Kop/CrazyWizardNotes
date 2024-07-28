namespace UNet.Core.Abstractions
{
    public enum AsyncJobWaitType : byte
    {
        None,
        WaitImmediately,
        WaitBeforeNext,
        CancelBeforeNext
    }
}