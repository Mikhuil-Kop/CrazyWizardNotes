using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CrazyWizardNotes.Editor
{
    public abstract class GraphGroupBase : Group
    {
        public string ID { get; set; }

        public GraphGroupBase(string groupTitle, Vector2 position)
        {
            ID = Guid.NewGuid().ToString();

            title = groupTitle;

            SetPosition(new Rect(position, Vector2.zero));
        }
    }
}