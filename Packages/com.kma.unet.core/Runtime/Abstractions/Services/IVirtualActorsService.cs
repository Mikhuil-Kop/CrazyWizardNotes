using System;

namespace UNet.Core.Abstractions
{
    /// <summary>
    /// Сервис для работы с простыми акторами без физического представления.
    /// </summary>
    public interface IVirtualActorsService
    {
        IActor AddActor(Func<IActor> actor);
        void RemoveActor(IActor actor);
    }
}