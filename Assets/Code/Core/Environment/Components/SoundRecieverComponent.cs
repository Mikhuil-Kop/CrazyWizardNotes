using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Model.Events;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components
{
    public class SoundRecieverComponent : ObjectComponentBase, ISoundReciever
    {
        private bool SoundRecieverActive { get; set; }

        private Vector2 SoundSourcePosition { get; set; }

        private event OnSoundRecievedEventHandler OnSoundRecieved;

        private void RecieveSound(ISoundSource source, string sound, float volume)
        {
            var args = new OnSoundRecievedEventArgs()
            {
                Source = source,
                Sound = sound,
                Volume = volume
            };

            OnSoundRecieved.Invoke(args);
        }


        #region Интерфейс

        bool ISoundReciever.SoundRecieverActive => SoundRecieverActive;

        Vector2 ISoundReciever.SoundSourcePosition => SoundSourcePosition;

        event OnSoundRecievedEventHandler ISoundReciever.OnSoundRecieved
        {
            add => OnSoundRecieved += value;
            remove => OnSoundRecieved -= value;
        }

        void ISoundReciever.RecieveSound(ISoundSource source, string sound, float volume)
            => RecieveSound(source, sound, volume);

        #endregion
    }
}