using System;
using UnityEngine;

namespace CrazyWizardNotes.Inventory
{
    [CreateAssetMenu(fileName = "new BookData", menuName = "ScriptableObjects/BookData", order = 1)]
    public class BookData : ScriptableObject
    {
        public string Type;
        public string[] Parameters;
        public PageData[] Pages;
    }

    [Serializable]
    public class PageData
    {
        public string Type;
        public string[] Parameters;
        public PageItemData[] Items;
    }

    [Serializable]
    public class PageItemData
    {
        public string Type;
        public string[] Parameters;
        public Vector2 Position;
        public Vector2 Size;
    }
}