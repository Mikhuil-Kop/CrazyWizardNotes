using System.Collections.Generic;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Model.Events;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    [RequireComponent(typeof(ISubjectsCollectionService))]
    public class SoundService : SingletonBehavior<ISoundService>, ISoundService
    {
        private ISubjectsCollectionService SubjectsCollectionService { get; set; }

        public event OnSoundRecievedEventHandler OnSoundRecieved;

        protected override void Awake()
        {
            base.Awake();

            SubjectsCollectionService = Locate<ISubjectsCollectionService>();
        }

        public void MakeSound(ISoundSource source, string sound, float volume)
        {
            var recievers = SubjectsCollectionService.Get<ISoundReciever>();

            var args = new OnSoundRecievedEventArgs()
            {
                Sound = sound,
                Volume = volume,
                Source = source
            };

            OnSoundRecieved?.Invoke(args);

            foreach (var reciever in recievers)
            {
                reciever.RecieveSound(source, sound, volume);
            }
        }
    }
}