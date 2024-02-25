using UnityEngine;
using System.Linq;
using CrazyWizardNotes.Spells;
using CrazyWizardNotes.Spells.Logic;

namespace CrazyWizardNotes.Common.Services
{
    public class SpellLogicService : SingletonBehavior<SpellLogicService>
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

        public void ForceExplosion(Vector2 point, float radius, float impulse)
        {
            var colliders = Physics2D.OverlapCircleAll(point, radius);
            var objects = colliders.SelectMany(x => x.gameObject.GetComponents<MonoBehaviour>());

            foreach (var obj in objects.Where(x => x is IPhysical && x is ISpacePoint))
            {
                var direction = (obj as ISpacePoint).Position - point;
                var distance = direction.magnitude;
                var normal = direction / distance;

                (obj as IPhysical).ApplyImpulse((impulse / (distance * distance)) * normal);
            }
        }
    }
}