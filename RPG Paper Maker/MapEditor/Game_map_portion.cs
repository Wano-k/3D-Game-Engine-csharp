using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class Game_map_portion
    {
        public List<int[]> texture_floors = new List<int[]>(); // All the textures used : {{0,0,16,16}, {0,16,16,16}, ...}
        public List<List<int[]>> floors = new List<List<int[]>>(); // All the coords for each texture : {{{coords1},{coords2},...}, {{coords3},...}, ...}
    }
}
