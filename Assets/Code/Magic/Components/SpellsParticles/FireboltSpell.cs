//using CrazyWizardNotes.Common.Services;
//using CrazyWizardNotes.Interfaces;
//using CrazyWizardNotes.Spells.Logic;

//namespace CrazyWizardNotes.Spells.DefaultSpells
//{
//    public class FireboltSpell : ISpell
//    {
//        public void StartCast(float deltaTime)
//        {
//            var inputService = InputService.Instance;
//            var casterService = CasterService.Instance;
//            var particleService = SpellLogicService.Instance;
//            var shedulerService = SchedulerService.Instance;

//            var casterPostion = casterService.CasterPosition;
//            var casterTarget = inputService.Pointer;
//            var castDirection = (casterTarget - casterPostion).normalized;

//            var particle = particleService.CreateParticle("Firebolt", casterPostion);

//            particle.As<IPhysical>().ApplyImpulse(30 * castDirection);
//            particle.As<ITouches>().OnTouch += (e) => particle.As<IVanishable>().Vanish();
//            shedulerService.Shedule(5f, () => particle.As<IVanishable>().Vanish());
//        }

//        public void TickCust(float deltaTime)
//        {
//        }

//        public void EndCust(float deltaTime)
//        {
//        }
//    }
//}
