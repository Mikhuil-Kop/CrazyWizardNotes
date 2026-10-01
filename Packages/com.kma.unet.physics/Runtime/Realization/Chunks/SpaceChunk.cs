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
        public List<IActor> ChunkActors { get; }
        IEnumerable<IActor> ISpaceChunk.ChunkActors => ChunkActors;

        public SpaceChunk(
            ISpaceChunks parentChunks,
            int chunkIndexX,
            int chunkIndexY,
            Vector2D chunkPosition,
            Vector2D chunkSize)
        {
            ParentChunks = parentChunks ?? throw new ArgumentNullException(nameof(parentChunks));
            ChunkIndexX = chunkIndexX;
            ChunkIndexY = chunkIndexY;
            ChunkPosition = chunkPosition;
            ChunkSize = chunkSize;
            ChunkActors = new List<IActor>();
        }
    }
}
