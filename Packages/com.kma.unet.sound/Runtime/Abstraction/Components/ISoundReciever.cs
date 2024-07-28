namespace UNet.Sound.Abstractions
{
    public interface ISoundReciever
    {
        bool SoundRecieverActive { get; }
        Vector2D SoundSourcePosition { get; }
        void RecieveSound(ISoundSource source, string sound, float volume);
        event OnSoundRecievedEventHandler OnSoundRecieved;
    }
}
