namespace UNet.Core.Abstractions
{
    public enum ServiceScopeType : byte
    {
        Application = 0,
        Scene = 1,
        Actor = 2,
        Component = 3,
        Transient = 4,
    }
}