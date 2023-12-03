using UNet;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public interface IArcWhipVisualizer
    {
        void UpdateTarget(Vector2D distance, float minRadius, bool isActive);
    }
}
