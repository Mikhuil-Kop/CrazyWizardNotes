using System.Collections.Generic;
using System.Linq;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Magic;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class WizardControlService : SingletonBehavior<WizardControlService>
    {
        private ISpellsService SpellService { get; set; }
        private IInputService InputService { get; set; }

        private List<ISpellBendable> CurrentSpells { get; set; }


        protected override void Awake()
        {
            base.Awake();

            SpellService = Locate<ISpellsService>();
            InputService = Locate<IInputService>();

            CurrentSpells = new List<ISpellBendable>();
        }

        public void Update()
        {
            if (!InputService.GetButton("WandActive"))
            {
                //if (inputService.GetButtonDown("mouse"))
                //{
                //    foreach (var readable in inputService.PointedTargets.Select(x => x.As<IReadable>()).Where(x => x != null))
                //    {
                //        var eventParams = new EventParams()
                //        {
                //            Target1 = this,
                //            Target2 = readable,
                //            TextValue = readable.GetReadText()
                //        };

                //        eventService.RaiseEvent(EventType.WordReaded, eventParams);
                //        Debug.Log("RaisedSound " + readable.GetReadText());
                //    }

                //    Debug.Log("Sound");
                //}
            }
            else
            {
                if (InputService.GetButtonDown("mouse"))
                {
                    CurrentSpells = InputService.PointedTargets.SubjectsAs<ISpellBendable>().ToList();

                    foreach (var spell in CurrentSpells)
                    {
                        SpellService.StartBending(spell, InputService.Pointer);
                    }
                }

                if (InputService.GetButton("mouse"))
                {
                    foreach (var spell in CurrentSpells)
                    {
                        SpellService.UpdateBending(spell, InputService.Pointer);
                    }
                }

                if (InputService.GetButtonUp("mouse"))
                {
                    foreach (var spell in CurrentSpells)
                    {
                        SpellService.EndBending(spell, InputService.Pointer);
                    }
                }
            }
        }
    }
}