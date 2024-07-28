using System.Collections.Generic;

namespace UNet.Core.Abstractions
{
    public interface IActorsCollectionService
    {
        void AddSubject(IActor subject);
        IEnumerable<IActor> GetAll();
        IEnumerable<T> Get<T>() where T : class;
    }
}