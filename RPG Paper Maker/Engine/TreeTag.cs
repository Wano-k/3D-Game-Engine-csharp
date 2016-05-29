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
        public string RealMapName { get; }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public TreeTag()
        {
            this.IsRoot = true;
        }

        public TreeTag(bool isMap, string realMapName)
        {
            this.IsMap = isMap;
            this.RealMapName = realMapName;
            this.IsRoot = false;
        }

        // -------------------------------------------------------------------
        // Creators
        // -------------------------------------------------------------------

        public static TreeTag CreateRoot()
        {
            return new TreeTag();
        }

        public static TreeTag CreateMap(string realMapName)
        {
            return new TreeTag(true, realMapName);
        }

        public static TreeTag CreateDirectory()
        {
            return new TreeTag(false, null);
        }
    }
}
