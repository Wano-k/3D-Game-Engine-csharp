using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class Autotile : SuperListItem
    {
        public static int MAX_AUTOTILES = 9999;
        public SystemGraphic Graphic;
        public Collision Collision;


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public Autotile(int id) : this(id, "", new SystemGraphic(WANOK.NONE_IMAGE_STRING, true, GraphicKind.Autotile), new Collision())
        {

        }

        public Autotile(int id, string n, SystemGraphic graphic, Collision collision)
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
            return new Autotile(Id, Name, Graphic.CreateCopy(), Collision.CreateCopy());
        }

        // -------------------------------------------------------------------
        // GetDefaultAutotiles
        // -------------------------------------------------------------------

        public static List<Autotile> GetDefaultAutotiles()
        {
            List<Autotile> list = new List<Autotile>();
            for (int i = 0; i < 20; i++) list.Add(new Autotile(i + 1));

            return list;
        }
    }
}
