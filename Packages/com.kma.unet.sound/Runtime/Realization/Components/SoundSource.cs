using System;
using UNet.Physics.Abstractions;
using UNet.Sound.Abstractions;

namespace UNet.Sound.Realization
{
    public class SoundSource : ISoundSource
    {
        public bool SoundSourceActive => true;
        public ISpacePoint SoundSourcePosition => throw new NotImplementedException();
    }

    public class SoundReceiver : ISoundReceiver
    {
        public bool SoundRecieverActive => true;
        public ISpacePoint SoundRecieverPosition => throw new NotImplementedException();

        public event OnSoundRecievedEventHandler OnSoundRecieved;

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
