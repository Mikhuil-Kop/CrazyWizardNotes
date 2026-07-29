using CrazyWizardNotes.Magic.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Physics.Abstractions;
using UNet.Sound.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    public class SpellParticleService : ISpellParticleService
    {
        private IPhysicsActorSpawnService SpawnService { get; }
        private IPhysicsService PhysicsService { get; }
        private ISoundService SoundService { get; }

        public SpellParticleService(IPhysicsService physicsService, ISoundService soundService, IPhysicsActorSpawnService physicsActorSpawnService)
        {
            SpawnService = physicsActorSpawnService;
            PhysicsService = physicsService;
            SoundService = soundService;

            SoundService.OnSoundRecieved += SoundService_OnSoundRecieved;
        }

        private void SoundService_OnSoundRecieved(OnSoundRecievedEventArgs args)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"#{(?'SpellWord'.*?)}");

            var matches = regex.Matches(args.Sound);
            var spellWords = matches.Select(match => match.Groups["SpellWord"].Value).ToList();

            foreach (var s in spellWords)
            {
                SpawnParticle(s, args.Source.SoundSourcePosition);
            }
        }

        public ISpellParticle SpawnParticle(string particleName, SpacePoint point, float lifetime = -1)
        {
            var spellType = GetSpellParticleType(particleName);

            var actor = SpawnService.SpawnActor("Magic/Particle", point, x =>
            {
                x.ServiceProvider.Add(typeof(ISpellParticle), null, spellType);
            });

            return actor.As<ISpellParticle>();
        }

        private static Dictionary<string, Type> SpellTypes { get; } = new()
        {
            { "", null }
        };

        private static Type GetSpellParticleType(string particleName)
        {
            return SpellTypes[particleName];
        }
    }
}
