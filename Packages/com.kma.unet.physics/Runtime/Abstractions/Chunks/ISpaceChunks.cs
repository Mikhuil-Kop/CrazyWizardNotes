using System.Collections.Generic;
using System.Linq;

namespace UNet.Physics.Abstractions
{
    /// <summary>
    /// Коллекция чанков.
    /// </summary>
    public interface ISpaceChunks : IEnumerable<ISpaceChunk>
    {
        /// <summary>
        /// Пространство, к которому принадлежит эта коллекция чанков.
        /// </summary>
        public ISpaceContainer ParentContainer { get; }

        /// <summary>
        /// Количество чанков по горизонтали.
        /// </summary>
        public int ChunksCountX { get; }

        /// <summary>
        /// Количество чанков по вертикали.
        /// </summary>
        public int ChunksCountY { get; }

        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }

        /// <summary>
        /// Получение чанка по его индексу. Индекс может принимать отрицательное значение.
        /// </summary>
        /// <param name="x">Индекс X. (Слева направо).</param>
        /// <param name="y">Индекс Y. (Снизу вверх).</param>
        /// <returns>Чанк. Null - если такого чанка нет.</returns>
        public ISpaceChunk this[int x, int y] { get; }

        /// <summary>
        /// Получение чанка, внутри которого находится переданная точка.
        /// </summary>
        /// <param name="position">Положение точки в пространстве чанков.</param>
        /// <returns>Чанк. Null - если такого чанка нет.</returns>
        public ISpaceChunk this[Vector2D position] { get; }

        /// <summary>
        /// Добавить новые чанки и перестроить структуру, чтобы вместить все объекты, находящиеся в пространстве <see cref="ISpaceContainer"/>.
        /// </summary>
        /// <param name="positions">Позиции объектов в пространстве <see cref="ISpaceContainer"/>.</param>
        public void Resize(IEnumerable<Vector2D> positions);
    }

    public static class ChunksExtensions
    {
        public static ISpaceChunk Left(this ISpaceChunk chunk) => chunk.ParentChunks[chunk.ChunkIndexX - 1, chunk.ChunkIndexY];
        public static ISpaceChunk Right(this ISpaceChunk chunk) => chunk.ParentChunks[chunk.ChunkIndexX + 1, chunk.ChunkIndexY];
        public static ISpaceChunk Up(this ISpaceChunk chunk) => chunk.ParentChunks[chunk.ChunkIndexX, chunk.ChunkIndexY + 1];
        public static ISpaceChunk Down(this ISpaceChunk chunk) => chunk.ParentChunks[chunk.ChunkIndexX, chunk.ChunkIndexY - 1];

        public static IEnumerable<ISpaceChunk> Adjacent(this ISpaceChunk chunk)
        {
            return new[]
            {
                chunk.ParentChunks[chunk.ChunkIndexX - 1, chunk.ChunkIndexY],
                chunk.ParentChunks[chunk.ChunkIndexX + 1, chunk.ChunkIndexY],
                chunk.ParentChunks[chunk.ChunkIndexX, chunk.ChunkIndexY + 1],
                chunk.ParentChunks[chunk.ChunkIndexX, chunk.ChunkIndexY - 1]
            }
            .Where(x => x != null);
        }

        public static IEnumerable<ISpaceChunk> Surrounding(this ISpaceChunk chunk)
        {
            return new[]
            {
                chunk.ParentChunks[chunk.ChunkIndexX - 1, chunk.ChunkIndexY - 1],
                chunk.ParentChunks[chunk.ChunkIndexX - 1, chunk.ChunkIndexY],
                chunk.ParentChunks[chunk.ChunkIndexX - 1, chunk.ChunkIndexY + 1],

                chunk.ParentChunks[chunk.ChunkIndexX, chunk.ChunkIndexY - 1],
                //chunk.ParentChunks[chunk.ChunkIndexX, chunk.ChunkIndexY],
                chunk.ParentChunks[chunk.ChunkIndexX, chunk.ChunkIndexY + 1],

                chunk.ParentChunks[chunk.ChunkIndexX + 1, chunk.ChunkIndexY - 1],
                chunk.ParentChunks[chunk.ChunkIndexX + 1, chunk.ChunkIndexY],
                chunk.ParentChunks[chunk.ChunkIndexX + 1, chunk.ChunkIndexY + 1],
            }
            .Where(x => x != null);
        }
    }
}
