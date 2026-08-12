using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Scenes.Realization
{
    public class SceneRulesService : IHandlesUpdate
    {
        private ISceneLoadService SceneLoadService { get; }

        public SceneRulesService(ISceneLoadService sceneLoadService)
        {
            SceneLoadService = sceneLoadService;
        }

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneLoadService.LoadScene("Test1", null);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneLoadService.LoadScene("Test2", null);
            }
        }
    }
}