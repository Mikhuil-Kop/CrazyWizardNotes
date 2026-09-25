using CrazyWizardNotes.Magic.Abstractions;
using System;
using System.Collections.Generic;
using UNet;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class ParticleWrapper : TypedWrapperBase
    {
        public SpellParamsScriptable SpellParams;

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new Type[]
            {
                //typeof(UNet.Core.Realization.Destroyable),
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Magic.Realization.CustomSpellParticle),
                typeof(Magic.Realization.CustomSpellParticleAnimatior),
                typeof(DestroyableByAnimation),
                GetParticleType()
            };
        }

        private Type GetParticleType()
        {
            return ReflectionExtension.GetTypeByNameRequired(SpellParams.DefaultSpellParticleType, true);
        }
    }
}
