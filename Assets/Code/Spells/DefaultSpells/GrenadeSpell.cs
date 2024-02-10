using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Spells.Logic;

namespace CrazyWizardNotes.Spells.DefaultSpells
{
    public class GrenadeSpell : ISpell
    {
        private float Force { get; set; }

        public void StartCast(float deltaTime)
        {
            Force = 0;
        }

        public void TickCust(float deltaTime)
        {
            if (Force > 3)
            {
                Force = 3;
            }
            else
            {
                Force += deltaTime;
            }
        }

        public void EndCust(float deltaTime)
        {
            var inputService = InputService.Instance;
            var casterService = CasterService.Instance;
            var particleService = SpellParticleService.Instance;
            var shedulerService = SchedulerService.Instance;

            var casterPostion = casterService.CasterPosition;
            var casterTarget = inputService.Pointer;
            var castDirection = (casterTarget - casterPostion).normalized;

            var particle = particleService.CreateParticle("FireGrenade", casterPostion);

            (particle as IPhysical)?.ApplyImpulse((20 * Force + 10)* castDirection);
            shedulerService.Shedule(5f, () => (particle as IExplosive)?.Explode());
        }
    }
}
