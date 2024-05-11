using UnityEngine;
using CrazyWizardNotes.Model.Events;

namespace CrazyWizardNotes.Interfaces
{
    public interface ISoundReciever
    {
        bool SoundRecieverActive { get; }
        Vector2 SoundSourcePosition { get; }
        event OnSoundRecievedEventHandler OnSoundRecieved;

        void RecieveSound(ISoundSource source, string sound, float volume);
    }
}
