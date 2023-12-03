namespace CrazyWizardNotes.CreaturesBehaviors.Abstractions
{
    public struct ChoiceValue
    {
        public NeedsValue[] NeedsValues { get; set; }

        public static implicit operator ChoiceValue(NeedsValue d) => new() { NeedsValues = new[] { d } };
    }
}
