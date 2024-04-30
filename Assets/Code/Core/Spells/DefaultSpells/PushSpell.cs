using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Spells.Logic;

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

            particle.As<IPhysical>().ApplyImpulse(2f * castDirection);
            particle.As<ITouches>().OnTouch += (e) => particle.As<IVanishable>()?.Vanish();
            shedulerService.Shedule(0.3f, () => particle.As<IVanishable>()?.Vanish());

            particle.As<IVanishable>().OnVanish += () =>
            {
                var pos = particle.As<ISpacePoint>().Position;
                spellLogicService.ForceExplosion(pos, 5, 10);
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
