using System;
using UNet;
using UNet.Common;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace Assets.Code
{
    public class Canvas : IHandlesStart, IHandlesUpdate
    {
        private IUServiceProvider ServiceProvider { get; }
        private IAsyncJobService AsyncJobService { get; }
        private CanvasConfig Config { get; }

        public Canvas(IUServiceProvider serviceProvider, IAsyncJobService asyncJobService, CanvasConfig config)
        {
            ServiceProvider = serviceProvider;
            AsyncJobService = asyncJobService;
            Config = config;
        }

        public void OnStart()
        {
            UnityEngine.Debug.Log("Canvas - OnStart");

            AsyncJobService.Register(OnTick, 1f);
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void OnTick()
        {
            var spacePoint = ServiceProvider.GetService<IPhysicsService>().GetDefaultSpacePoint(Vector2D.Zero);
            var spaceChunks = spacePoint.ParentSpace.SpaceChunks;

            spaceChunks.Resize(new Vector2D[]
            {
                new(10, 10),
                new(-10, -10),
            });

            Debug.Log($"SpaceChunks.ChunksCountX - " + spaceChunks.ChunksCountX);
            Debug.Log($"SpaceChunks.ChunksCountY - " + spaceChunks.ChunksCountY);
        }
    }

    [Serializable]
    public class CanvasConfig
    {
        public GameObject AS;
    }

    public class F : IChunkSpaceActorFactory
    {
        private CanvasConfig Config { get; }

        public F(CanvasConfig config)
        {
            Config = config;
        }

        public IActor CreateActorForChunk(Vector2D position, Vector2D size)
        {
            Debug.Log("CreateActorForChunk");
            return new A();

            var g = GameObject.Instantiate(Config.AS);
            g.transform.position = position;
            g.transform.localScale = size;

            g.GetComponent<SpriteRenderer>().color = new Color((position.X + 10) / 20, (position.Y + 10) / 20, 0f);

            return new A();
        }
    }

    public class A : IActor
    {
        public IReadonlyTags Tags => new Tags();

        public T As<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public bool Is<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
