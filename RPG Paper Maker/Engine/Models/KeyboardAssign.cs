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

        public Keys EditorMoveUp { get { return KeyboardEditorAssign["Move Up"]; } }
        public Keys EditorMoveDown { get { return KeyboardEditorAssign["Move Down"]; }  }
        public Keys EditorMoveLeft { get { return KeyboardEditorAssign["Move Left"]; }  }
        public Keys EditorMoveRight { get { return KeyboardEditorAssign["Move Right"]; }  }
        public Keys EditorShowGrid { get { return KeyboardEditorAssign["Show grid"]; } }


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
            KeyboardEditorAssign["Move Up"] = Keys.W;
            KeyboardEditorAssign["Move Down"] = Keys.S;
            KeyboardEditorAssign["Move Left"] = Keys.A;
            KeyboardEditorAssign["Move Right"] = Keys.D;
            KeyboardEditorAssign["Show grid"] = Keys.G;

            // Game assign
            KeyboardGameAssign = new Dictionary<string, Keys>();
        }
    }
}
