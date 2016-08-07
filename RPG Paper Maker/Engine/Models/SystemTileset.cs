using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemTileset : SuperListItem
    {
        public static int MAX_TILESETS = 9999;
        public SystemGraphic Graphic;
        public Collision Collision;
        public List<int> Autotiles = new List<int>();
        public List<int> Reliefs = new List<int>();


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public SystemTileset(int id) : this(id, "", new SystemGraphic(WANOK.NONE_IMAGE_STRING, true, GraphicKind.Tileset), new Collision(), new List<int>(), new List<int>())
        {
            
        }

        public SystemTileset(int id, string n, SystemGraphic graphic, Collision collision, List<int> autotiles, List<int> reliefs)
        {
            Id = id;
            Name = n;
            Graphic = graphic;
            Collision = collision;
            Autotiles = autotiles;
            Reliefs = reliefs;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override SuperListItem CreateCopy()
        {
            return new SystemTileset(Id, Name, Graphic.CreateCopy(), Collision.CreateCopy(), new List<int>(Autotiles), new List<int>(Reliefs));
        }

        // -------------------------------------------------------------------
        // GetDefaultTilesets
        // -------------------------------------------------------------------

        public static List<SystemTileset> GetDefaultTilesets()
        {
            List<SystemTileset> list = new List<SystemTileset>();
            list.Add(new SystemTileset(1, "Plains", new SystemGraphic("plains.png", true, GraphicKind.Tileset), new Collision(Collision.GetDefaultPassableCollision(8, 8)), new List<int>(new int[] { 1, 2 }), new List<int>(new int[] { 1 })));
            list.Add(new SystemTileset(2, "Halloween", new SystemGraphic("halloween.png", true, GraphicKind.Tileset), new Collision(Collision.GetDefaultPassableCollision(8, 8)), new List<int>(), new List<int>(new int[] { 2 })));

            return list;
        }
    }
}
