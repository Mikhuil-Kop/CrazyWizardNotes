using CrazyWizardNotes.CreaturesBehaviors.Abstractions;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public class GoingForFoodChoiceInfo : ICreatureChoiceInfo
    {
        private IActor Creature { get; }
        private IActor Target { get; }
        private IUServiceProvider ServiceProvider { get; }

        public GoingForFoodChoiceInfo(IActor creature, IActor target, IUServiceProvider serviceProvider)
        {
            Creature = creature;
            Target = target;
            ServiceProvider = serviceProvider;
        }

        public ChoiceValue GetChoiceValue()
        {
            throw new System.NotImplementedException();
        }

        public ICreatureChoiceTask GetChoiceTask()
        {
            return new GoingForFoodChoiceTask(Creature, Target, ServiceProvider);
        }
    }
}