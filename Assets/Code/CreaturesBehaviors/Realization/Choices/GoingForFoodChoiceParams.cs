using CrazyWizardNotes.Creatures.Abstractions;
using UNet;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public sealed class GoingForFoodChoiceParams
    {
        public ServicesParams Services { get; internal set; }
        public CreatureParams Creature { get; internal set; }
        public TargetParams Target { get; internal set; }

        public class ServicesParams
        {
            public IPhysicsService PhysicsService { get; internal set; }
        }

        public class CreatureParams
        {
            public IActor Actor { get; internal set; }
            public ICanEat CanEat { get; internal set; }
            public ICanWalk CanWalk { get; internal set; }
            public IHasSpacePoint HasSpacePoint { get; internal set; }
        }

        public class TargetParams
        {
            public IActor Actor { get; internal set; }
            public IEatable Eatable { get; internal set; }
            public ICreature Creature { get; internal set; }
            public IClaimable Claimable { get; internal set; }
            public IDestroyable Destroyable { get; internal set; }
            public IHasSpacePoint HasSpacePoint { get; internal set; }
        }

        public Vector2D Distance => Services.PhysicsService.GetDistanceOrNull(Creature.HasSpacePoint, Target.HasSpacePoint) ?? Vector2D.PositiveInfinity;
        public float AverageWalkSpeed => Creature.CanWalk?.AverageWalkSpeed ?? 0f;
    }
}