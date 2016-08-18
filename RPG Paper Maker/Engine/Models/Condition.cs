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
            int selection = (int)condition[1];

            switch ((int)condition[0])
            {
                // Switches & varaibles
                case 0:
                    if (selection == 0)
                    {
                        return "Switch " + "--TODO--" + " is " + ((int)condition[3] == 0 ? "ON" : "OFF");  
                    }
                    else if (selection == 1)
                    {
                        return "";
                    }
                    else if (selection == 3)
                    {
                        return "";
                    }
                    break;
                // Heroes
                case 1:
                    if (selection == 0)
                    {
                        return "";
                    }
                    else if (selection == 1)
                    {
                        return "";
                    }
                    else if (selection == 3)
                    {
                        return "";
                    }
                    break;
                case 2:
                    break;
                default:
                    break;
            }

            return "";
        }
    }
}
