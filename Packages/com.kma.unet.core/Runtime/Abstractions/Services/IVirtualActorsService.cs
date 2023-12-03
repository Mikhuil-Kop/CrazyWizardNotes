using System;

namespace UNet.Core.Abstractions
{
    /// <summary>
    /// Сервис для работы с простыми акторами без физического представления.
    /// </summary>
    [Obsolete("Сомневаюсь, что правильный подход. Сервис не порождает объекты, но как будто бы должен главенствовать. Странная ситуация.")]
    public interface IVirtualActorsService
    {
        void RegisterActor(IActor actor);
        void RemoveActor(IActor actor);
    }
}