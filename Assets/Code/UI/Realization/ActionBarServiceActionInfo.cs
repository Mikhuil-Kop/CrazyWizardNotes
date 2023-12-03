using UnityEngine;

namespace CrazyWizardNotes.UI.Realization
{
    [CreateAssetMenu(fileName = "new Action UI Info", menuName = "ScriptableObjects/Action UI Info", order = 1)]
    public class ActionBarServiceActionInfo : ScriptableObject
    {
        public Sprite Icon;
        public string Title;
        public string Description;
    }
}
