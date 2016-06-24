using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    class TreeTag
    {
        public bool IsMap { get; }
        public bool IsRoot { get; }
        public string MapName { get; set; }
        public string RealMapName { get; }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public TreeTag()
        {
            IsRoot = true;
        }

        public TreeTag(bool isMap, string mapName, string realMapName)
        {
            IsMap = isMap;
            MapName = mapName;
            RealMapName = realMapName;
            IsRoot = false;
        }

        // -------------------------------------------------------------------
        // Creators
        // -------------------------------------------------------------------

        public static TreeTag CreateRoot()
        {
            return new TreeTag();
        }

        public static TreeTag CreateMap(string mapName, string realMapName)
        {
            return new TreeTag(true, mapName, realMapName);
        }

        public static TreeTag CreateDirectory()
        {
            return new TreeTag(false, null, null);
        }
    }
}
