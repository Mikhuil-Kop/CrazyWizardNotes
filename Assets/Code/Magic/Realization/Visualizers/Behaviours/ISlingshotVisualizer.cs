using UNet;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public interface ISlingshotVisualizer
    {
        bool IsEnabled { get; set; }
        bool IsActive { get; set; }
        bool IsReleased { get; set; }
        int Level { get; set; }

        void UpdateTarget(Vector2D pull);
    }
}
