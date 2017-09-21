using UnityEngine;
using UnityEditor;

namespace Charblar.Stickies
{
    public class StickyNote : MonoBehaviour 
    {
        public enum NoteColor { white, yellow, blue, pink, green, red }

        [SerializeField] private string title;
        [SerializeField] private NoteColor color;
        [Space(5)]
        [SerializeField] [TextArea(5,10)] private string text;

        public string Title { get { return title; } set { return; } }
        public NoteColor Color { get { return color; } set { return; } }

        [MenuItem("Tools/Stickies/Create Note", priority = 0)]
        static void CreateNote(MenuCommand command) 
        {
            var note = new GameObject("New Note", typeof(StickyNote));
            note.tag = "EditorOnly";
            Undo.RegisterCreatedObjectUndo(note, "Created Note");
            Selection.activeObject = note;
        }

        void OnDrawGizmos() 
        {
            switch (color)
            {
                case NoteColor.white:
                    Gizmos.DrawIcon(transform.position, "Stickies/Gizmos_Note_White", true);
                    break;
                case NoteColor.yellow:
                    Gizmos.DrawIcon(transform.position, "Stickies/Gizmos_Note_Yellow", true);
                    break;
                case NoteColor.blue:
                    Gizmos.DrawIcon(transform.position, "Stickies/Gizmos_Note_Blue", true);
                    break;
                case NoteColor.pink:
                    Gizmos.DrawIcon(transform.position, "Stickies/Gizmos_Note_Pink", true);
                    break;
                case NoteColor.green:
                    Gizmos.DrawIcon(transform.position, "Stickies/Gizmos_Note_Green", true);
                    break;
                case NoteColor.red:
                    Gizmos.DrawIcon(transform.position, "Stickies/Gizmos_Note_Red", true);
                    break;
                default:
                    Gizmos.DrawIcon(transform.position, "Stickies/Gizmos_Note_White", true);
                    break;
            }
        }
    }
}
