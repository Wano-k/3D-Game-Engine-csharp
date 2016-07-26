using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class Mountain
    {
        public int SquareHeight;
        public int PixelHeight;
        public int Angle;

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public Mountain CreateCopy()
        {
            Mountain newMountain = new Mountain();
            newMountain.SquareHeight = SquareHeight;
            newMountain.PixelHeight = PixelHeight;
            newMountain.Angle = Angle;

            return newMountain;
        }
    }
}
