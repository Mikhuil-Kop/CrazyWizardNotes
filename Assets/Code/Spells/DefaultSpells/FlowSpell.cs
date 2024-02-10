using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Spells.Logic;
using UnityEngine;

namespace CrazyWizardNotes.Spells.DefaultSpells
{
    public class FlowSpell : ISpell
    {
        private float Timer { get; set; }
        private float TimeToCast { get; set; }

        public void StartCast(float deltaTime)
        {
            TimeToCast = 0.1f;
            Timer = 0;
        }

        public void TickCust(float deltaTime)
        {
            var inputService = InputService.Instance;
            var casterService = CasterService.Instance;
            var particleService = SpellParticleService.Instance;
            var shedulerService = SchedulerService.Instance;

            Timer -= deltaTime;

            while (Timer <= 0)
            {
                Timer += TimeToCast;

                var casterPostion = casterService.CasterPosition;
                var casterTarget = inputService.Pointer + new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                var castDirection = (casterTarget - casterPostion).normalized;

                var particle = particleService.CreateParticle("Fireflow", casterPostion);

                (particle as IPhysical)?.ApplyImpulse(1f * castDirection);
                shedulerService.Shedule(0.3f, () => (particle as IExplosive)?.Explode());
            }
        }

        public void EndCust(float deltaTime)
        {
        }
    }
}
