namespace UNet.Physics.Abstractions
{
    public interface IPhysical
    {
        float Mass { get; }
        Vector2D ForceSummary { get; }
        Vector2D SpeedSummary { get; }
        void ApplyForce(Vector2D force);
        void ApplyImpulse(Vector2D force);
    }
}
