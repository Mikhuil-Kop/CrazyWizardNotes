using UnityEngine;
using UnityEngine.Audio;

namespace CrazyWizardNotes.Main
{
    public sealed class AnimationSoundBehaviour : StateMachineBehaviour
    {
        [SerializeField] private AudioResource resource;
        [SerializeField] private float timeOffset = 0f;
        [SerializeField] private float volumeMult = 1f;
        private AudioSource source;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            source = animator.GetComponent<AudioSource>();
            
            if (source == null || resource == null)
            {
                Debug.LogWarning($"AnimationSoundBehaviour: Missing AudioSource or AudioResource on {animator.gameObject.name}");
                return;
            }

            source.resource = resource;
            source.time = timeOffset;
            source.volume = volumeMult;
            source.Play();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (source != null && source.isPlaying)
            {
                source.Stop();
            }
        }
    }
}
