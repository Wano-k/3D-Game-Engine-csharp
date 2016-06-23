using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemColor : SuperListItem
    {
        public int[] Color = new int[] { 0, 0, 0 };
        public static SystemColor BlackColor = new SystemColor("Black", new int[] { 0, 0, 0 });
        public static SystemColor BlackGrayColor = new SystemColor("Black-Gray", new int[] { 32, 32, 32 });
        public static SystemColor SilverColor = new SystemColor("Silver", new int[] { 185, 185, 185 });
        public static SystemColor WhiteColor = new SystemColor("White", new int[] { 255, 255, 255 });
        public static SystemColor RedColor = new SystemColor("Red", new int[] { 255, 0, 0 });
        public static SystemColor OrangeColor = new SystemColor("Orange", new int[] { 255, 150, 0 });
        public static SystemColor YellowColor = new SystemColor("Yellow", new int[] { 255, 255, 0 });
        public static SystemColor GreenColor = new SystemColor("Green", new int[] { 0, 255, 0 });
        public static SystemColor CyanColor = new SystemColor("Cyan", new int[] { 0, 255, 255 });
        public static SystemColor BlueColor = new SystemColor("Blue", new int[] { 0, 0, 255 });
        public static SystemColor PurpleColor = new SystemColor("Purple", new int[] { 150, 0, 255 });
        public static SystemColor MagentaColor = new SystemColor("Magenta", new int[] { 255, 0, 255 });
        public static SystemColor PinkColor = new SystemColor("Pink", new int[] { 255, 0, 150 });


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemColor(string n, int[] color)
        {
            Name = n;
            Color = color;
        }

        // -------------------------------------------------------------------
        // GetWinformsColor
        // -------------------------------------------------------------------

        public System.Drawing.Color GetWinformsColor()
        {
            return System.Drawing.Color.FromArgb(Color[0], Color[1], Color[2]);
        }

        // -------------------------------------------------------------------
        // GetMonogameColor
        // -------------------------------------------------------------------

        public Microsoft.Xna.Framework.Color GetMonogameColor()
        {
            return new Microsoft.Xna.Framework.Color(Color[0], Color[1], Color[2]);
        }
    }
}
