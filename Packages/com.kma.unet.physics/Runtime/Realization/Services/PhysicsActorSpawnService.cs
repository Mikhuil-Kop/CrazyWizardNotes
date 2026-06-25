using System;
using UNet.Common;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;
using ILogger = UNet.Core.Abstractions.ILogger;

namespace UNet.Physics.Realization
{
    public class PhysicsActorSpawnService : IPhysicsActorSpawnService
    {
        private IPhysicsService PhysicsService { get; set; }
        private Transform ActorsRootTransform { get; set; }

        public void SpawnActor(string actorId, SpacePoint spawnPoint, Action<IActorCreateContext> onCreate = null)
        {
            Exceptions.CheckArgumentNull(actorId, nameof(actorId));
            Exceptions.CheckArgumentNull(spawnPoint, nameof(spawnPoint));

            var prefab = Resources.Load<GameObject>($"Actors/{actorId}");
            var gameObject = GameObject.Instantiate(prefab, spawnPoint.Position, Quaternion.identity, ActorsRootTransform);
            var actor = gameObject.GetComponent<IActor>();

            gameObject.transform.position = spawnPoint.Position;
            actor.InsertContext(onCreate);
            // TODO: ФИГНЯ, ПОПРАВИТЬ
            actor.InsertContext(x =>
            {
                var hasSpacePoint = actor.As<IHasSpacePoint>();

                if (hasSpacePoint != null)
                {
                    hasSpacePoint.SpacePoint = spawnPoint;
                }
            });
        }

        public void SpawnActor(string actorId, Action<IActorCreateContext> onCreate = null)
        {
            SpawnActor(actorId, PhysicsService.GetDefaultSpacePoint(Vector2D.Zero), onCreate);
        }

        private class ActorCreateContext : IActorCreateContext
        {
            public IUServiceProvider ServiceProvider { get; set; }
            public ILogger Logger { get; set; }
            public IScene Scene { get; set; }
            public IActor Actor { get; set; }
        }
    }
}