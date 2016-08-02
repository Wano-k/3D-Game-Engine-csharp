using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemAutotile : ComboxBoxSpecialTilesetItem
    {
        public static int MAX_AUTOTILES = 9999;
        public Collision Collision;


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public SystemAutotile(int id) : this(id, "", new SystemGraphic(GraphicKind.Autotile), new Collision())
        {

        }

        public SystemAutotile(int id, string n, SystemGraphic graphic, Collision collision)
        {
            Id = id;
            Name = n;
            Graphic = graphic;
            Collision = collision;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override SuperListItem CreateCopy()
        {
            return new SystemAutotile(Id, Name, Graphic.CreateCopy(), Collision.CreateCopy());
        }

        // -------------------------------------------------------------------
        // GetDefaultAutotiles
        // -------------------------------------------------------------------

        public static List<SystemAutotile> GetDefaultAutotiles()
        {
            List<SystemAutotile> list = new List<SystemAutotile>();
            list.Add(new SystemAutotile(1, "Grass", new SystemGraphic("grass.png", true, GraphicKind.Autotile), new Collision()));
            list.Add(new SystemAutotile(2, "Ground", new SystemGraphic("ground.png", true, GraphicKind.Autotile), new Collision()));
            for (int i = 2; i < 20; i++) list.Add(new SystemAutotile(i + 1));

            return list;
        }
    }
}
