using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace UNet.Physics.Realization
{
    public class SpaceChunkActorsFactory : ISpaceChunkActorsFactory
    {
        private ISpaceChunkActorFactory[] Factories { get; }

        public SpaceChunkActorsFactory(IEnumerable<ISpaceChunkActorFactory> factories)
        {
            Factories = factories?.ToArray() ?? throw new ArgumentNullException(nameof(factories));
        }

        public IEnumerable<IActor> CreateActorForChunk(ISpaceChunk spaceChunk)
        {
            return Factories.Select(x => x.CreateActorForChunk(spaceChunk));
        }
    }
}
