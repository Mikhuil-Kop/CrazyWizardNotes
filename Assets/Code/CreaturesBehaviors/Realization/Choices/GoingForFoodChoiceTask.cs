using CrazyWizardNotes.CreaturesBehaviors.Abstractions;
using System;
using static CrazyWizardNotes.CreaturesBehaviors.Realization.CreatureChoiceTools;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public sealed class GoingForFoodChoiceTask : ICreatureChoiceTask
    {
        private GoingForFoodChoiceParams Params { get; }
        private IDisposable TargetClaim;
        private bool IsClaimed;

        public GoingForFoodChoiceTask(GoingForFoodChoiceParams param)
        {
            Params = param;
        }

        public bool IsCompleted =>
               !IsClaimed
            || (Params.Target.Destroyable?.IsDestroyed ?? false)
            || (Params.Target.Eatable?.CaloriesLeft ?? 0f) <= 0f
            || (Params.Creature.CanEat?.EatingIsFull ?? true);

        public void OnTaskStart()
        {
            IsClaimed = Params.Target.Claimable?.TryClaim("", Params.Creature, out TargetClaim) ?? true;
        }

        public void OnTaskUpdate(float deltaTime)
        {
            if (Params.Distance.Magnitude < 0.5f)
            {
                TryEat(Params.Creature.CanEat, Params.Target.Actor);
            }
        }

        public void OnTaskFixedUpdate(float deltaTime)
        {
            TryWalk(Params.Creature.CanWalk, Params.Target.HasSpacePoint);
        }

        public void OnTaskEnd()
        {
            TargetClaim?.Dispose();
        }

        public void Dispose()
        {
            TargetClaim?.Dispose();
        }
    }
}