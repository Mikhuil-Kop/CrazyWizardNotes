using UNet.Physics.Abstractions;

namespace UNet.Sound.Abstractions
{
    public interface ISoundReceiver
    {
        bool SoundRecieverActive { get; }
        ISpacePoint SoundRecieverPosition { get; }
        void RecieveSound(ISoundSource source, string sound, float volume);
        event OnSoundRecievedEventHandler OnSoundRecieved;
    }
}
