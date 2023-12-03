namespace UNet.Sound.Abstractions
{
    public interface ISoundService
    {
        event OnSoundRecievedEventHandler OnSoundRecieved;

        void MakeSound(ISoundSource source, string sound, float volume);
    }

    public static class SoundServiceExtensions
    {
        //public static void AddCircleGravity(this ISoundService service)
        //{
        //}
    }
}