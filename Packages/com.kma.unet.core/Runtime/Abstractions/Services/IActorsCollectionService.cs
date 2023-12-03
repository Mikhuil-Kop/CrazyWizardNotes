using System.Collections.Generic;

namespace UNet.Core.Abstractions
{
    public interface IActorsCollectionService
    {
        void AddActor(IActor actor);
        void RemoveActor(IActor actor);
        IEnumerable<IActor> GetAll();
        IEnumerable<T> Get<T>() where T : class;
    }
}