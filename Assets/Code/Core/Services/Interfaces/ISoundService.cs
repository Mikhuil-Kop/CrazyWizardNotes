using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Model.Events;

namespace CrazyWizardNotes.Common.Services
{
    public interface ISoundService
    {
        event OnSoundRecievedEventHandler OnSoundRecieved;

        void MakeSound(ISoundSource source, string sound, float volume);
    }
}