using System;

namespace CrazyWizardNotes.Scenes.Abstractions
{
    [Serializable]
    public class SceneRuntimeData
    {
        public bool KeepField;
        public bool UseRules;
        public bool LastSceneIsCompleted;
    }
}