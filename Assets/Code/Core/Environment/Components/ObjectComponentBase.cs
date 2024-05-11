using CrazyWizardNotes.Environment.Objects;
using CrazyWizardNotes.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components
{
    [RequireComponent(typeof(EnvironmentObject))]
    public abstract class ObjectComponentBase : MonoBehaviour
    {
        protected ISubject Parent { get; private set; }

        protected virtual void Start()
        {
            Parent = Locate<ISubject>();
        }

        protected T Locate<T>()
        {
            if (this is T)
            {
                throw new System.Exception("Components can't depend on themselves");
            }

            return GetComponent<T>();
        }
    }
}