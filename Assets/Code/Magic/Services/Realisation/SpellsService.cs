using System.Collections.Generic;
using System.Linq;
using CrazyWizardNotes.Common;
using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Model.Events;
using UnityEngine;

namespace CrazyWizardNotes.Magic
{
    public class SpellsService : SingletonBehavior<SpellsService>, ISpellsService
    {
        private ISoundService SoundService { get; set; }

        protected override void Awake()
        {
            base.Awake();

            SoundService = Locate<ISoundService>();
        }

        protected void Start()
        {
            SoundService.OnSoundRecieved += OnSoundRecieved;
        }

        public void StartBending(ISpellBendable spell, Vector2 target)
        {
            var args = new SpellBendStartEventArgs()
            {
                Target = target
            };

            spell.OnStartBend(args);
        }

        public void UpdateBending(ISpellBendable spell, Vector2 target)
        {
            var args = new SpellBendUpdateEventArgs()
            {
                Target = target
            };

            spell.OnUpdateBend(args);
        }

        public void EndBending(ISpellBendable spell, Vector2 target)
        {
            var args = new SpellBendEndEventArgs()
            {
                Target = target
            };

            spell.OnEndBend(args);
        }

        [SerializeField]
        private Transform ParticlesParent;

        public ISubject CreateParticle(string particleName, Vector2 position, Quaternion? rotation = null)
        {
            var resource = Resources.Load("SpellParticles/" + particleName);
            var gameObject = Instantiate(resource, position, rotation ?? Quaternion.identity, ParticlesParent) as GameObject;
            var spellParticle = gameObject.GetComponent<ISubject>();

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

        private void OnSoundRecieved(OnSoundRecievedEventArgs args)
        {
        }
    }
}