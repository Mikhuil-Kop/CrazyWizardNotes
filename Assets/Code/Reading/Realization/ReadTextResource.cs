using UnityEngine;

namespace CrazyWizardNotes.Reading.Realization
{
    [CreateAssetMenu(menuName = "Resources/Read Text Resource", fileName = "New Read Text Resource")]
    public class ReadTextResource : ScriptableObject
    {
        public ReadTextItemResource[] Items;
    }
}
