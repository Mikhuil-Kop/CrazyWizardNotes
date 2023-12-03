using CrazyWizardNotes.CreaturesBehaviors.Abstractions;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public sealed class GoingForFoodChoiceInfo : ICreatureChoiceInfo
    {
        private GoingForFoodChoiceParams Params { get; }

        public GoingForFoodChoiceInfo(GoingForFoodChoiceParams param)
        {
            Params = param;
        }

        public ICreatureChoiceTask GetChoiceTask()
        {
            return new GoingForFoodChoiceTask(Params);
        }

        public ChoiceValue GetChoiceValue()
        {
            return new NeedsValue()
            {
                SelfishnessMult = 1f,
                TimeDelayMod = Params.Distance.Magnitude / Params.AverageWalkSpeed,
                Food = Params.Target.Eatable?.CaloriesLeft ?? 0f,
            };
        }
    }
}