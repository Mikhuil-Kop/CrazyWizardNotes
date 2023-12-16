using System;
using UnityEngine;

namespace CrazyWizardNotes.Common
{
    public class SingletonBehavior<T> : MonoBehaviour where T : SingletonBehavior<T>
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

            Instance = null;
        }
    }
}