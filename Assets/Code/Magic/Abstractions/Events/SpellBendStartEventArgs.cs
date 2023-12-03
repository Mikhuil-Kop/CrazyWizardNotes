using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Abstractions
{
    public delegate void SpellBendStartEventHandler(SpellBendStartEventArgs args);

    public class SpellBendStartEventArgs
    {
        public SpacePoint StartPoint { get; set; }
    }
}