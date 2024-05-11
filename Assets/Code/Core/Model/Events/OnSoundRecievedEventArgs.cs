using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Model.Events
{
    public delegate void OnSoundRecievedEventHandler(OnSoundRecievedEventArgs args);

    public class OnSoundRecievedEventArgs
    {
        public ISoundSource Source { get; set; }
        public string Sound { get; set; }
        public float Volume { get; set; }
    }
}