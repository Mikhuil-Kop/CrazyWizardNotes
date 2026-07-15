using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class CreatureWrapper : TypedWrapperBase
    {
        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new[]
            {
                typeof(UNet.Core.Realization.Destroyable),
                typeof(UNet.Physics.Realization.PhysicalObject),
                typeof(Creatures.Realization.Creature),
                typeof(Creatures.Realization.CreatureBehaviorHandler),
                typeof(Creatures.Realization.TribemanCreatureBehavior),
            };
        }
    }
}
