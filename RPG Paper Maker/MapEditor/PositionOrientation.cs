using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    class PositionOrientation
    {
        int[] Position = new int[] { 0, 0 };
        int Orientation = 0;


        public override bool Equals(object obj)
        {
            return Position.SequenceEqual(((PositionOrientation)obj).Position) && Orientation == ((PositionOrientation)obj).Orientation;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // -------------------------------------------------------------------
        // IsEqual
        // -------------------------------------------------------------------

        public bool IsEqual(PositionOrientation positionOrientation)
        {
            return Position.SequenceEqual(positionOrientation.Position) && Orientation == positionOrientation.Orientation;
        }
    }
}
