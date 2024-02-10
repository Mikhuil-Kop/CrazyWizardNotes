using System.Collections.Generic;
using UnityEngine;
using CrazyWizardNotes.Spells;

namespace CrazyWizardNotes.Common.Services
{
    public class SpellExecuteService : SingletonBehavior<SpellExecuteService>
    {
        private Queue<string> SpellWordsQueue { get; set; }
        private float LastUpdateTime { get; set; }

        private void Update()
        {
            UpdateSpellWords();
            UpdateSpellControl();
        }

        private void UpdateSpellWords()
        {
        }

        private void SetNewSpell(string spellText)
        {

        }

        private void UpdateSpellControl()
        {

        }
    }
}