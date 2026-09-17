using UNet;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public interface ILazerVisualizer
    {
        bool IsEnabled { get; set; }
        bool IsActive { get; set; }
        int Level { get; set; }

        void UpdateTarget(Vector2D distance);
    }
}
