using CrazyWizardNotes.Model;

namespace CrazyWizardNotes.Common.Services
{
    public class SceneManagerService : SingletonBehavior<ISceneManagerService>, ISceneManagerService
    {
        public SceneParams CurrentSceneParams => throw new System.NotImplementedException();

        public void OpenScene(string sceneName, SceneParams sceneParams)
        {

        }
    }
}