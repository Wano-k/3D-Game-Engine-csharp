using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemDatas
    {
        public Dictionary<string,string> GameName;
        public List<string> Langs;
        public string StartMapName = "MAP0001";
        public int[] StartPosition = new int[] { 12, 0, 0, 12 };
        public int ScreenWidth = 640;
        public int ScreenHeight = 480;
        public bool FullScreen = false;
        public int SquareSize = 16;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemDatas(string gameName)
        {
            GameName = new Dictionary<string, string>();
            Langs = new List<string>(new string[] { "eng", "fr" });
            for (int i = 0; i < Langs.Count; i++)
            {
                GameName[Langs[i]] = gameName;
            }
        }

        // -------------------------------------------------------------------
        // NoStart
        // -------------------------------------------------------------------

        public void NoStart()
        {
            StartMapName = "";
            StartPosition = new int[] { 0, 0, 0 };
        }
    }
}
