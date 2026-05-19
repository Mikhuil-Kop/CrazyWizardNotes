namespace CrazyWizardNotes.Effects.Abstractions
{
    public interface IEffectsService
    {
        void SpawnEffect(IEffectBehavior effect);
    }

    public interface IEffectPlacementContext
    {

    }

    public interface IEffectBehavior
    {
        void ExecutePlacement(IEffectPlacementContext context);
    }
}
