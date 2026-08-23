using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    /// <summary>
    /// Класс, отвечающий за создание <see cref="IActor"/> - основания чанка. Заполняет <see cref="ISpaceChunk.SpaceActor"/>.
    /// Простая фабрика, не отвечающая за состояние <see cref="IActor"/> после создания.
    /// </summary>
    public interface IChunkSpaceActorFactory
    {
        /// <summary>
        /// Создать <see cref="IActor"/> для нового чанка.
        /// Не отвечающая за состояние <see cref="IActor"/> после создания.
        /// </summary>
        /// <param name="position">Позиция создаваемого чанка.</param>
        /// <param name="size">Размер создаваемого чанка.</param>
        public IActor CreateActorForChunk(Vector2D position, Vector2D size);
    }
}
