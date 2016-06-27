using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class Tileset : SuperListItem
    {
        public static int MAX_TILESETS = 9999;
        public SystemGraphic Graphic;
        public TilesetPassage[,] PassableCollision;


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public Tileset(int id) : this(id, "", new SystemGraphic("plains.png", true, GraphicKind.Tileset), GetDefaultPassableCollision(8,8))
        {
            
        }

        public Tileset(int id, string n, SystemGraphic graphic, TilesetPassage[,] passableCollision)
        {
            Id = id;
            Name = n;
            Graphic = graphic;
            PassableCollision = passableCollision;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override SuperListItem CreateCopy()
        {
            return new Tileset(Id, Name, Graphic.CreateCopy(), PassableCollision);
        }

        // -------------------------------------------------------------------
        // GetDefaultPassableCollision
        // -------------------------------------------------------------------

        public static TilesetPassage[,] GetDefaultPassableCollision(int width, int height)
        {
            TilesetPassage[,] array = new TilesetPassage[width,height];

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
