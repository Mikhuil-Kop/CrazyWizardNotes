using System;
using UnityEngine;

namespace UNet.Common
{
    public class SingletonBehavior<T> : MonoBehaviour where T : class
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            //if (Instance != null)
            //{
            //    throw new Exception("Singleton может существовать только 1 экземпляре!");
            //}

            if (this is not T)
            {
                throw new Exception("Тип Singelton не соответствует своему наследнику.");
            }

            Instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            //if (Instance != this)
            //{
            //    throw new Exception("Singleton может существовать только 1 экземпляре!");
            //}

            //Instance = null;
        }

        protected C Locate<C>()
        {
            if (this is C)
            {
                throw new Exception("Services must not depend on themselves.");
            }

            var component = GetComponent<C>();

            if (component is null)
            {
                throw new Exception($"\"{this.GetType().Name}\" cannot find service \"{typeof(C).Name}\".");
            }

            return component;
        }
    }
}