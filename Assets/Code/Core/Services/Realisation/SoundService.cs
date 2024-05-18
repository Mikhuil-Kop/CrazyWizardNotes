using System.Collections.Generic;
using CrazyWizardNotes.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    [RequireComponent(typeof(ISubjectsCollectionService))]
    public class SoundService : SingletonBehavior<ISoundService>, ISoundService
    {
        private ISubjectsCollectionService SubjectsCollectionService { get; set; }

        protected override void Awake()
        {
            base.Awake();

            SubjectsCollectionService = Locate<ISubjectsCollectionService>();
        }

        public void MakeSound(ISoundSource source, string sound, float volume)
        {
            var recievers = SubjectsCollectionService.Get<ISoundReciever>();

            foreach (var reciever in recievers)
            {
                reciever.RecieveSound(source, sound, volume);
            }
        }
    }
}