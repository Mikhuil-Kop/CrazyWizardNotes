using CrazyWizardNotes.Scenes.Abstractions;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Scenes.Realization
{
    public class SceneRulesService : IHandlesUpdate, IHandlesSceneLoaded
    {
        private ISceneLoadService SceneLoadService { get; }

        public SceneRulesService(ISceneLoadService sceneLoadService)
        {
            SceneLoadService = sceneLoadService;
        }

        public void OnSceneLoaded(ISceneInfo sceneInfo, ISceneData sceneData)
        {
            var data1 = sceneData.GetResourceData<SceneResourceData>();
            var data2 = sceneData.GetPreloadData<SceneRuntimeData>();
        }

        // Test
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