using System;
using UnityEngine;
using CrazyWizardNotes.Interfaces;
using System.Collections.Generic;

namespace CrazyWizardNotes.Environment.Components
{
    public class TagComponent : ObjectComponentBase, ITaggable
    {
        [SerializeField]
        private string[] Tags;

        string[] ITaggable.Tags => Tags;
    }
}