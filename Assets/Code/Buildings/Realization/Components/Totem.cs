using CrazyWizardNotes.Magic.Abstractions;
using UNet;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Buildings.Realization
{
    public class Totem : IHandlesStart, IHandlesDestroy
    {
        private ISpellParticleService SpellParticleService { get; }
        private IAsyncJobService AsyncJobService { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private TotemConfig Config { get; }
        private IRegistredJob RegistredJob { get; set; }

        public Totem(IHasSpacePoint hasSpacePoint, IAsyncJobService asyncJobService, ISpellParticleService spellParticleService, TotemConfig config)
        {
            SpellParticleService = spellParticleService;
            AsyncJobService = asyncJobService;
            HasSpacePoint = hasSpacePoint;
            Config = config;
        }

        public void OnStart()
        {
            RegistredJob = AsyncJobService.Register(SpawnParticle, Config.SpellSpawnDelay);
        }

        public void OnDestroy()
        {
            RegistredJob?.Dispose();
        }

        public void SpawnParticle()
        {
            SpellParticleService.SpawnParticle(Config.SpellId, HasSpacePoint.SpacePoint + Vector2D.Up);
        }
    }
}
