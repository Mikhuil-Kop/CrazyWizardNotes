using UNet.Physics.Abstractions;
using UNet.Sound.Abstractions;

namespace UNet.Sound.Realization
{
    public class SoundReceiver : ISoundReceiver
    {
        private IHasSpacePoint HasSpacePoint { get; }
        public SpacePoint SoundRecieverPosition => HasSpacePoint.SpacePoint;
        public bool SoundRecieverActive => true;

        public event OnSoundRecievedEventHandler OnSoundRecieved;

        public SoundReceiver(IHasSpacePoint hasSpacePoint)
        {
            HasSpacePoint = hasSpacePoint;
        }

        public void RecieveSound(ISoundSource source, string sound, float volume)
        {
            OnSoundRecieved?.Invoke(new OnSoundRecievedEventArgs()
            {
                Source = source,
                Sound = sound,
                Volume = volume
            });
        }
    }
}
