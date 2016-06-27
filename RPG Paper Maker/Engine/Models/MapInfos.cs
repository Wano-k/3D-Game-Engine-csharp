using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class MapInfos
    {
        public string RealMapName = "MAP0001";
        public string MapName = "MAP0001";
        public int Width = 25;
        public int Height = 25;
        public int Tileset = 1;
        public int SkyColor = 1;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public MapInfos()
        {

        }

        public MapInfos(string mapName, int width, int height)
        {
            RealMapName = mapName;
            MapName = mapName;
            Width = width;
            Height = height;
        }
    }
}
