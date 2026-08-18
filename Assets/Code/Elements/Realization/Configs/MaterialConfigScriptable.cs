using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Elements.Realization
{

    [CreateAssetMenu(fileName = "MaterialConfig", menuName = "ScriptableObjects/MaterialConfig", order = 1)]
    public class MaterialConfigScriptable : ScriptableObject, IResource<MaterialConfig>
    {
        public MaterialConfig MaterialConfig;

        public MaterialConfig Value => MaterialConfig;
    }
}
