using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Common;
using UNet.Core.Abstractions;
using UnityEditor.VersionControl;
using UnityEngine;
using ILogger = UNet.Core.Abstractions.ILogger;

namespace UNet.Core.Realization
{
    /// <summary>
    /// Базовый класс в UNet. Должен быть размещен на каждом игровом объекте.
    /// </summary>
    public class UNetActor : MonoBehaviour, IActor
    {
        #region Поля Unity

        public string[] tags;

        #endregion

        #region Поля

        public IUServiceProvider ServiceProvider { get; private set; } // TODO: закрыть
        public List<object> Components { get; private set; } // TODO: закрыть
        public Queue<Action<IActorCreateContext>> ActorActions { get; } = new Queue<Action<IActorCreateContext>>(); // TODO: закрыть
        public Queue<Action<IActorDestroyContext>> DestroyActions { get; } = new Queue<Action<IActorDestroyContext>>(); // TODO: закрыть
        public IReadonlyTags Tags { get; set; }

        #endregion

        #region Логика

        public bool Is<T>() where T : class
        {
            return Components.Any(x => x is T);
        }

        public T As<T>() where T : class
        {
            return Components.FirstOrDefault(x => x is T) as T;
        }

        public void InsertContext(params Action<IActorCreateContext>[] context)
        {
            foreach (var action in context)
            {
                ActorActions.Enqueue(action);
            }
        }

        private void Initialize()
        {
            Tags = new Tags(tags);

            var scene = UNetScene.Instance;
            var provider = scene.GetServiceProvider().BeginScope(ServiceScopeType.Actor);

            var components = gameObject
                .GetComponents<Component>()
                .Where(x => x != this)
                .Select(x => x as object)
                .ToList();

            var componentsWrappers = components.As<IComponentsWrapper>().ToList();
            var origins = components.Where(x => x is not IComponentsWrapper).ToList();

            foreach (var origin in origins)
            {
                provider.Add(origin);
            }

            scene.InitializeActor(this, provider);

            ServiceProvider = provider;
            Components = origins.Concat(componentsWrappers.SelectMany(x => x.GetComponents(provider))).ToList();
        }

        private void Dispose()
        {
            ServiceProvider?.Dispose();
        }

        #endregion

        #region События

        protected virtual void Start()
        {
            Initialize();

            foreach (var component in Components.As<IHandlesAwake>())
            {
                component.OnAwake();
            }

            foreach (var component in Components.As<IHandlesStart>())
            {
                component.OnStart();
            }
        }

        protected virtual void Update()
        {
            foreach (var component in Components.As<IHandlesUpdate>())
            {
                component.OnUpdate(Time.deltaTime);
            }
        }

        protected virtual void LateUpdate()
        {
            foreach (var component in Components.As<IHandlesLateUpdate>())
            {
                component.OnLateUpdate(Time.deltaTime);
            }
        }

        protected virtual void FixedUpdate()
        {
            foreach (var component in Components.As<IHandlesFixedUpdate>())
            {
                component.OnFixedUpdate(Time.deltaTime);
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var component in Components.As<IHandlesDestroy>())
            {
                component.OnDestroy();
            }

            var scene = UNetScene.Instance;
            scene.DestroyActor(this, ServiceProvider);

            Dispose();
        }

        #endregion
    }
}