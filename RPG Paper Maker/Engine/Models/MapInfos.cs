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
        protected Dictionary<int[], int> OccurrenceFloors = new Dictionary<int[], int>();
        public string RealMapName;
        public string MapName;
        public int Width;
        public int Height;
        public int SkyColor = 0;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public MapInfos(string mapName, int width, int height)
        {
            RealMapName = mapName;
            MapName = mapName;
            Width = width;
            Height = height;
        }
    }
}
