using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Charblar.Stickies
{
    public class SearchStickiesWindow : EditorWindow {

        private enum SearchType { title, color }
        private SearchType searchBy;

        private string searchTitle;
        private StickyNote.NoteColor searchColor;

        Vector2 scrollPosition = Vector2.zero;
        List<StickyNote> matchingNotes = null;

	    [MenuItem("Tools/Stickies/Search Notes", priority = 100)]
        public static void ShowWindow()
        {
            GetWindow<SearchStickiesWindow>(false, "Search Notes", true);
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            searchBy = (SearchType)EditorGUILayout.EnumPopup("Search by", searchBy);
            EditorGUILayout.EndHorizontal();

            //display proper gui for current search type
            switch (searchBy)       
            {
                case (SearchType.title):
                    searchTitle = EditorGUILayout.TextField(searchTitle);
                    break;
                case (SearchType.color):
                    searchColor = (StickyNote.NoteColor)EditorGUILayout.EnumPopup(searchColor);
                    break;
            }

            //display search results
            if (matchingNotes != null)
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
                EditorGUILayout.BeginVertical(GUI.skin.box);
                if (matchingNotes.Count > 0)
                {
                    EditorGUILayout.LabelField(matchingNotes.Count.ToString() + " results found!");
                    foreach (StickyNote n in matchingNotes)
                    {
                        DisplayNote(n);
                    }
                } 
                else
                {
                    EditorGUILayout.LabelField("No results found :(");
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndScrollView();
            }

            GUILayout.FlexibleSpace();

            //display buttons
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Clear", GUILayout.Width(50f)))
                matchingNotes = null;
            if (GUILayout.Button("Search", GUILayout.Width(60f)))
                PerformSearch();
            EditorGUILayout.EndHorizontal();
        }

        void PerformSearch()
        {
            matchingNotes = new List<StickyNote>();
            StickyNote[] notes = FindObjectsOfType<StickyNote>();

            switch (searchBy)       
            {
                case (SearchType.title):
                    foreach (StickyNote n in notes)
                    {
                        if (n.Title.IndexOf(searchTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                            matchingNotes.Add(n);
                    }
                    break;
                case (SearchType.color):
                    foreach (StickyNote n in notes)
                    {
                        if (n.Color == searchColor)
                            matchingNotes.Add(n);
                    }
                    break;
            }

            //sort results first to last
            matchingNotes.Reverse();
        }

        void DisplayNote(StickyNote n)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(n.name);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Select"))
            {
                if (n)
                    Selection.activeObject = n;
                else
                    Debug.LogWarning("Note does not exist, did you delete it?");
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }
    }
}
