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
    public class UNetScene : SingletonBehavior<UNetScene>, IScene
    {
        #region Данные приложения

        private static bool Initialized { get; set; }
        private static IUServiceProvider CoreServiceProvider { get; set; }
        private static IUServiceProvider ApplicationServiceProvider { get; set; }
        private static Action<IUServiceProvider>[] ApplicationActions { get; set; }
        private static Func<IUServiceProvider, Task>[] ApplicationAsyncActions { get; set; }
        private static Action<IUServiceProvider>[] SceneActions { get; set; }
        private static Func<IUServiceProvider, Task>[] SceneAsyncActions { get; set; }

        #endregion

        #region Данные сцены

        private IUServiceProvider SceneServiceProvider { get; set; }
        private List<object> SceneComponents { get; set; }

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
        }

        protected override void OnDestroy()
        {
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
            var serviceBuilder = new UNetServiceBuilder();

            var context = new UNetStartupContext
            {
                AppBuilder = appBuilder,
                SceneBuilder = sceneBuilder,
                ServiceBuilder = serviceBuilder,
            };

            ProcessStartup(context);

            CoreServiceProvider = serviceBuilder.Build();
            ApplicationServiceProvider = CoreServiceProvider.BeginScope(ServiceScopeType.Application);

            ApplicationActions = appBuilder.GetActions();
            ApplicationAsyncActions = appBuilder.GetAsyncActions();
            SceneActions = sceneBuilder.GetActions();
            SceneAsyncActions = sceneBuilder.GetAsyncActions();

            foreach (var action in ApplicationActions)
            {
                action.Invoke(ApplicationServiceProvider);
            }
        }

        private void InitializeScene()
        {
            SceneServiceProvider = ApplicationServiceProvider.BeginScope(ServiceScopeType.Scene);

            // ComponentsInitialization

            var components = gameObject
                .GetComponentsInChildren<Component>()
                .Where(x => x != this)
                .Select(x => x as object)
                .ToList();

            var optionsWrappers = components.As<IOptionsWrapper>().ToList();
            var componentsWrappers = components.As<IComponentsWrapper>().ToList();
            var origins = components.Where(x => x is not IOptionsWrapper && x is not IComponentsWrapper).ToList();

            foreach (var origin in origins)
            {
                SceneServiceProvider.Add(origin);
            }

            foreach (var optionsWrapper in optionsWrappers)
            {
                foreach (var option in optionsWrapper.GetOptions())
                {
                    SceneServiceProvider.Add(option);
                }
            }

            SceneComponents = origins.Concat(componentsWrappers.SelectMany(x => x.GetComponents(SceneServiceProvider))).ToList();

            // Actions Execution

            foreach (var action in SceneActions)
            {
                action.Invoke(SceneServiceProvider);
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
