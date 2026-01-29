using CrazyWizardNotes.Magic.Abstractions;
using System.Linq;
using UNet.Core.Abstractions;
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
                CreateParticle(s, args.Source.SoundSourcePosition);
            }
        }

        private void CreateParticle(string spellWord, ISpacePoint point)
        {
            SpawnService.SpawnActor("Magic/Particle", point, x =>
            {
                x.ServiceProvider.Add("SpellWord", spellWord);
            });
        }
    }
}
