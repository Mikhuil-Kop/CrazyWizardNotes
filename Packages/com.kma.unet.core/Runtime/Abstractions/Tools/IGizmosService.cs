namespace UNet.Core.Abstractions
{
    public interface IGizmosService
    {
        void DrawLine(Vector2D start, Vector2D end);
        void DrawPoint(Vector2D point);
    }
}