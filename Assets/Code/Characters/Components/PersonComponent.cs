using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Environment.Components;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Model.Events;
using CrazyWizardNotes.Services;
using UnityEngine;

namespace CrazyWizardNotes.Characters
{
    [RequireComponent(typeof(ISpacePoint))]
    [RequireComponent(typeof(ISoundSource))]
    [RequireComponent(typeof(ISoundReciever))]
    [RequireComponent(typeof(IBehaviorController))]
    public class PersonComponent : ObjectComponentBase, IIntelegent, IThinking
    {
        private ISpacePoint SpacePoint { get; set; }
        private ISoundSource SoundSource { get; set; }
        private ISoundReciever SoundReciever { get; set; }
        private ISoundService SoundService { get; set; }
        private IBehaviorController BehaviorController { get; set; }


        protected override void Start()
        {
            base.Start();

            SpacePoint = Locate<ISpacePoint>();
            SoundSource = Locate<ISoundSource>();
            SoundReciever = Locate<ISoundReciever>();
            BehaviorController = Locate<IBehaviorController>();

            SoundService = Common.Services.SoundService.Instance;

            SoundReciever.OnSoundRecieved += SoundReciever_OnSoundRecieved;
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SoundService.MakeSound(SoundSource, "Q", 1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                SoundService.MakeSound(SoundSource, "W", 1);
            }
        }

        private void SoundReciever_OnSoundRecieved(OnSoundRecievedEventArgs args)
        {
            Debug.Log($"{args.Source} hears \"{args.Sound}\"");
        }

        #region Реализация интерфейсов

        string IThinking.GetCurrentThoughtDialog()
        {
            return "My thought";
        }

        #endregion
    }
}