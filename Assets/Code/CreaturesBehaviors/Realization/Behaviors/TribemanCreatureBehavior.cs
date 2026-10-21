using CrazyWizardNotes.CreaturesBehaviors.Abstractions;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public class TribemanCreatureBehavior : CreatureBehaviorBase
    {
        private IUServiceProvider ServiceProvider { get; }
        private IActor Actor { get; }
        private IEnumerable<ICreatureChoiceProvider> CreatureChoiceProviders { get; }

        public TribemanCreatureBehavior(IUServiceProvider serviceProvider, IActor actor)
        {
            ServiceProvider = serviceProvider;
            Actor = actor;

            CreatureChoiceProviders = new ICreatureChoiceProvider[]
            {
                new GoingForFoodChoiceProvider(ServiceProvider, Actor)
            };
        }

        protected override IEnumerable<ICreatureChoiceProvider> GetChoiceProviders()
        {
            return CreatureChoiceProviders;
        }

        protected override float ValueToFloat(ChoiceValue choice)
        {
            if (choice.NeedsValues.Length == 0)
            {
                return 0f;
            }

            return choice.NeedsValues.Sum(NeedsValueToFloat);
        }

        private float NeedsValueToFloat(NeedsValue value)
        {

        }
    }
}
