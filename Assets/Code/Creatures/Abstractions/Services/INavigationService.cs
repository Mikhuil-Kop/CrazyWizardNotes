using UNet;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Abstractions
{
    public interface INavigationService
    {
        public Vector2D? GetDirection(SpacePoint from, SpacePoint to);
    }

    public interface IClaimService
    {

    }

   
}
