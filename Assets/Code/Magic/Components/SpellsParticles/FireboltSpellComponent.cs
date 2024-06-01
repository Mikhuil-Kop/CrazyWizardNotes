using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Magic
{
    [RequireComponent(typeof(ISpacePoint))]
    [RequireComponent(typeof(IPhysical))]
    public class FireboltSpellComponent : SpellParticleComponentBase, ISpellBendable
    {
        private IPhysicsService PhysicsService { get; set; }

        private ISpacePoint SpacePoint { get; set; }

        private IPhysical Physical { get; set; }

        private bool Controled { get; set; }


        protected void Awake()
        {
            SpacePoint = Locate<ISpacePoint>();
            Physical = Locate<IPhysical>();
            PhysicsService = Common.Services.PhysicsService.Instance;
        }

        protected void FixedUpdate()
        {
            if (Physical.SpeedSummary.magnitude > 0.3f)
            {
                Physical.ApplyForce(-0.9f * Physical.ForceSummary);
            }

            if (Controled)
            {
                PhysicsService.AddCircleGravity(SpacePoint.Position, 8f);
            }
        }

        public void OnStartBend(SpellBendStartEventArgs args)
        {
            Controled = true;
        }

        public void OnUpdateBend(SpellBendUpdateEventArgs args)
        {

        }

        public void OnFixedUpdateBend(SpellBendUpdateEventArgs args)
        {
            var direction = (args.Target - SpacePoint.Position);

            Physical.ApplyForce(direction * direction.magnitude);
        }

        public void OnEndBend(SpellBendEndEventArgs args)
        {

        }
    }
}