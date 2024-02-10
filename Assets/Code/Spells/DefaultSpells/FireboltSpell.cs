using System;
using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Spells.Logic;

namespace CrazyWizardNotes.Spells.DefaultSpells
{
    public class FireboltSpell : ISpell
    {
        public void StartCast(float deltaTime)
        {
            var inputService = InputService.Instance;
            var casterService = CasterService.Instance;
            var particleService = SpellParticleService.Instance;
            var shedulerService = SchedulerService.Instance;

            var casterPostion = casterService.CasterPosition;
            var casterTarget = inputService.Pointer;
            var castDirection = (casterTarget - casterPostion).normalized;

            var particle = particleService.CreateParticle("Firebolt", casterPostion);

            (particle as IPhysical)?.ApplyImpulse(30 * castDirection);
            (particle as ITouches).OnTouch += () => (particle as IVanishable)?.Vanish();
            shedulerService.Shedule(5f, () => (particle as IVanishable)?.Vanish());
        }

        public void TickCust(float deltaTime)
        {
        }

        public void EndCust(float deltaTime)
        {
        }
    }
}
