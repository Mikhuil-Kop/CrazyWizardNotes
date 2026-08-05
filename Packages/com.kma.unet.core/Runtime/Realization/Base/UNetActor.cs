using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Common;
using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    /// <summary>
    /// Базовый класс в UNet. Должен быть размещен на каждом игровом объекте.
    /// </summary>
    public sealed class UNetActor : MonoBehaviour, IActor, ISpawnableActor
    {
        #region Поля Unity

        public string[] tags;

        #endregion

        #region Поля

        public IUServiceProvider ServiceProvider { get; private set; } // TODO: закрыть
        public List<object> Components { get; private set; } = new(); // TODO: закрыть
        internal Queue<Action<IActorCreateContext>> ActorCreateActions { get; } = new Queue<Action<IActorCreateContext>>(); // TODO: закрыть
        internal Queue<Action<IActorCreatedContext>> ActorCreatedActions { get; } = new Queue<Action<IActorCreatedContext>>(); // TODO: закрыть
        internal Queue<Action<IActorDestroyedContext>> ActorDestroyedActions { get; } = new Queue<Action<IActorDestroyedContext>>(); // TODO: закрыть
        private bool Initialized { get; set; }

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

        public void AddOnCreateAction(params Action<IActorCreateContext>[] context)
        {
            foreach (var action in context.Where(x => x != null))
            {
                ActorCreateActions.Enqueue(action);
            }
        }

        public void AddOnCreatedAction(params Action<IActorCreatedContext>[] context)
        {
            foreach (var action in context.Where(x => x != null))
            {
                ActorCreatedActions.Enqueue(action);
            }
        }

        public void AddOnDestroyAction(params Action<IActorDestroyedContext>[] context)
        {
            foreach (var action in context.Where(x => x != null))
            {
                ActorDestroyedActions.Enqueue(action);
            }
        }

        public void Initialize()
        {
            if (Initialized)
            {
                return;
            }

            Initialized = true;
            Tags = new Tags(tags);

            var scene = UNetScene.Instance;
            var serviceProvider = scene.GetServiceProvider().BeginScope(ServiceScopeType.Actor);
            var componentsProvider = new ActorComponentsProvider();

            scene.InitializeActorCreate(this, serviceProvider, componentsProvider);

            var components = gameObject
                .GetComponents<Component>()
                .Where(x => x != this)
                .Select(x => x as object)
                .ToList();

            var servicesWrappers = components.As<IServicesWrapper>().ToList();
            var componentsWrappers = components.As<IComponentsWrapper>().ToList();
            var origins = components.Where(x => x is not IServicesWrapper && x is not IComponentsWrapper).ToList();

            serviceProvider.Add(this);
            serviceProvider.Add(gameObject);

            foreach (var origin in origins)
            {
                serviceProvider.Add(origin);
            }

            foreach (var serviceWrapper in servicesWrappers)
            {
                serviceWrapper.AddServices(serviceProvider);
            }

            ServiceProvider = serviceProvider;
            Components = origins
                .Concat(componentsWrappers.SelectMany(x => x.GetComponents(serviceProvider)))
                .Concat(componentsProvider.Components.Select(x => x.Invoke(serviceProvider)))
                .Distinct()
                .ToList();

            scene.InitializeActorCreated(this, serviceProvider);
        }

        private void Deinitialize()
        {
            var scene = UNetScene.Instance;
            scene.InitializeActorDestroyed(this, ServiceProvider);
        }

        private void Dispose()
        {
            ServiceProvider?.Dispose();
        }

        #endregion

        #region События

        private void Start()
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

        private void Update()
        {
            foreach (var component in Components.As<IHandlesUpdate>())
            {
                component.OnUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            foreach (var component in Components.As<IHandlesLateUpdate>())
            {
                component.OnLateUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            foreach (var component in Components.As<IHandlesFixedUpdate>())
            {
                component.OnFixedUpdate(Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            foreach (var component in Components.As<IHandlesDestroy>())
            {
                component.OnDestroy();
            }

            Deinitialize();
            Dispose();
        }

        #endregion
    }
}