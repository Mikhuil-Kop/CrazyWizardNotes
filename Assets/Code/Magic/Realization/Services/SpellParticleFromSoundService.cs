using CrazyWizardNotes.Magic.Abstractions;
using System.Linq;
using UNet.Physics.Abstractions;
using UNet.Sound.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    public class SpellParticleFromSoundService
    {
        private ISpellParticleService SpellParticleService { get; }
        private ISoundService SoundService { get; }

        public SpellParticleFromSoundService(ISpellParticleService spellParticleService, ISoundService soundService, IPhysicsActorSpawnService physicsActorSpawnService)
        {
            SpellParticleService = spellParticleService;
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
                SpellParticleService.SpawnParticle(s, args.Source.SoundSourcePosition);
            }
        }
    }
}
