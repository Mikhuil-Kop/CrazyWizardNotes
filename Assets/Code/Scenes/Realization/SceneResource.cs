using CrazyWizardNotes.Scenes.Abstractions;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Scenes.Realization
{
    [CreateAssetMenu(fileName = "SceneResource", menuName = "ScriptableObjects/SceneResource", order = 1)]
    public class SceneResource : ScriptableObject, ISceneResource
    {
        public string SceneName;
        public string SceneTitle;
        public SceneResourceData SceneData;

        string ISceneResource.SceneName => SceneName;
        object ISceneResource.SceneData => SceneData;
    }
}