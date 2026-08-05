using CrazyWizardNotes.Magic.Abstractions;
using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    public class SpellParticleService : ISpellParticleService
    {
        private IPhysicsActorSpawnService SpawnService { get; }
        private static Dictionary<string, Type> SpellTypes { get; } = new()
        {
            //{ "Fireball", typeof(FireballParticle) }
        };

        public SpellParticleService(IPhysicsActorSpawnService physicsActorSpawnService)
        {
            SpawnService = physicsActorSpawnService;

            foreach (var spellType in SpellTypes.Values)
            {
                if (spellType.IsAssignableFrom(typeof(ISpellParticle)))
                {
                    throw new Exception($"SpellTypes must contain only ISpellParticles. {spellType} is not.");
                }
            }
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
                throw new Exception("Actor must contains ISpellParticleComponent.");
            }

            return actor;
        }

        //public IActor SpawnParticle(string particleName, SpacePoint point, float lifetime = -1)
        //{
        //    if (particleName == null)
        //    {
        //        throw new ArgumentNullException(nameof(particleName));
        //    }

        //    var spellType = SpellTypes.GetValueOrDefault(particleName)
        //        ?? throw new ArgumentException($"{particleName} is not registered.", nameof(particleName));

        //    var actor = SpawnService.SpawnActor("Magic/Particle", point, x =>
        //    {
        //        x.ServiceProvider.Add(typeof(ISpellParticle), null, spellType);
        //        x.ComponentsProvider.AddComponent<ISpellParticle>();
        //    });

        //    if (actor.IsNot<ISpellParticle>())
        //    {
        //        throw new Exception("Actor must contains ISpellParticleComponent.");
        //    }

        //    return actor;
        //}
    }
}
