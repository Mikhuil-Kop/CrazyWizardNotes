using CrazyWizardNotes.Creatures.Abstractions;
using UNet;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class NavigationService : INavigationService
    {
        private IPhysicsService PhysicsService { get; set; }

        public NavigationService(IPhysicsService physicsService)
        {
            PhysicsService = physicsService;
        }

        public Vector2D? GetDirection(SpacePoint from, SpacePoint to)
        {
            return PhysicsService.GetDistance(from, to).Normalized;
        }
    }
}