using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class KeyboardAssign
    {
        public Dictionary<string, Keys> KeyboardEditorAssign { get; set; }
        public Dictionary<string, Keys> KeyboardGameAssign { get; set; }

        public Keys EditorMoveUp { get { return KeyboardEditorAssign["Move Up"]; } set { KeyboardEditorAssign["Move Up"] = value; } }
        public Keys EditorMoveDown { get { return KeyboardEditorAssign["Move Down"]; } set { KeyboardEditorAssign["Move Down"] = value; } }
        public Keys EditorMoveLeft { get { return KeyboardEditorAssign["Move Left"]; } set { KeyboardEditorAssign["Move Left"] = value; } }
        public Keys EditorMoveRight { get { return KeyboardEditorAssign["Move Right"]; } set { KeyboardEditorAssign["Move Right"] = value; } }


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public KeyboardAssign()
        {
            DefaultKeyboard();
        }
        public KeyboardAssign(Dictionary<string, Keys> editor, Dictionary<string, Keys> game)
        {
            KeyboardEditorAssign = new Dictionary<string, Keys>();
            foreach (KeyValuePair<string, Keys> entry in editor)
            {
                KeyboardEditorAssign[entry.Key] = entry.Value;
            }
            KeyboardGameAssign = new Dictionary<string, Keys>();
            foreach (KeyValuePair<string, Keys> entry in game)
            {
                KeyboardGameAssign[entry.Key] = entry.Value;
            }
        }

        // -------------------------------------------------------------------
        // DefaultKeyboard
        // -------------------------------------------------------------------

        public void DefaultKeyboard()
        {
            // Editor assign
            KeyboardEditorAssign = new Dictionary<string, Keys>();
            EditorMoveUp = Keys.W;
            EditorMoveDown = Keys.S;
            EditorMoveLeft = Keys.A;
            EditorMoveRight = Keys.D;

            // Game assign
            KeyboardGameAssign = new Dictionary<string, Keys>();
        }
    }
}
