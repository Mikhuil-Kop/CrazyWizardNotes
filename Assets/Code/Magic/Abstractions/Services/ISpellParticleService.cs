using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Abstractions
{
    /// <summary>
    /// Управляет частицей.
    /// </summary>
    public interface ISpellParticleService
    {
        public ISpellParticle SpawnParticle(string particleName, SpacePoint point, float lifetime = -1);
    }
}