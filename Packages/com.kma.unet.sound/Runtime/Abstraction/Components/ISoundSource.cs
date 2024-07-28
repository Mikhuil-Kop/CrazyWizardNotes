namespace UNet.Sound.Abstractions
{
    public interface ISoundSource
    {
        bool SoundSourceActive { get; }
        Vector2D SoundSourcePosition { get; }
    }
}
