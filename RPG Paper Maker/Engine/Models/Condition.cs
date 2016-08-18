using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public static class Condition
    {

        // -------------------------------------------------------------------
        // DefaultObjects
        // -------------------------------------------------------------------

        public static List<object> DefaultObjects()
        {
            return new List<object>(new object[] { 0, 0, 1, 0 });
        }

        // -------------------------------------------------------------------
        // ToBool
        // -------------------------------------------------------------------

        public static bool ToBool(List<object> condition)
        {
            return true;
        }

        // -------------------------------------------------------------------
        // ToString
        // -------------------------------------------------------------------

        public static string ToString(List<object> condition)
        {
            return "";
        }
    }
}
