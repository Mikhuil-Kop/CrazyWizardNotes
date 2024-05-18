using System;
using System.Linq;
using UnityEngine;
using CrazyWizardNotes.Spells.Logic;
using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Common.Services
{
    public class WizardControlService : SingletonBehavior<WizardControlService>
    {
        public void Update()
        {
            var inputService = InputService.Instance;
            var eventService = EventsService.Instance;

            if (!inputService.GetButton("WandActive"))
            {
                if (inputService.GetButtonDown("mouse"))
                {
                    foreach (var readable in inputService.PointedTargets.Select(x => x.As<IReadable>()).Where(x => x != null))
                    {
                        var eventParams = new EventParams()
                        {
                            Target1 = this,
                            Target2 = readable,
                            TextValue = readable.GetReadText()
                        };

                        eventService.RaiseEvent(EventType.WordReaded, eventParams);
                        Debug.Log("RaisedSound " + readable.GetReadText());
                    }

                    Debug.Log("Sound");
                }
            }
            else
            {
                if (inputService.GetButtonDown("mouse"))
                {
                    Debug.Log("WizardControlService - StartCasting");

                    //SpellExecuteService.Instance.
                    //Spell.StartCast(Time.deltaTime);
                }

                if (inputService.GetButton("mouse"))
                {
                    Debug.Log("WizardControlService - TickCasting");

                    //Spell.TickCust(Time.deltaTime);
                }

                if (inputService.GetButtonUp("mouse"))
                {
                    Debug.Log("WizardControlService - EndCasting");

                    //Spell.EndCust(Time.deltaTime);
                }
            }
        }

        private ISpell Spell = new Spells.DefaultSpells.PushSpell();
    }
}