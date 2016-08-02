using System;
using System.Collections.Generic;
using System.IO;
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
        public List<SystemColor> Colors = new List<SystemColor>();
        public List<SystemElement> Elements = new List<SystemElement>();
        public string PathRTP;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemDatas(string gameName)
        {
            // Game name and langages
            GameName = new Dictionary<string, string>();
            Langs = new List<string>(new string[] { "eng" });
            for (int i = 0; i < Langs.Count; i++)
            {
                GameName[Langs[i]] = gameName;
            }

            // Path RTP
            PathRTP = Path.Combine(WANOK.ExcecutablePathDir, "RTP");

            // Colors
            Colors.Add(SystemColor.BlackColor);
            Colors.Add(SystemColor.BlackGrayColor);
            Colors.Add(SystemColor.SilverColor);
            Colors.Add(SystemColor.WhiteColor);
            Colors.Add(SystemColor.RedColor);
            Colors.Add(SystemColor.OrangeColor);
            Colors.Add(SystemColor.YellowColor);
            Colors.Add(SystemColor.GreenColor);
            Colors.Add(SystemColor.CyanColor);
            Colors.Add(SystemColor.BlueColor);
            Colors.Add(SystemColor.PurpleColor);
            Colors.Add(SystemColor.MagentaColor);
            Colors.Add(SystemColor.PinkColor);
            for (int i = 13; i < 20; i++) Colors.Add(new SystemColor(i+1));

            // Elements
            Elements.Add(new SystemElement(1, WANOK.GetDefaultNames("Fire"), new SystemGraphic("fire.png", true, GraphicKind.Icon), Langs[0]));
            Elements.Add(new SystemElement(2, WANOK.GetDefaultNames("Water"), new SystemGraphic("water.png", true, GraphicKind.Icon), Langs[0]));
            Elements.Add(new SystemElement(3, WANOK.GetDefaultNames("Grass"), new SystemGraphic("grass.png", true, GraphicKind.Icon), Langs[0]));
            Elements.Add(new SystemElement(4, WANOK.GetDefaultNames("Wind"), new SystemGraphic("wind.png", true, GraphicKind.Icon), Langs[0]));
            Elements.Add(new SystemElement(5, WANOK.GetDefaultNames("Light"), new SystemGraphic("light.png", true, GraphicKind.Icon), Langs[0]));
            Elements.Add(new SystemElement(6, WANOK.GetDefaultNames("Darkness"), new SystemGraphic("darkness.png", true, GraphicKind.Icon), Langs[0]));
        }

        // -------------------------------------------------------------------
        // GetColorById
        // -------------------------------------------------------------------

        public SystemColor GetColorById(int id)
        {
            if (id > Colors.Count) return new SystemColor(-1);
            return Colors.Find(i => i.Id == id);
        }

        // -------------------------------------------------------------------
        // GetColorIndexById
        // -------------------------------------------------------------------

        public int GetColorIndexById(int id)
        {
            return Colors.IndexOf(GetColorById(id));
        }

        // -------------------------------------------------------------------
        // NoStart
        // -------------------------------------------------------------------

        public void NoStart()
        {
            StartMapName = "";
            StartPosition = new int[] { 0, 0, 0 };
        }

        // -------------------------------------------------------------------
        // MajElementsNames
        // -------------------------------------------------------------------

        public void MajElementsNames()
        {
            /*
            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].SetName();
            }*/
        }
    }
}
