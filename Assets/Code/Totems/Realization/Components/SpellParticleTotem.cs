using CrazyWizardNotes.Magic.Abstractions;
using CrazyWizardNotes.Totems.Abstractions;
using UNet;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Totems.Realization
{
    public class SpellParticleTotem : TotemBase, ISpellParticleTotem, IHandlesStart
    {
        private ISpellParticleService SpellParticleService { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private SpellParticleTotemConfig Config { get; }
        public string SpellId => Config.SpellId;

        public SpellParticleTotem(IActor actor, ITotemVisualizer totemVisualizer, IHasSpacePoint hasSpacePoint, ISpellParticleService spellParticleService, SpellParticleTotemConfig config) : base(actor, totemVisualizer)
        {
            SpellParticleService = spellParticleService;
            HasSpacePoint = hasSpacePoint;
            Config = config;

            OnActivated += SpellParticleTotem_OnActivated;
        }

        public void OnStart()
        {
            IsEnabled = Config.IsEnabled;
            IsActive = Config.IsActive;
            ActivationTimeGap = System.TimeSpan.FromSeconds(Config.ActivationTimeGap);
        }

        private void SpellParticleTotem_OnActivated(TotemActivationEventArgs args)
        {
            SpellParticleService.SpawnParticle(Config.SpellId, HasSpacePoint.SpacePoint + Vector2D.Up);
        }
    }
}
