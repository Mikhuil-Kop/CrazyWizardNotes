using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Common.Services
{
    public interface ISoundService
    {
        void MakeSound(ISoundSource source, string sound, float volume);
    }
}