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

        public void AddActor(IActor actor)
        {
            Collection.AddLast(actor);
        }

        public void RemoveActor(IActor actor)
        {
            Collection.Remove(actor);
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