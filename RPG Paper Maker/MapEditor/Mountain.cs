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
        public bool DrawTop = true;
        public bool DrawBot = true;
        public bool DrawLeft = true;
        public bool DrawRight = true;


        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public Mountain CreateCopy()
        {
            Mountain newMountain = new Mountain();
            newMountain.SquareHeight = SquareHeight;
            newMountain.PixelHeight = PixelHeight;
            newMountain.Angle = Angle;
            newMountain.DrawTop = DrawTop;
            newMountain.DrawBot = DrawBot;
            newMountain.DrawLeft = DrawLeft;
            newMountain.DrawRight = DrawRight;

            return newMountain;
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(Mountains mountains, int[] coords, int[] portion)
        {
            if (mountains.TileOnTop(coords, portion)) DrawTop = false;
            if (mountains.TileOnBottom(coords, portion)) DrawBot = false;
            if (mountains.TileOnLeft(coords, portion)) DrawLeft = false;
            if (mountains.TileOnRight(coords, portion)) DrawRight = false;

            // Update & save update
            int[] portionToUpdate = MapEditor.Control.GetPortion(coords[0], coords[3]);
            MapEditor.Control.AddPortionToUpdate(portionToUpdate);
            MapEditor.Control.AddPortionToSave(portionToUpdate);
            WANOK.AddPortionsToAddCancel(MapEditor.Control.Map.MapInfos.RealMapName, MapEditor.Control.GetGlobalPortion(portionToUpdate));
        }
    }
}
