using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNet.Common;
using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    /// <summary>
    /// Базовый класс в UNet. Должен быть размещен в каждой сцене.
    /// </summary>
    //[ExecuteInEditMode]
    public sealed class UNetScene : SingletonBehavior<UNetScene>, IScene
    {
        #region Данные приложения

        private static bool Initialized { get; set; }
        private static IUServiceProvider CoreServiceProvider { get; set; }
        private static IUServiceProvider ApplicationServiceProvider { get; set; }
        private static Action<IApplicationCreateContext>[] ApplicationActions { get; set; }
        private static Func<IApplicationCreateContext, Task>[] ApplicationAsyncActions { get; set; }
        private static Action<ISceneCreateContext>[] SceneActions { get; set; }
        private static Func<ISceneCreateContext, Task>[] SceneAsyncActions { get; set; }
        private static Action<IActorCreateContext>[] ActorCreateActions { get; set; }
        private static Func<IActorCreateContext, Task>[] ActorCreateAsyncActions { get; set; }
        private static Action<IActorCreatedContext>[] ActorCreatedActions { get; set; }
        private static Func<IActorCreatedContext, Task>[] ActorCreatedAsyncActions { get; set; }
        private static Action<IActorDestroyedContext>[] ActorDestroyedActions { get; set; }
        private static Func<IActorDestroyedContext, Task>[] ActorDestroyedAsyncActions { get; set; }

        #endregion

        #region Данные сцены
        
        private IUServiceProvider SceneServiceProvider { get; set; }
        private List<object> SceneComponents { get; set; }
        public IReadonlyTags Tags { get; set; }

        #endregion

        #region Поля Unity

        public string[] tags;

        #endregion

        #region Функционал

        protected override void Awake()
        {
            base.Awake();

            if (!Initialized)
            {
                InitializeApp();
            }

            InitializeScene();

            foreach (var component in SceneComponents.As<IHandlesAwake>())
            {
                component.OnAwake();
            }
        }

        private void Start()
        {
            foreach (var component in SceneComponents.As<IHandlesStart>())
            {
                component.OnStart();
            }
        }

        private void Update()
        {
            foreach (var component in SceneComponents.As<IHandlesUpdate>())
            {
                component.OnUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            foreach (var component in SceneComponents.As<IHandlesLateUpdate>())
            {
                component.OnLateUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            foreach (var component in SceneComponents.As<IHandlesFixedUpdate>())
            {
                component.OnFixedUpdate(Time.deltaTime);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (var component in SceneComponents.As<IHandlesDestroy>())
            {
                component.OnDestroy();
            }

            SceneServiceProvider?.Dispose();
        }

        public IUServiceProvider GetServiceProvider()
        {
            return SceneServiceProvider;
        }

        private void InitializeApp()
        {
            Initialized = true;

            var appBuilder = new UNetAppBuilder();
            var sceneBuilder = new UNetSceneBuilder();
            var actorBuilder = new UNetActorBuilder();
            var serviceBuilder = new UNetServiceBuilder();

            var context = new UNetStartupContext
            {
                AppBuilder = appBuilder,
                SceneBuilder = sceneBuilder,
                ActorBuilder = actorBuilder,
                ServiceBuilder = serviceBuilder,
            };

            ProcessStartup(context);

            CoreServiceProvider = serviceBuilder.Build();// TODO: фигня какая-то
            ApplicationServiceProvider = CoreServiceProvider.BeginScope(ServiceScopeType.Application);

            ApplicationActions = appBuilder.GetActions();
            ApplicationAsyncActions = appBuilder.GetAsyncActions();

            SceneActions = sceneBuilder.GetActions();
            SceneAsyncActions = sceneBuilder.GetAsyncActions();

            ActorCreateActions = actorBuilder.GetActions();
            ActorCreateAsyncActions = actorBuilder.GetAsyncActions();
            ActorCreatedActions = actorBuilder.GetCreatedActions();
            ActorCreatedAsyncActions = actorBuilder.GetAsyncCreatedActions();
            ActorDestroyedActions = actorBuilder.GetDestroyActions();
            ActorDestroyedAsyncActions = actorBuilder.GetAsyncDestroyActions();

            var appContext = new UNetApplicationCreateContext
            {
                ServiceProvider = ApplicationServiceProvider,
            };

            foreach (var action in ApplicationActions)
            {
                action.Invoke(appContext);
            }
        }

        private void InitializeScene()
        {
            SceneServiceProvider = ApplicationServiceProvider.BeginScope(ServiceScopeType.Scene);
            Tags = new Tags(tags);

            // ComponentsInitialization

            var serviceProvider = SceneServiceProvider;
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

            SceneComponents = origins
                .Concat(componentsWrappers.SelectMany(x => x.GetComponents(serviceProvider)))
                .Distinct()
                .ToList();

            // Actions Execution

            var sceneContext = new UNetSceneCreateContext
            {
                ServiceProvider = SceneServiceProvider,
                Logger = SceneServiceProvider.GetService<Abstractions.ILogger>(),
                Scene = this,
            };

            foreach (var action in SceneActions)
            {
                action.Invoke(sceneContext);
            }
        }

        internal void InitializeActorCreate(UNetActor actor, IUServiceProvider provider, IActorComponentsProvider components)
        {
            var actorContext = new UNetActorCreateContext
            {
                ServiceProvider = provider,
                ComponentsProvider = components,
                Logger = provider.GetService<Abstractions.ILogger>(),
                ActorTags = actor.Tags,
                Scene = this,
            };

            foreach (var asyncAction in actor.ActorCreateActions)
            {
                asyncAction?.Invoke(actorContext);
            }

            foreach (var asyncAction in ActorCreateActions)
            {
                asyncAction?.Invoke(actorContext);
            }
        }

        internal void InitializeActorCreated(UNetActor actor, IUServiceProvider provider)
        {
            var actorContext = new UNetActorCreatedContext
            {
                ServiceProvider = provider,
                Logger = provider.GetService<Abstractions.ILogger>(),
                Scene = this,
                Actor = actor
            };

            foreach (var asyncAction in actor.ActorCreatedActions)
            {
                asyncAction?.Invoke(actorContext);
            }

            foreach (var asyncAction in ActorCreatedActions)
            {
                asyncAction?.Invoke(actorContext);
            }
        }

        internal void InitializeActorDestroyed(UNetActor actor, IUServiceProvider provider)
        {
            var actorContext = new UNetActorDestroyedContext
            {
                ServiceProvider = provider,
                Logger = provider.GetService<Abstractions.ILogger>(),
                Scene = this,
                Actor = actor,
            };

            foreach (var action in actor.ActorDestroyedActions)
            {
                action?.Invoke(actorContext);
            }

            foreach (var action in ActorDestroyedActions)
            {
                action?.Invoke(actorContext);
            }
        }

        private void ProcessStartup(IStartupContext context)
        {
            var startup = GetStartup();
            startup.ConfigureStartup(context);
            (startup as IDisposable)?.Dispose();
        }

        private IStartup GetStartup()
        {
            var type1 = typeof(IStartup);
            var type2 = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .First(p => type1.IsAssignableFrom(p));

            var constructor = type2.GetConstructors().FirstOrDefault();

            return constructor.Invoke(new object[0]) as IStartup;
        }

        #endregion
    }
}
