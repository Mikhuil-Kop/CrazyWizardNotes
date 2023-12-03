using UNet;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace Assets.Code
{
    public class Canvas : IHandlesStart
    {
        private IPhysicsService PhysicsService { get; }

        public Canvas(IPhysicsService physicsService)
        {
            PhysicsService = physicsService;
        }

        public void OnStart()
        {
            PhysicsService.GetDefaultSpacePoint(default).ParentSpace.SpaceChunks.Resize(new[]
            {
                new Vector2D(-16, -9),
                new Vector2D(16, 9)
            });
        }
    }
}
