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


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Game_map_portion(int pos_x, int pos_y)
        {
            texture_floors.Add(new int[] {0,0,16,16});
            texture_floors.Add(new int[] {16,0,16,16});

            floors.Add(new List<int[]>());
            floors.Add(new List<int[]>());
            for (int i = pos_x; i < pos_x+16; i++)
            {
                for (int j = pos_y; j < pos_y + 16; j++)
                {
                    /*
                    if ((i+ j) % 2 == 0)
                    {
                    */
                        floors[0].Add(new int[] { i, j, 0, 0 });
                    /*
                    }
                    else{
                        floors[1].Add(new int[] { i, j, 0, 0 });
                    }
                    */
                }
            }
        }
    }
}
