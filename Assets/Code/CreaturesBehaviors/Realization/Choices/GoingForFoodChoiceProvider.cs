using CrazyWizardNotes.CreaturesBehaviors.Abstractions;
using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public class GoingForFoodChoiceProvider : ICreatureChoiceProvider
    {
        public IEnumerable<ICreatureChoiceInfo> GetChoices()
        {
            throw new NotImplementedException();
        }
    }
}