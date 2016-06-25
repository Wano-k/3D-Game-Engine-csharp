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
        public static SystemColor BlackColor = new SystemColor(1, "Black", new int[] { 0, 0, 0 });
        public static SystemColor BlackGrayColor = new SystemColor(2, "Black-Gray", new int[] { 32, 32, 32 });
        public static SystemColor SilverColor = new SystemColor(3, "Silver", new int[] { 185, 185, 185 });
        public static SystemColor WhiteColor = new SystemColor(4, "White", new int[] { 255, 255, 255 });
        public static SystemColor RedColor = new SystemColor(5, "Red", new int[] { 255, 0, 0 });
        public static SystemColor OrangeColor = new SystemColor(6, "Orange", new int[] { 255, 150, 0 });
        public static SystemColor YellowColor = new SystemColor(7, "Yellow", new int[] { 255, 255, 0 });
        public static SystemColor GreenColor = new SystemColor(8, "Green", new int[] { 0, 255, 0 });
        public static SystemColor CyanColor = new SystemColor(9, "Cyan", new int[] { 0, 255, 255 });
        public static SystemColor BlueColor = new SystemColor(10, "Blue", new int[] { 0, 0, 255 });
        public static SystemColor PurpleColor = new SystemColor(11, "Purple", new int[] { 150, 0, 255 });
        public static SystemColor MagentaColor = new SystemColor(12, "Magenta", new int[] { 255, 0, 255 });
        public static SystemColor PinkColor = new SystemColor(13, "Pink", new int[] { 255, 0, 150 });


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemColor()
        {
            GetDefaultColor(-1);
        }

        public SystemColor(int id, string n, int[] color)
        {
            Id = id;
            Name = n;
            Color = color;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override SuperListItem CreateCopy()
        {
            return new SystemColor(Id, Name, Color);
        }

        // -------------------------------------------------------------------
        // GetDefaultColor
        // -------------------------------------------------------------------

        public static SystemColor GetDefaultColor(int id)
        {
            return new SystemColor(id, "", new int[] { 0, 0, 0 });
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

        public static Microsoft.Xna.Framework.Color GetMonogameColor(SystemColor c)
        {
            return new Microsoft.Xna.Framework.Color(c.Color[0], c.Color[1], c.Color[2]);
        }

        // -------------------------------------------------------------------
        // ToString
        // -------------------------------------------------------------------

        public override string ToString()
        {
            return WANOK.GetStringList(Id, Name);
        }
    }
}
