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


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public Autotile(int id) : this(id, "")
        {

        }

        public Autotile(int id, string n)
        {
            Id = id;
            Name = n;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override SuperListItem CreateCopy()
        {
            return new Autotile(Id);
        }
    }
}
