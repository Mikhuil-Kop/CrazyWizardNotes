using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Model.Events;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components
{
    public class SoundSourceComponent : ObjectComponentBase, ISoundSource
    {
        public bool SoundSourceActive { get; set; }

        public Vector2 SoundSourcePosition { get; set; }


        #region Интерфейс

        bool ISoundSource.SoundSourceActive => SoundSourceActive;

        Vector2 ISoundSource.SoundSourcePosition => SoundSourcePosition;

        #endregion
    }
}