using UnityEngine;
using CrazyWizardNotes.Spells.Logic;
using CrazyWizardNotes.Common.Services;

namespace CrazyWizardNotes.Spells.DefaultSpells
{
    public class PushSpell : ISpell
    {
        public void StartCast(float deltaTime)
        {
            var inputService = InputService.Instance;
            var casterService = CasterService.Instance;
            var spellLogicService = SpellLogicService.Instance;
            var shedulerService = SchedulerService.Instance;

            var casterPostion = casterService.CasterPosition;
            var casterTarget = inputService.Pointer;
            var castDirection = (casterTarget - casterPostion).normalized;

            var particle = spellLogicService.CreateParticle("Pushwave", casterPostion);

            (particle as IPhysical).ApplyImpulse(2 * castDirection);
            (particle as ITouches).OnTouch += (e) => (particle as IVanishable)?.Vanish();
            shedulerService.Shedule(0.5f, () => (particle as IVanishable)?.Vanish());

            (particle as IVanishable).OnVanish += () =>
            {
                var pos = (particle as ISpacePoint).Position;
                spellLogicService.ForceExplosion(pos, 3, 10);
            };
        }

        public void TickCust(float deltaTime)
        {
        }

        public void EndCust(float deltaTime)
        {
        }
    }
}
