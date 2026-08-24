using CrazyWizardNotes.Magic.Abstractions;
using CrazyWizardNotes.Totems.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Buildings.Realization
{
    public class AntimagicTotem : TotemBase, IHandlesStart
    {
        private ISpellParticleService SpellParticleService { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private AntimagicTotemConfig Config { get; }

        public AntimagicTotem(IActor actor, ITotemVisualizer totemVisualizer, IHasSpacePoint hasSpacePoint, ISpellParticleService spellParticleService, AntimagicTotemConfig config) : base(actor, totemVisualizer)
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
            ActivationTimeGap = System.TimeSpan.FromSeconds(3);
        }

        private void SpellParticleTotem_OnActivated(TotemActivationEventArgs args)
        {
            //SpellParticleService.SpawnParticle(Config.SpellId, HasSpacePoint.SpacePoint + Vector2D.Up);
        }
    }
}
