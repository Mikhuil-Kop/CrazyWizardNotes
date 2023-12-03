using System;
using System.Collections;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace UNet.Physics.Realization
{
    // ИИ
    public sealed class SpaceChunks : ISpaceChunks
    {
        public ISpaceContainer ParentContainer { get; }

        private IVirtualActorsService ActorsService { get; }
        private ISpaceChunkActorsFactory ActorsFactory { get; }
        private Vector2D WorldOrigin { get; }
        private Vector2D ChunkSize { get; }

        private ISpaceChunk[,] Chunks { get; set; }
        private int MinChunkX { get; set; }
        private int MinChunkY { get; set; }

        public SpaceChunks(IVirtualActorsService actorsService, ISpaceChunkActorsFactory actorsFactory, ISpaceContainer parentContainer, Vector2D worldOrigin, Vector2D chunkSize)
        {
            ActorsService = actorsService ?? throw new ArgumentNullException(nameof(actorsService));
            ActorsFactory = actorsFactory ?? throw new ArgumentNullException(nameof(actorsFactory));
            ParentContainer = parentContainer ?? throw new ArgumentNullException(nameof(parentContainer));

            WorldOrigin = worldOrigin;
            ChunkSize = chunkSize;

            Chunks = new ISpaceChunk[0, 0];
            MinChunkX = 0;
            MinChunkY = 0;
        }

        public int ChunksCountX => Chunks.GetLength(0);
        public int ChunksCountY => Chunks.GetLength(1);
        public int MinX => MinChunkX;
        public int MaxX => MinChunkX + ChunksCountX - 1;
        public int MinY => MinChunkY;
        public int MaxY => MinChunkY + ChunksCountY - 1;

        public ISpaceChunk this[int x, int y]
        {
            get
            {
                var ix = x - MinChunkX;
                var iy = y - MinChunkY;

                if (ix < 0 || ix >= ChunksCountX || iy < 0 || iy >= ChunksCountY)
                {
                    return null;
                }

                return Chunks[ix, iy];
            }
        }

        public ISpaceChunk this[Vector2D position]
        {
            get
            {
                var (x, y) = PositionToIndex(position);
                return this[x, y];
            }
        }

        public void Resize(IEnumerable<Vector2D> positions)
        {
            if (positions == null)
            {
                throw new ArgumentNullException(nameof(positions));
            }

            var reqMinX = int.MaxValue;
            var reqMaxX = int.MinValue;
            var reqMinY = int.MaxValue;
            var reqMaxY = int.MinValue;
            var hasPositions = false;

            foreach (var pos in positions)
            {
                var (cx, cy) = PositionToIndex(pos);

                if (cx < reqMinX) reqMinX = cx;
                if (cx > reqMaxX) reqMaxX = cx;
                if (cy < reqMinY) reqMinY = cy;
                if (cy > reqMaxY) reqMaxY = cy;

                hasPositions = true;
            }

            // Нечего добавлять
            if (!hasPositions)
            {
                return;
            }

            // Объединяем с уже существующим диапазоном (никогда не сжимаем)
            if (ChunksCountX > 0)
            {
                reqMinX = Math.Min(reqMinX, MinChunkX);
                reqMaxX = Math.Max(reqMaxX, MinChunkX + ChunksCountX - 1);
                reqMinY = Math.Min(reqMinY, MinChunkY);
                reqMaxY = Math.Max(reqMaxY, MinChunkY + ChunksCountY - 1);
            }

            // Уже всё покрыто — ничего не делаем
            if (ChunksCountX > 0 &&
                reqMinX >= MinChunkX &&
                reqMaxX <= MinChunkX + ChunksCountX - 1 &&
                reqMinY >= MinChunkY &&
                reqMaxY <= MinChunkY + ChunksCountY - 1)
            {
                return;
            }

            // Нужно расширять
            var newCountX = reqMaxX - reqMinX + 1;
            var newCountY = reqMaxY - reqMinY + 1;
            var newChunks = new ISpaceChunk[newCountX, newCountY];

            // Переносим старые чанки (их логические индексы не меняются)
            for (var y = 0; y < ChunksCountY; y++)
            {
                for (var x = 0; x < ChunksCountX; x++)
                {
                    var logicalX = MinChunkX + x;
                    var logicalY = MinChunkY + y;

                    var newX = logicalX - reqMinX;
                    var newY = logicalY - reqMinY;

                    newChunks[newX, newY] = Chunks[x, y];
                }
            }

            // Создаём недостающие чанки
            for (var y = 0; y < newCountY; y++)
            {
                for (var x = 0; x < newCountX; x++)
                {
                    if (newChunks[x, y] != null)
                        continue;

                    var logicalX = reqMinX + x;
                    var logicalY = reqMinY + y;

                    var chunkPos = new Vector2D(
                        WorldOrigin.X + logicalX * ChunkSize.X,
                        WorldOrigin.Y + logicalY * ChunkSize.Y);

                    var chunk = new SpaceChunk(this, logicalX, logicalY, chunkPos, ChunkSize);
                    
                    // Для привязки нескольких акторов к чанку, необходимо сделать IEnumerable<IFactory>
                    var actors = ActorsFactory.CreateActorForChunk(chunk);

                    chunk.ChunkActors.AddRange(actors);

                    foreach (var actor in actors)
                    {
                        ActorsService.RegisterActor(actor);
                    }

                    newChunks[x, y] = chunk;
                }
            }

            // Фиксируем новое состояние
            Chunks = newChunks;
            MinChunkX = reqMinX;
            MinChunkY = reqMinY;
        }

        public IEnumerator<ISpaceChunk> GetEnumerator()
        {
            for (var y = 0; y < ChunksCountY; y++)
            {
                for (var x = 0; x < ChunksCountX; x++)
                {
                    var chunk = Chunks[x, y];
                    if (chunk != null)
                        yield return chunk;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private (int x, int y) PositionToIndex(Vector2D position)
        {
            var x = (int)Math.Floor((position.X - WorldOrigin.X) / ChunkSize.X);
            var y = (int)Math.Floor((position.Y - WorldOrigin.Y) / ChunkSize.Y);
            return (x, y);
        }
    }
}
