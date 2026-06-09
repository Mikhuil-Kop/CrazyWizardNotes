using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Abstractions
{
    public delegate void SpellBendUpdateEventHandler(SpellBendUpdateEventArgs args);

    public class SpellBendUpdateEventArgs
    {
        public SpacePoint TargetPoint { get; set; }
    }
}