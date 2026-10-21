namespace CrazyWizardNotes.Creatures.Abstractions
{
    public interface IEatable
    {
        public float CaloriesMax { get; }
        public float CaloriesLeft { get; }

        public float EatThis(ICanEat digester);
    }
}
