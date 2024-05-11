using System;
using UnityEngine;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Environment.Components;

namespace CrazyWizardNotes.Characters
{
    [RequireComponent(typeof(ISpacePoint))]
    [RequireComponent(typeof(IBehaviorController))]
    public class PersonComponent : ObjectComponentBase, IIntelegent, ITalking, IThinking, IHearing
    {
        private ISpacePoint SpacePoint { get; set; }
        private IBehaviorController BehaviorController { get; set; }

        protected override void Start()
        {
            base.Start();

            SpacePoint = Locate<ISpacePoint>();
            BehaviorController = Locate<IBehaviorController>();
        }

        #region Реализация интерфейсов

        Vector2 IHearing.HearPosition => SpacePoint.Position;

        string IThinking.GetCurrentThoughtDialog()
        {
            return "My thought";
        }

        void IHearing.Hear(string sound)
        {
            Debug.Log($"{this.Parent} hears \"{sound}\"");
        }

        #endregion
    }
}