using CrazyWizardNotes.Creatures.Abstractions;
using System;
using UNet.Animations.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public sealed partial class Creature : ICreature, ICanWalk, ICanCarry, ICanAttack, IHasName, IHasEmotion, IHandlesCustomUpdate, IHandlesFixedUpdate
    {
        private ILogger Logger { get; }
        private IPhysicsService PhysicsService { get; }

        private IPhysical Physical { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private IHasAdjustableMass HasAdjustableMass { get; }
        private IHasAnimationEvents HasAnimationEvents { get; }
        private IHasAnimationData<CreatureAnimationData> HasAnimationData { get; }
        private ICreatureVisualizer CreatureVisualizer { get; }

        private bool IsBusy => HasAnimationData.AnimationValue.IsBusy;
        public float AverageWalkSpeed => 1f;
        public string Name => throw new NotImplementedException();
        public Emotion Emotion => throw new NotImplementedException();

        public Creature(
            ILogger logger,
            IPhysicsService physicsService,
            IPhysical physical,
            IHasSpacePoint hasSpacePoint,
            IHasAdjustableMass hasAdjustableMass,
            IHasAnimationEvents hasAnimationEvents,
            IHasAnimationData<CreatureAnimationData> hasAnimationData,
            ICreatureVisualizer creatureVisualizer)
        {
            Logger = logger;
            PhysicsService = physicsService;
            Physical = physical;
            HasSpacePoint = hasSpacePoint;
            HasAdjustableMass = hasAdjustableMass;
            HasAnimationEvents = hasAnimationEvents;
            HasAnimationData = hasAnimationData;
            CreatureVisualizer = creatureVisualizer;

            InitializeEat();
        }

        public float OnCustomUpdateDelay => 1f;
        public void OnCustomUpdate()
        {
            OnRareUpdateEat(OnCustomUpdateDelay);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            Physical.ApplyBreak(1f, fixedDeltaTime);
        }

        public void WalkTo(SpacePoint spacePoint)
        {
            if (IsBusy)
            {
                return;
            }

            var direction = spacePoint.Position - HasSpacePoint.SpacePoint.Position;

            Physical.ApplyWalk(direction, 3f, AverageWalkSpeed);
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Carry(ICarrieble carry)
        {
            throw new NotImplementedException();
        }
    }
}