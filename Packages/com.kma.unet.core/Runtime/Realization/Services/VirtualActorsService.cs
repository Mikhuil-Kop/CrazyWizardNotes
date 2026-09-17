using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class VirtualActorsService : IVirtualActorsService
    {
        private HashSet<IActor> Actors { get; }

        public IActor AddActor(Func<IActor> actor)
        {
            throw new NotImplementedException();
        }

        public void RemoveActor(IActor actor)
        {
            throw new NotImplementedException();
        }
    }
}