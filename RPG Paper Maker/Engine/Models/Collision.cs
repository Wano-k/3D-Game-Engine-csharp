using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class Collision
    {
        public TilesetPassage[,] PassableCollision;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Collision() : this(new TilesetPassage[0,0])
        {

        }

        public Collision(TilesetPassage[,] passable)
        {
            PassableCollision = passable;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public Collision CreateCopy()
        {
            return new Collision(CopyPassable());
        }

        // -------------------------------------------------------------------
        // CopyPassable
        // -------------------------------------------------------------------

        public TilesetPassage[,] CopyPassable()
        {
            TilesetPassage[,] array = new TilesetPassage[PassableCollision.GetLength(0), PassableCollision.GetLength(1)];

            for (int i = 0; i < PassableCollision.GetLength(0); i++)
            {
                for (int j = 0; j < PassableCollision.GetLength(1); j++)
                {
                    array[i, j] = PassableCollision[i, j].CreateCopy();
                }
            }

            return array;
        }

        // -------------------------------------------------------------------
        // GetDefaultPassableCollision
        // -------------------------------------------------------------------

        public static TilesetPassage[,] GetDefaultPassableCollision(int width, int height)
        {
            TilesetPassage[,] array = new TilesetPassage[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    array[i, j] = new TilesetPassage();
                }
            }

            return array;
        }
    }
}
