using CrazyWizardNotes.CreaturesBehaviors.Abstractions;
using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public class TribemanCreatureBehavior : CreatureBehaviorBase
    {
        protected override IEnumerable<ICreatureChoiceProvider> GetChoiceProviders()
        {
            throw new NotImplementedException();
        }

        protected override float ValueToFloat(ChoiceValue choice)
        {
            throw new NotImplementedException();
        }
    }
}
