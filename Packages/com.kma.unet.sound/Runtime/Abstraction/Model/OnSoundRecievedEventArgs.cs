namespace UNet.Sound.Abstractions
{
    public delegate void OnSoundRecievedEventHandler(OnSoundRecievedEventArgs args);

    public class OnSoundRecievedEventArgs
    {
        public ISoundSource Source { get; set; }
        public string Sound { get; set; }
        public float Volume { get; set; }
        public float Distance { get; set; }
    }
}