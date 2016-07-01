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
        public List<Tileset> Tilesets = new List<Tileset>();
        public List<SystemAutotile> Autotiles = new List<SystemAutotile>();
        public List<SystemColor> Colors = new List<SystemColor>();
        public string PathRTP;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemDatas(string gameName)
        {
            // Game name and langages
            GameName = new Dictionary<string, string>();
            Langs = new List<string>(new string[] { "eng", "fr" });
            for (int i = 0; i < Langs.Count; i++)
            {
                GameName[Langs[i]] = gameName;
            }

            // Path RTP
            PathRTP = Path.Combine(WANOK.ExcecutablePathDir, "RTP");

            // Tilesets
            Tilesets.Add(new Tileset(1, "Plains", new SystemGraphic("plains.png", true, GraphicKind.Tileset), new Collision(Collision.GetDefaultPassableCollision(8,8)), new List<int>(new int[] { 1, 2 })));
            for (int i = 1; i < 20; i++) Tilesets.Add(new Tileset(i + 1));
            Autotiles = SystemAutotile.GetDefaultAutotiles();

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
        // GetTilesetById
        // -------------------------------------------------------------------

        public Tileset GetTilesetById(int id)
        {
            if (id > Tilesets.Count) return new Tileset(-1);
            return Tilesets.Find(i => i.Id == id);
        }

        // -------------------------------------------------------------------
        // GetTilesetIndexById
        // -------------------------------------------------------------------

        public int GetTilesetIndexById(int id)
        {
            return Tilesets.IndexOf(GetTilesetById(id));
        }

        // -------------------------------------------------------------------
        // GetAutotileById
        // -------------------------------------------------------------------

        public SystemAutotile GetAutotileById(int id)
        {
            if (id > Autotiles.Count) return new SystemAutotile(-1);
            return Autotiles.Find(i => i.Id == id);
        }

        // -------------------------------------------------------------------
        // GetAutotileIndexById
        // -------------------------------------------------------------------

        public int GetAutotileIndexById(int id)
        {
            return Autotiles.IndexOf(GetAutotileById(id));
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
