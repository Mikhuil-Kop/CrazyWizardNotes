using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyWizardNotes.Spells.Logic;
using CrazyWizardNotes.Common.Services;

public class ScrollObject : PhysicalObject, IReadable
{
    #region Поля

    public string Text;

    #endregion

    #region Поведение заклинаний

    string IReadable.GetReadText() => Text;

    #endregion
}