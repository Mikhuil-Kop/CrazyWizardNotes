using CrazyWizardNotes.Scenes.Abstractions;
using System;
using System.Linq;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Scenes.Realization
{
    public class MenuSceneRulesService : IHandlesSceneLoaded
    {
        private ISceneLoadResourceService SceneLoadService { get; }
        private IAsyncJobService AsyncJobService { get; }
        private IUServiceProvider ServiceProvider { get; }
        private Transform Transform { get; }
        private ISceneRule SceneRule { get; set; }
        private bool RuleStarted { get; set; }

        public MenuSceneRulesService(ISceneLoadResourceService sceneLoadService, IAsyncJobService asyncJobService, IUServiceProvider serviceProvider, Transform transform)
        {
            SceneLoadService = sceneLoadService;
            AsyncJobService = asyncJobService;
            ServiceProvider = serviceProvider;
            Transform = transform;

            AsyncJobService.Register(OnTick, 1f);
        }

        public void OnSceneLoaded(ISceneInfo sceneInfo, ISceneData sceneData)
        {
            var data1 = sceneData.GetResourceData<SceneResourceData>();
            var data2 = sceneData.GetPreloadData<SceneRuntimeData>();

            if (data2 == null || !data2.KeepField)
            {
                SpawnSceneField(data1);
            }

            if (data2 == null || data2.UseRules)
            {
                InitializeRules(data1);
            }
        }

        private void OnTick()
        {
            if (SceneRule != null)
            {
                if (!RuleStarted)
                {
                    RuleStarted = true;
                    SceneRule.OnStart(ServiceProvider);
                }

                SceneRule.OnTick();

                if (SceneRule.IsCompleted)
                {
                    FinishScene(true);
                    return;
                }

                if (SceneRule.IsFailed)
                {
                    FinishScene(false);
                    return;
                }
            }
        }

        private void FinishScene(bool isCompleted)
        {
            Debug.Log("FinishScene - " + isCompleted);
            
            SceneLoadService.LoadScene("Main/MainMenu", new SceneRuntimeData()
            {
                KeepField = false,
                UseRules = true,
                LastSceneIsCompleted = isCompleted
            });
        }

        private void SpawnSceneField(SceneResourceData data1)
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
        
        private void InitializeRules(SceneResourceData data1)
        {
            var name = $"CrazyWizardNotes.Scenes.Realization.{data1.RuleType.name}";
            var type = ByName(name) ?? throw new Exception($"Cannot find type \"{name}\"");
            var constructor = type.GetConstructors().FirstOrDefault() ?? throw new Exception($"Cannot find constructor for \"{type}\"");
            var rule = constructor.Invoke(new object[0]) as ISceneRule ?? throw new Exception($"{type} is not a ISceneRule");

            SceneRule = rule;
        }

        public static Type ByName(string name)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Reverse())
            {
                var tt = assembly.GetType(name);
                if (tt != null)
                {
                    return tt;
                }
            }

            return null;
        }
    }
}