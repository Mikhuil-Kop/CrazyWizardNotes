using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Scenes.Realization
{
    public class SceneLoadAnimator : IHandlesSceneLoaded, IHandlesNextSceneLoading
    {
        private SceneLoadAnimatorConfig Config { get; }
        private bool Started { get; set; }
        private float Timer { get; set; }

        public SceneLoadAnimator(SceneLoadAnimatorConfig config)
        {
            Config = config;

            Started = false;
            Timer = 0f;
        }

        public void OnNextSceneLoading(ISceneLoadingContext context)
        {
            if (!Started)
            {
                Started = true;
                Config.Animator.Play("Loading_Fade");
            }

            Config.Slider.normalizedValue = context.SceneLoadingProcess.Progress;

            Timer += UnityEngine.Time.deltaTime;

            if (Timer <= 0.5f)
            {
                context.PreventOpenNextScene = true;
            }
        }

        public void OnSceneLoaded(ISceneInfo sceneInfo, ISceneData sceneData)
        {
            Config.Animator.Play("Loading_Unfade");
            Config.Slider.normalizedValue = 1;
        }
    }
}