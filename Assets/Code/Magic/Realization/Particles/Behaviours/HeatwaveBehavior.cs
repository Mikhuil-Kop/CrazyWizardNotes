using CrazyWizardNotes.Elements.Abstractions;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public class HeatwaveBehavior : LazerBehaviorBase
    {
        public HeatwaveBehavior(IPhysicsService physicsService, ILazerVisualizer visualizer, IHasSpacePoint spacePoint, IPhysical physical)
            : base(physicsService, visualizer, spacePoint, physical)
        {
        }

        protected override void OnLazer(ICustomParticleBehaviorContext context, IEnumerable<IActor> actors)
        {
            foreach (var actor in actors.WithComponents<IHasTemperature, IFlammable>())
            {
                actor.Component1?.AddThermalEnergy(Level * 3f * context.DeltaTime);
                actor.Component2?.ApplySpark(Level / 10f);
            }
        }
    }
}
