using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Elements.Realization
{

    [CreateAssetMenu(fileName = "MaterialConfig", menuName = "Configs/MaterialConfig", order = 1)]
    public class MaterialConfigScriptable : ScriptableObject, IResource<MaterialConfig>
    {
        public MaterialConfig MaterialConfig;

        public MaterialConfig Value => MaterialConfig;
    }
}
