using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class VirtualActorsService : IVirtualActorsService, IHandlesStart, IHandlesUpdate, IHandlesFixedUpdate
    {
        private HashSet<IActor> Actors { get; }
        private CustomUpdateRunner CustomUpdateRunner { get; set; }

        public VirtualActorsService()
        {
            Actors = new HashSet<IActor>();
            CustomUpdateRunner = new CustomUpdateRunner(Actors.ActorsAs<IHandlesCustomUpdate>());
        }

        public void RegisterActor(IActor actor)
        {
            Actors.Add(actor);

            if (actor.Is<IHandlesCustomUpdate>())
            {
                CustomUpdateRunner = new CustomUpdateRunner(Actors.ActorsAs<IHandlesCustomUpdate>());
            }
        }

        public void RemoveActor(IActor actor)
        {
            Actors.Remove(actor);

            if (actor.Is<IHandlesCustomUpdate>())
            {
                CustomUpdateRunner = new CustomUpdateRunner(Actors.ActorsAs<IHandlesCustomUpdate>());
            }
        }

        public void OnStart()
        {
            foreach (var actor in Actors.ActorsAs<IHandlesStart>())
            {
                actor.OnStart();
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var actor in Actors.ActorsAs<IHandlesUpdate>())
            {
                actor.OnUpdate(deltaTime);
            }

            CustomUpdateRunner.Update(deltaTime);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            foreach (var actor in Actors.ActorsAs<IHandlesFixedUpdate>())
            {
                actor.OnFixedUpdate(fixedDeltaTime);
            }
        }
    }
}