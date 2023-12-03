using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Creatures.Abstractions
{
    public interface ICanEat
    {
        public float CaloriesPerBite { get; }
        public bool EatingIsFull { get; }

        public void StartEat(IActor target);
    }
}
