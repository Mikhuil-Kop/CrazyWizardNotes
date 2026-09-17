using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class MainMenuStaticWrapper : TypedWrapperBase
    {
        public Camera Camera;
        public TextMeshProUGUI ActiveLog;

        public UNet.UI.Realization.TooltipUIMonoBehaviour TooltipUIMonoBehaviour;
        public UNet.UI.Realization.TooltipCanvasMonoBehaviour TooltipCanvasMonoBehaviour;
        public Scenes.Realization.SceneLoadAnimatorConfig SceneLoadAnimatorConfig;
        public Scenes.Realization.SceneResource SceneResource;

        protected override IEnumerable<object> GetObjects()
        {
            return new object[]
            {
                Camera,
                ActiveLog,
                TooltipUIMonoBehaviour,
                TooltipCanvasMonoBehaviour,
                SceneLoadAnimatorConfig,

                new UNet.Core.Realization.SceneLoadServiceConfig()
                {
                    TestSceneName = SceneResource?.SceneName,
                    TestSceneData1 = SceneResource?.SceneData,
                    TestSceneData2 = null
                }
            };
        }

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new Type[]
            {
                typeof(UNet.Core.Realization.ActorsCollectionService),
                typeof(UNet.Core.Realization.AsyncExecutionService),
                typeof(UNet.Core.Realization.AsyncJobService),
                typeof(UNet.Core.Realization.CameraService),
                typeof(UNet.Core.Realization.DebugGizmosService),
                typeof(UNet.Core.Realization.EventsService),
                typeof(UNet.Core.Realization.DebugLogger),
                typeof(UNet.Core.Realization.ResourcesService),
                typeof(UNet.Core.Realization.SceneLoadService),
                typeof(UNet.Core.Realization.SceneLoadResourceService),

                typeof(UNet.Inputs.Realization.CameraInputService),

                typeof(UNet.UI.Realization.LocalizationService),
                //typeof(UNet.UI.Realization.TooltipService), // Работает, но надо включить позже

                typeof(Scenes.Realization.SceneLoadAnimator),
                typeof(Scenes.Realization.MenuSceneRulesService),
                
                typeof(UI.Realization.ActiveLogger),
            };
        }
    }
}
