namespace CrazyWizardNotes.CreaturesBehaviors.Abstractions
{
    public struct ChoiceValue
    {
        public NeedsValue? ShortTermIndividualNeeds { get; set; }
        public NeedsValue? ShortTermCollectiveNeeds { get; set; }
        public NeedsValue? LongTermIndividualNeeds { get; set; }
        public NeedsValue? LongTermCollectiveNeeds { get; set; }
    }
}
