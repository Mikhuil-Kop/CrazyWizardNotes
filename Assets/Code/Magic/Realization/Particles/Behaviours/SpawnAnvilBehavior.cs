using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public sealed class SpawnAnvilBehavior : CustomParticleBehaviourBase
    {
        private IPhysicsActorSpawnService SpawnService { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private Animator Animator { get; }

        public SpawnAnvilBehavior(IPhysicsActorSpawnService spawnService, IHasSpacePoint spacePoint, Animator animator)
        {
            SpawnService = spawnService;
            HasSpacePoint = spacePoint;
            Animator = animator;
        }

        public override void OnBendEnd(ICustomParticleBehaviorContext context)
        {
            context.IsBendEnabled = false;
            context.IsVanishRequested = true;

            SpawnService.SpawnActor("Magic/Spawns/Anvil", HasSpacePoint.SpacePoint);
        }
    }
}