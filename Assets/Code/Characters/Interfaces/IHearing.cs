using CrazyWizardNotes.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Characters
{
    /// <summary>
    /// Объект способен слышать звуки.
    /// </summary>
    public interface IHearing
    {
        Vector2 HearPosition { get; }

        void Hear(string sound);
    }
}