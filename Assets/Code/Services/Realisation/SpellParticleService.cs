using UnityEngine;
using CrazyWizardNotes.Spells;

namespace CrazyWizardNotes.Common.Services
{
    public class SpellParticleService : SingletonBehavior<SpellParticleService>
    {
        [SerializeField]
        private Transform ParticlesParent;

        public ISpellParticle CreateParticle(string particleName, Vector2 position, Quaternion? rotation = null)
        {
            var resource = Resources.Load("SpellParticles/" + particleName);
            var gameObject = Instantiate(resource, position, rotation ?? Quaternion.identity, ParticlesParent) as GameObject;
            var spellParticle = gameObject.GetComponent<ISpellParticle>();

            return spellParticle;
        }
    }
}