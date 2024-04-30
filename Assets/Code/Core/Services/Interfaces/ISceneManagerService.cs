using CrazyWizardNotes.Model;

namespace CrazyWizardNotes.Common.Services
{
    public interface ISceneManagerService
    {
        SceneParams CurrentSceneParams { get; }

        void OpenScene(string sceneName, SceneParams sceneParams);
    }
}