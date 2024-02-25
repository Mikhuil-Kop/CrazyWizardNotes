using CrazyWizardNotes.Common;
using CrazyWizardNotes.Spells;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Objects
{
    public class ParticleObject : DefaultEnvironmentObject, ISpellParticle
    {
        protected override void LocateComponents(IRealisationProvider provider)
        {
            base.LocateComponents(provider);

            Debug.Log(gameObject);
        }
    }
}