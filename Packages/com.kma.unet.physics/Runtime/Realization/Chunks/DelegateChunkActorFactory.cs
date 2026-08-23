using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace UNet.Physics.Realization
{
    public class DelegateChunkActorFactory : IChunkSpaceActorFactory
    {
        private Func<Vector2D, Vector2D, IActor> FactoryFunc;

        public DelegateChunkActorFactory(Func<Vector2D, Vector2D, IActor> factoryFunc)
        {
            if (FactoryFunc == null)
            {
                throw new ArgumentNullException(nameof(factoryFunc));
            }

            FactoryFunc = factoryFunc;
        }

        public IActor CreateActorForChunk(Vector2D position, Vector2D size)
        {
            return FactoryFunc(position, size);
        }
    }
}
