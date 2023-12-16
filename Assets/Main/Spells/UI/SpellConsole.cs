using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace CrazyWizardNotes.Spells.UI
{
    public class SpellConsole : MonoBehaviour
    {
        public TMP_InputField Text;
        public Button SaveButton;

        public void Awake()
        {
            SaveButton.onClick.AddListener(UpdateSpell);
        }

        public void UpdateSpell()
        {
            var text = Text.text.Trim();
            Spells.Controllers.SpellController.Instance.SetSpell(text);
        }
    }
}