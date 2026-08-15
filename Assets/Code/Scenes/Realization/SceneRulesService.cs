using CrazyWizardNotes.Scenes.Abstractions;
using System.Linq;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Scenes.Realization
{
    public class SceneRulesService : IHandlesUpdate, IHandlesSceneLoaded
    {
        private ISceneLoadService SceneLoadService { get; }
        private Transform Transform { get; }

        public SceneRulesService(ISceneLoadService sceneLoadService, Transform transform)
        {
            SceneLoadService = sceneLoadService;
            Transform = transform;
        }

        public void OnSceneLoaded(ISceneInfo sceneInfo, ISceneData sceneData)
        {
            var data1 = sceneData.GetResourceData<SceneResourceData>();
            var data2 = sceneData.GetPreloadData<SceneRuntimeData>();

            if (data2 == null || !data2.KeepField)
            {
                foreach (var t in Transform.GetChildren().ToArray())
                {
                    GameObject.Destroy(t.gameObject);
                }

                var field = GameObject.Instantiate(data1.Field, Transform);

                foreach (var t in field.transform.GetChildren().ToArray())
                {
                    t.parent = Transform;
                }

                GameObject.Destroy(field);
            }
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