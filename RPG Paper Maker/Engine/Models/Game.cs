using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class Game
    {
        public SystemDatas System;
        public TilesetsDatas Tilesets;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Game(SystemDatas system, TilesetsDatas tilesets)
        {
            System = system;
            Tilesets = tilesets;
        }
    }
}
