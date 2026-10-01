using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    /// <summary>
    /// Чанк. Фрагмент пространства.
    /// </summary>
    public interface ISpaceChunk
    {
        /// <summary>
        /// Коллекция чанков, к которой принадлежит этот чанк.
        /// </summary>
        public ISpaceChunks ParentChunks { get; }

        /// <summary>
        /// Индекс чанка в его <see cref="ISpaceChunks"/>.
        /// </summary>
        public int ChunkIndexX { get; }

        /// <summary>
        /// Индекс чанка в его <see cref="ISpaceChunks"/>.
        /// </summary>
        public int ChunkIndexY { get; }

        /// <summary>
        /// Положение чанка в его <see cref="ISpaceContainer"/>.
        /// </summary>
        public Vector2D ChunkPosition { get; }
        
        /// <summary>
        /// Размер чанка (ширина и высота).
        /// </summary>
        public Vector2D ChunkSize { get; }

        /// <summary>
        /// Объекты, отвечающий за игровое поведение данного чанка. Обычно представляет поведение воздуха.
        /// </summary>
        public IEnumerable<IActor> ChunkActors { get; }
    }

    public static class ISpaceChunkExtensions
    {
        public static T As<T>(this ISpaceChunk chunk) where T : class
        {
            return chunk.ChunkActors.FirstOrDefault(x => x is T) as T;
        }
    }
}
