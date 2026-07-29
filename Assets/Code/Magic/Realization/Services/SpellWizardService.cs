using Assets.Code.Magic.Abstractions.Services;
using CrazyWizardNotes.Magic.Abstractions;
using System.Linq;
using UNet.Sound.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    public class SpellWizardService : ISpellWizardService
    {
        private ISpellParticleService SpellParticleService { get; }


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
