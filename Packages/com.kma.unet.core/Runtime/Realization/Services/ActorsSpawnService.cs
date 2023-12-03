using System;
using UNet.Common;
using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    public class ActorsSpawnService : IActorsSpawnService
    {
        private Transform ActorsRootTransform { get; set; }

        public ActorsSpawnService(ActorSpawnServiceConfig config)
        {
            ActorsRootTransform = config.ActorsRootTransform;
        }

        public IActor SpawnActor(string actorId, Action<IActorCreateContext> onCreate = null, Action<IActorCreatedContext> onCreated = null)
        {
            Exceptions.CheckArgumentNull(actorId, nameof(actorId));

            var prefab = Resources.Load<GameObject>($"Actors/{actorId}");
            var gameObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, ActorsRootTransform);

            var actor = gameObject.GetComponent<ISpawnableActor>()
                ?? throw new Exception("Actor created by service must contain ISpawnableActor component.");

            if (onCreate != null)
            {
                actor.AddOnCreateAction(onCreate);
            }

            if (onCreated != null)
            {
                actor.AddOnCreatedAction(onCreated);
            }

            actor.Initialize();

            return actor;
        }
    }
}