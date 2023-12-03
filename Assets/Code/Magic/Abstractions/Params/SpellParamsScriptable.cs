using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Abstractions
{
    // Протекшая абстракция: используется как заклинаниями, так и тотемами. Не хорошо.
    [CreateAssetMenu(fileName = "SpellParams", menuName = "Params/SpellParams", order = 1)]
    public class SpellParamsScriptable : ScriptableObject, IResource<SpellParams>
    {
        public Color Color = Color.white;
        public string DefaultSpellName => name;
        public string DefaultSpellTitle => $"@Spells.{name}_Title";
        public string DefaultSpellDescription => $"@Spells.{name}_Description";
        public string DefaultSpellParticleType => $"CrazyWizardNotes.Magic.Realization.Particles.{name}Behavior";
        public string DefaultSpellParticleAsset => $"Particles/{name}";
        public string DefaultSpellTotemAsset => $"Totems/{name}_Totem";

        public SpellParams Value => new()
        {
            SpellName = DefaultSpellName,
            SpellTitle = DefaultSpellTitle,
            SpellDescription = DefaultSpellDescription,
        };
    }
}
