using System;
using UnityEngine;

namespace CrazyWizardNotes.Books
{
    [CreateAssetMenu(fileName = "new BookData", menuName = "ScriptableObjects/BookData", order = 1)]
    public class BookData : ScriptableObject
    {
        public string BookTitle;
        public PagesData[] Pages;
    }

    [Serializable]
    public class PagesData
    {
        public PageItemData[] Items;
    }

    [Serializable]
    public class PageItemData
    {
        public string Type;
        public Vector2 Position;
        public Vector2 Scale;
        public string[] Parameters;
    }
}