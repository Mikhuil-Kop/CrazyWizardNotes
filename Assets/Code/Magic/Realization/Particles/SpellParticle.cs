using CrazyWizardNotes.Magic.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    public class SpellPointParticle : ISpellParticle, ISpellBendable, IHandlesFixedUpdate
    {
        private IPhysicsService PhysicsService { get; }
        private ISpacePoint SpacePoint { get; }
        private IPhysical Physical { get; }

        public void OnStartBend(SpellBendStartEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdateBend(SpellBendUpdateEventArgs args)
        {
            var force = PhysicsService.GetDistance(args.TargetPoint, SpacePoint);

            Physical.ApplyForce(force);
        }

        public void OnEndBend(SpellBendEndEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
