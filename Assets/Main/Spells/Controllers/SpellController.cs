using System;
using System.Linq;
using UnityEngine;
using CrazyWizardNotes.Spells.Logic;

namespace CrazyWizardNotes.Spells.Controllers
{
    public class SpellController : Common.SingletonBehavior<SpellController>
    {
        [TextArea]
        public string SpellString;

        private ISpell Spell;


        protected override void Awake()
        {
            base.Awake();

            Spell = new Spell(SpellString);
        }

        public void Update()
        {
            if (Spell == null)
            {
                return;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Spell.StartCast(Time.deltaTime);
            }

            if (Input.GetMouseButton(1))
            {
                Spell.TickCust(Time.deltaTime);
            }

            if (Input.GetMouseButtonUp(1))
            {
                Spell.EndCust(Time.deltaTime);
            }
        }

        public void SetSpell(string spellString)
        {
            Spell?.EndCust(Time.deltaTime);

            SpellString = spellString;
            Spell = new Spell(spellString);
        }
    }
}