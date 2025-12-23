using UNet.Physics.Abstractions;

namespace UNet.Sound.Abstractions
{
    public interface ISoundSource
    {
        bool SoundSourceActive { get; }
        ISpacePoint SoundSourcePosition { get; }
    }
}
