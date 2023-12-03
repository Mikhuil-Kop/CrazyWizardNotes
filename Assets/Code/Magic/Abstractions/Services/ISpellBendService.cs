using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Abstractions
{
    /// <summary>
    /// Управляет магическим потоком.
    /// </summary>
    public interface ISpellBendService
    {
        public void StartBend(ISpellBending owner, object key, SpacePoint bendStart);
        public void UpdateBend(ISpellBending owner, object key, SpacePoint bendDirection);
        public void EndBend(ISpellBending owner, object key);
        public void EndBend(ISpellBending owner);
    }
}