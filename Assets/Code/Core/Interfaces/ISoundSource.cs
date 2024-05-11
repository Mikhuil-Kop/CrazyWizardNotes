using UnityEngine;

namespace CrazyWizardNotes.Interfaces
{
    public interface ISoundSource
    {
        bool SoundSourceActive { get; }
        Vector2 SoundSourcePosition { get; }
    }
}
