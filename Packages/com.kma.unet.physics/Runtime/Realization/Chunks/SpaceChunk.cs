using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace UNet.Physics.Realization
{
    public sealed class SpaceChunk : ISpaceChunk
    {
        public ISpaceChunks ParentChunks { get; }
        public int ChunkIndexX { get; }
        public int ChunkIndexY { get; }
        public Vector2D ChunkPosition { get; }
        public Vector2D ChunkSize { get; }
        public IActor SpaceActor { get; }

        [Obsolete("Не используется. Не доработано.")]
        public IEnumerable<IActor> ActorsInside => Array.Empty<IActor>();

        public SpaceChunk(
            ISpaceChunks parentChunks,
            int chunkIndexX,
            int chunkIndexY,
            Vector2D chunkPosition,
            Vector2D chunkSize,
            IActor spaceActor)
        {
            ParentChunks = parentChunks ?? throw new ArgumentNullException(nameof(parentChunks));
            ChunkIndexX = chunkIndexX;
            ChunkIndexY = chunkIndexY;
            ChunkPosition = chunkPosition;
            ChunkSize = chunkSize;
            SpaceActor = spaceActor ?? throw new ArgumentNullException(nameof(spaceActor));
        }
    }
}
