using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CrazyWizardNotes.Editor
{
    public class SearchWindow : ScriptableObject, ISearchWindowProvider
    {
        public SearchWindow()
        {
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var searchTreeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Elements")),
                new SearchTreeGroupEntry(new GUIContent("Dialogue Nodes"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice"))
                {
                    userData = null,
                    level = 2
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice"))
                {
                    userData = null,
                    level = 2
                },
                new SearchTreeGroupEntry(new GUIContent("Dialogue Groups"), 1),
                new SearchTreeEntry(new GUIContent("Single Group"))
                {
                    userData = null,
                    level = 2
                }
            };

            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            if (searchTreeEntry.userData is Action)
            {
                (searchTreeEntry.userData as Action).Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}