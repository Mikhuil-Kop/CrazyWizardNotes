using CrazyWizardNotes.Magic.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UNet.Core.Abstractions;
using UNet.Inputs.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization
{
    public class SpellBarService : ISpellBarService, IHandlesUpdate
    {
        #region Поля

        private IInputService InputService { get; }
        private RectTransform RectTransform { get; }
        private Dictionary<string, SpellBarItem> Spells { get; } = new Dictionary<string, SpellBarItem>();

        #endregion

        #region Перечисление

        public byte Count => (byte)Spells.Count;
        public ISpellBarItem this[string spellName] => Spells[spellName];
        public IEnumerator<KeyValuePair<string, ISpellBarItem>> GetEnumerator() => Spells.Select(x => new KeyValuePair<string, ISpellBarItem>(x.Key, x.Value)).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Функционал

        public ISpellBarItem AddSpell(string spellName)
        {
            var currentSpell = Spells[spellName];

            if (currentSpell == null)
            {
                currentSpell = Spells[spellName] = new SpellBarItem(this, RectTransform);

                currentSpell.SpellName = spellName;
                currentSpell.SpellDescription = spellName;
                currentSpell.SpellHotkey = spellName;
                currentSpell.SpellName = spellName;
            }

            return currentSpell;
        }

        public void RemoveSpell(string spellName)
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Элементы

        public class SpellBarItem : ISpellBarItem
        {
            private SpellBarService SpellBarService { get; }
            private RectTransform SpellRectTransform { get; }
            private RectTransform ParentRectTransform { get; }
            private TextMeshPro SpellTitleText { get; }
            private TextMeshPro SpellHotkeyText { get; }

            private string _spellName;
            private string _spellTitle;
            private string _spellDescription;
            private string _spellHotkey;
            private bool _isEnabled;

            public SpellBarItem(SpellBarService spellBarService, RectTransform spellRectTransform)
            {
                SpellBarService = spellBarService;
                SpellRectTransform = spellRectTransform;
                ParentRectTransform = spellRectTransform.parent as RectTransform;

                SpellTitleText = spellRectTransform.Find("Title").GetComponent<TextMeshPro>();
                SpellHotkeyText = spellRectTransform.Find("Hotkey").GetComponent<TextMeshPro>();
            }

            public string SpellName
            {
                get => _spellName;
                set
                {
                    _spellName = value;
                }
            }

            public string SpellTitle
            {
                get => _spellTitle;
                set
                {
                    _spellTitle = value;
                    SpellTitleText.text = value;
                }
            }

            public string SpellDescription
            {
                get => _spellDescription;
                set
                {
                    _spellDescription = value;
                }
            }

            public string SpellHotkey
            {
                get => _spellHotkey;
                set
                {
                    _spellHotkey = value;
                    SpellHotkeyText.text = value;
                }
            }

            public bool IsEnabled
            {
                get => _isEnabled;
                set
                {
                    _isEnabled = value;
                }
            }
        }

        #endregion
    }
}
