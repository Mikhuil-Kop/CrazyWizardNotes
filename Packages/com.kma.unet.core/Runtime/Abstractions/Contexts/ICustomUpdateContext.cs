namespace UNet.Core.Abstractions
{
    public interface ICustomUpdateContext
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }

        float NextExecutionDelay { get; set; }
        string NextExecutionGroup { get; set; }
    }
}