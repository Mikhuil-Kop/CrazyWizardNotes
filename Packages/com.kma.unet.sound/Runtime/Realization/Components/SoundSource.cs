using UNet.Physics.Abstractions;
using UNet.Sound.Abstractions;

namespace UNet.Sound.Realization
{
    public class SoundSource : ISoundSource
    {
        private IHasSpacePoint HasSpacePoint { get; }
        public ISpacePoint SoundSourcePosition => HasSpacePoint?.SpacePoint;
        public bool SoundSourceActive => true;

        public SoundSource(IHasSpacePoint hasSpacePoint)
        {
            HasSpacePoint = hasSpacePoint;
        }
    }
}
