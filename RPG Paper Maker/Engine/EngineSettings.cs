using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    class EngineSettings
    {
        public bool ShowDemoTip {get; set;}
        public const int MAX_RECENT_SIZE = 10;
        public List<string> ListRecentProjects { get; set; }
        public KeyboardAssign KeyboardAssign { get; set; }


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public EngineSettings()
        {
            ShowDemoTip = false;
            ListRecentProjects = new List<string>();
            KeyboardAssign = new KeyboardAssign();
        }

        // -------------------------------------------------------------------
        // AddProjectPath
        // -------------------------------------------------------------------

        public int AddProjectPath(string path)
        {
            int index = ListRecentProjects.IndexOf(path);
            if (index != -1) ListRecentProjects.RemoveAt(index);
            ListRecentProjects.Insert(0, path);
            if (ListRecentProjects.Count > MAX_RECENT_SIZE) ListRecentProjects.RemoveAt(ListRecentProjects.Count-1);
            return index;
        }
    }
}
