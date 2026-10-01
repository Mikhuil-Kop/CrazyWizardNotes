using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    /// <summary>
    /// Класс, отвечающий за создание <see cref="IActor"/> - основания чанка. Заполняет <see cref="ISpaceChunk.ChunkActors"/>.
    /// Простая фабрика, не отвечающая за состояние <see cref="IActor"/> после создания.
    /// </summary>
    public interface ISpaceChunkActorFactory
    {
        /// <summary>
        /// Создать <see cref="IActor"/> для нового чанка.
        /// Не отвечающая за состояние <see cref="IActor"/> после создания.
        /// </summary>
        /// <param name="spaceChunk">Чанк, для которого создается актор.</param>
        public IActor CreateActorForChunk(ISpaceChunk spaceChunk);
    }
}
