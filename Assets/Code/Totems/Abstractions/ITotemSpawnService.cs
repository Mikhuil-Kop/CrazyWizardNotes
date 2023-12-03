using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Totems.Abstractions
{
    public interface ITotemSpawnService
    {
        ITotem SpawnTotem(string spellId, SpacePoint point);
    }
}
