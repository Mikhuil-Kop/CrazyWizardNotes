using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    /// <summary>
    /// Класс, отвечающий за создание <see cref="IActor"/> - основания чанка. Заполняет <see cref="ISpaceChunk.ChunkActors"/>.
    /// Простая фабрика, не отвечающая за состояние <see cref="IActor"/> после создания.
    /// </summary>
    public interface ISpaceChunkActorsFactory
    {
        /// <summary>
        /// Создать <see cref="IActor"/> для нового чанка.
        /// Не отвечающая за состояние <see cref="IActor"/> после создания.
        /// </summary>
        /// <param name="spaceChunk">Чанк, для которого создается актор.</param>
        /// <param name="position">Позиция создаваемого чанка, его центр в пространстве его контейнера.</param>
        /// <param name="size">Размер создаваемого чанка.</param>
        public IEnumerable<IActor> CreateActorForChunk(ISpaceChunk spaceChunk);
    }
}
