using CrazyWizardNotes.Magic.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    public class SpellParticleService : ISpellParticleService
    {
        private IAsyncJobService AsyncJobService { get; }
        private IPhysicsActorSpawnService SpawnService { get; }

        public SpellParticleService(IAsyncJobService asyncJobService, IPhysicsActorSpawnService physicsActorSpawnService)
        {
            AsyncJobService = asyncJobService;
            SpawnService = physicsActorSpawnService;
        }

        public IActor SpawnParticle(string particleName, SpacePoint point, float lifetime = -1)
        {
            if (particleName == null)
            {
                throw new ArgumentNullException(nameof(particleName));
            }

            var actor = SpawnService.SpawnActor("Magic/Particles/" + particleName, point);

            if (actor.IsNot<ISpellParticle>())
            {
                throw new Exception("Actor must contains ISpellParticle component.");
            }

            if (lifetime > 0)
            {
                AsyncJobService.ExecuteDelayed(() => actor.As<IVanishable>()?.Vanish(), lifetime);
            }

            return actor;
        }
    }
}
