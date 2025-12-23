using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class ActorsCollectionService : IActorsCollectionService
    {
        private LinkedList<IActor> Collection { get; }

        public ActorsCollectionService()
        {
            Collection = new LinkedList<IActor>();
        }

        public void AddActor(IActor subject)
        {
            Collection.AddLast(subject);
        }

        public IEnumerable<IActor> GetAll()
        {
            return Collection;
        }

        public IEnumerable<T> Get<T>() where T : class
        {
            return Collection.ActorsAs<T>();
        }
    }
}