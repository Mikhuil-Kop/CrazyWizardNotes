using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Interface
{
    public class Dropper : MonoBehaviour, IDropper
    {
        public string Value => throw new System.NotImplementedException();

        public Vector2 Position { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IDragger CurrentDragger { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public bool CanDrop(IDragger dragger)
        {
            throw new System.NotImplementedException();
        }

        public void OnDrop(IDragger dragger)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IDropper
    {
        string Value { get; }
        Vector2 Position { get; set; }
        IDragger CurrentDragger { get; set; }

        bool CanDrop(IDragger dragger);
        void OnDrop(IDragger dragger);
    }
}
