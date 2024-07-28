using UnityEngine;

namespace UNet.Animations.Realization
{
    public class AnimationStateBehavior : StateMachineBehaviour
    {
        public string StateName;
        private AnimationHandler AnimationHandler { get; set; }

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineEnter(animator, stateMachinePathHash);

            AnimationHandler = animator.gameObject.GetComponent<AnimationHandler>();

            if (AnimationHandler == null)
            {
                AnimationHandler = animator.gameObject.AddComponent<AnimationHandler>();
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            var context = new AnimationContext
            {
                AnimationStateName = StateName
            };

            AnimationHandler?.RaiseOnAnimationStart(context);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            var context = new AnimationContext
            {
                AnimationStateName = StateName
            };

            AnimationHandler?.RaiseOnAnimationEnd(context);
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineExit(animator, stateMachinePathHash);

            var context = new AnimationContext
            {
                AnimationStateName = StateName
            };

            AnimationHandler?.RaiseOnAnimationEnd(context);
        }
    }
}