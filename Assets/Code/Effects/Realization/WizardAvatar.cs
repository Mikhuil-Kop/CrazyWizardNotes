using CrazyWizardNotes.Effects.Abstractions;
using System;

namespace CrazyWizardNotes.Effects.Realization
{
    public class EffectsService : IEffectsService
    {
        public void SpawnEffect(IEffectBehavior effect)
        {
            throw new NotImplementedException();
        }
    }

    public class EffectPlacementContext : IEffectPlacementContext
    {

    }

    public class EffectBehavior : IEffectBehavior
    {
        public void ExecutePlacement(IEffectPlacementContext context)
        {
            throw new NotImplementedException();
        }
    }
}
