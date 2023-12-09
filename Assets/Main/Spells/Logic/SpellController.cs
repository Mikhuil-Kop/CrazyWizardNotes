using System;
using System.Linq;
using UnityEngine;

namespace CrazyWizardNotes.Spells.Logic
{
    public class SpellController : MonoBehaviour
    {
        [TextArea]
        public string SpellString;

        private ISpell Spell;


        public void Awake()
        {
            var nodes = SpellString.Split('\n')
                .Select(x => x.Split(' ', ',', ';')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim() + "Operator")
                    .Select(x => Activator.CreateInstance(Type.GetType("CrazyWizardNotes.Spells.Logic.SpellOperators." + x)))
                    .Cast<ISpellOperator>()
                    .Select(x => new SpellNode(x))
                    .Cast<ISpellNode>());

            Spell = new Spell(nodes);
        }

        public void Update()
        {
            if (Spell == null)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Spell.StartCast(Time.deltaTime);
            }

            if (Input.GetMouseButton(0))
            {
                Spell.TickCust(Time.deltaTime);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Spell.EndCust(Time.deltaTime);
            }
        }

        public void SetSpell(ISpell spell)
        {
            Spell = spell;
        }
    }
}