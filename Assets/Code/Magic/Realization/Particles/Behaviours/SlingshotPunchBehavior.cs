using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public sealed class SlingshotPunchBehavior : SlingshotBehaviorBase
    {
        private ITouches Touches { get; }

        public SlingshotPunchBehavior(IPhysicsService physicsService, ISlingshotVisualizer visualizer, IHasSpacePoint spacePoint, IPhysical physical, ITouches touches)
            : base(physicsService, visualizer, spacePoint, physical)
        {
            Touches = touches;

            Touches.OnTouch += Touches_OnTouch;
        }

        protected override void OnSlingRelease(ICustomParticleBehaviorContext context)
        {
        }

        private void Touches_OnTouch(OnTouchEventArgs obj)
        {
        }
    }
}
