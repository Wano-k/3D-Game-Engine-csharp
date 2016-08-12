using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemRelief : ComboxBoxSpecialTilesetItem
    {
        public static int MAX_RELIEFS = 9999;
        public Collision Collision;


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public SystemRelief(int id) : this(id, "", new SystemGraphic(GraphicKind.Relief), new Collision())
        {

        }

        public SystemRelief(int id, string n, SystemGraphic graphic, Collision collision)
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
            return new SystemRelief(Id, Name, Graphic.CreateCopy(), Collision.CreateCopy());
        }

        // -------------------------------------------------------------------
        // GetDefaultReliefs
        // -------------------------------------------------------------------

        public static List<SystemRelief> GetDefaultReliefs()
        {
            List<SystemRelief> list = new List<SystemRelief>();
            list.Add(new SystemRelief(1, "Grass", new SystemGraphic("grass.png", true, GraphicKind.Relief), new Collision()));
            list.Add(new SystemRelief(2, "Halloween", new SystemGraphic("halloween.png", true, GraphicKind.Relief), new Collision()));

            return list;
        }
    }
}
