using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Spells.Logic;
using UnityEngine;

namespace CrazyWizardNotes.Spells.DefaultSpells
{
    public class GravityWellSpell : ISpell
    {
        private Vector2 StartPoint;

        public void StartCast(float deltaTime)
        {
            var inputService = InputService.Instance;

            StartPoint = inputService.Pointer;
        }

        public void TickCust(float deltaTime)
        {
        }

        public void EndCust(float deltaTime)
        {
            Debug.Log("123");
            var inputService = InputService.Instance;
            var particleService = SpellLogicService.Instance;
            var shedulerService = SchedulerService.Instance;

            var startPoint = StartPoint;
            var endPoint = inputService.Pointer;

            var particle = particleService.CreateParticle("GravityWell", (startPoint + endPoint) / 2, Quaternion.FromToRotation(Vector2.up, (endPoint - startPoint)));
            shedulerService.Shedule(15f, () => particle.As<IVanishable>()?.Vanish());
        }
    }
}
