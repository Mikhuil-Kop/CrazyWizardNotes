using CrazyWizardNotes.Creatures.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class GoingForCarryTask : ICreatureTask
    {
        private ICanEat CreatureCanEat { get; }
        private ICanWalk CreatureCanWalk { get; }
        private IHasSpacePoint CreaturePosition { get; }

        private IEatable TargetEatable { get; }
        private IHasSpacePoint TargetPosition { get; }
        private IClaimable TargetClaimable { get; }

        private IDisposable TargetClaim;

        private IPhysicsService PhysicsService { get; }


        public bool IsCompleted { get; private set; }

        public GoingForCarryTask(IActor creature, IActor target, IUServiceProvider serviceProvider)
        {
            CreatureCanEat = creature.As<ICanEat>();
            CreatureCanWalk = creature.As<ICanWalk>();
            CreaturePosition = creature.As<IHasSpacePoint>();

            TargetEatable = target.As<IEatable>();
            TargetPosition = target.As<IHasSpacePoint>();
            TargetClaimable = target.As<IClaimable>();

            PhysicsService = serviceProvider.GetService<IPhysicsService>();

            IsCompleted = false;
        }

        public void OnTaskStart()
        {
            var isClaimed = TargetClaimable?.TryClaim("", this, out TargetClaim) ?? true;

            IsCompleted = !isClaimed;
        }

        public void OnTaskRareUpdate()
        {
        }

        public void OnTaskUpdate(float deltaTime)
        {
            if (CreatureCanEat == null || CreaturePosition == null || TargetEatable == null || TargetPosition == null)
            {
                return;
            }

            if (PhysicsService.GetDistance(CreaturePosition, TargetPosition).Magnitude < 0.5f)
            {
                CreatureCanEat.Eat(TargetEatable, deltaTime);

                if (TargetEatable.CaloriesLeft == 0f)
                {
                    IsCompleted = true;
                }

                if (CreatureCanEat.EatingIsFull)
                {
                    IsCompleted = true;
                }
            }
        }

        public void OnTaskFixedUpdate(float deltaTime)
        {
            if (CreatureCanWalk == null || TargetPosition == null)
            {
                return;
            }

            CreatureCanWalk.WalkTo(TargetPosition.SpacePoint);
        }

        public void OnTaskEnd()
        {
            TargetClaim?.Dispose();
        }
    }
}