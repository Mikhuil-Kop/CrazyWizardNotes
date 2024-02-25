using System;
using UnityEngine;
using CrazyWizardNotes.Spells.Logic;
using CrazyWizardNotes.Common;
using CrazyWizardNotes.Environment.Components.Interfaces;
using CrazyWizardNotes.Environment.Components.DefaultRealisation;

namespace CrazyWizardNotes.Environment.Objects
{
    public abstract class CustomEnvironmentObjectBase : MonoBehaviour
    {
        #region Поля

        #endregion

        #region Абстракции

        protected abstract void LocateComponents(IRealisationProvider provider);

        #endregion

        #region Логика

        protected virtual void Awake()
        {
            var provider = new ComponentsProvider(this);

            provider.AddDefaultImplementation(() => new NullPhysicalComponent());

            LocateComponents(provider);
        }

        #endregion
    }
}