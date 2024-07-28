using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    /// <summary>
    /// Базовый класс в UNet. Должен быть размещен на каждом игровом объекте.
    /// </summary>
    public class UNetActor : MonoBehaviour, IActor
    {
        #region Поля

        private IUServiceProvider ServiceProvider { get; set; }
        private List<object> Components { get; set; }

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

        private void Initialize()
        {
            var scene = UNetScene.Instance;
            var provider = scene.GetServiceProvider().BeginScope(ServiceScopeType.Actor);

            var components = gameObject
                .GetComponents<Component>()
                .Where(x => x != this)
                .Select(x => x as object)
                .ToList();

            var optionsWrappers = components.As<IOptionsWrapper>().ToList();
            var componentsWrappers = components.As<IComponentsWrapper>().ToList();
            var origins = components.Where(x => x is not IOptionsWrapper && x is not IComponentsWrapper).ToList();

            foreach (var origin in origins)
            {
                provider.Add(origin);
            }

            foreach (var optionsWrapper in optionsWrappers)
            {
                foreach (var option in optionsWrapper.GetOptions())
                {
                    provider.Add(option);
                }
            }

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

            Dispose();
        }

        #endregion
    }
}