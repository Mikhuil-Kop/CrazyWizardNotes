using System.Collections.Generic;
using System.Linq;
using CrazyWizardNotes.Common;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Common.Services;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Objects
{
    public class EnvironmentObject : MonoBehaviour, ISubject
    {
        #region Поля

        private List<object> Components;

        #endregion

        #region Логика

        protected virtual void Awake()
        {
            Components = gameObject
                .GetComponents<MonoBehaviour>()
                .Where(x => x.GetType().Namespace.StartsWith(nameof(CrazyWizardNotes)))
                .Select(x => x as object)
                .ToList();
        }

        protected virtual void Start()
        {
            SubjectsCollectionService.Instance.AddSubject(this);
        }

        protected virtual void OnDestroy()
        {
            Components = new List<object>();
        }

        public bool Is<T>() where T : class
        {
            return Components.Any(x => x is T);
        }

        public T As<T>() where T : class
        {
            return Components.FirstOrDefault(x => x is T) as T;
        }

        #endregion
    }
}