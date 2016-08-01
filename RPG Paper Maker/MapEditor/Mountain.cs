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
        // Constructor
        // -------------------------------------------------------------------

        public Mountain(int squareHeight, int pixelHeight, int angle)
        {
            SquareHeight = squareHeight;
            PixelHeight = pixelHeight;
            Angle = angle;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public Mountain CreateCopy()
        {
            Mountain newMountain = CreatePartialCopy();
            newMountain.DrawTop = DrawTop;
            newMountain.DrawBot = DrawBot;
            newMountain.DrawLeft = DrawLeft;
            newMountain.DrawRight = DrawRight;

            return newMountain;
        }

        public Mountain CreatePartialCopy()
        {
            return new Mountain(SquareHeight, PixelHeight, Angle);
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(Mountains mountains, int[] coords, int[] portion, int height)
        {
            if (CanDraw(mountains.TileOnTop(coords, portion, height))) DrawTop = false;
            if (CanDraw(mountains.TileOnBottom(coords, portion, height))) DrawBot = false;
            if (CanDraw(mountains.TileOnLeft(coords, portion, height))) DrawLeft = false;
            if (CanDraw(mountains.TileOnRight(coords, portion, height))) DrawRight = false;

            // Update & save update
            int[] portionToUpdate = MapEditor.Control.GetPortion(coords[0], coords[3]);
            MapEditor.Control.AddPortionToUpdate(portionToUpdate);
            MapEditor.Control.AddPortionToSave(portionToUpdate);
            WANOK.AddPortionsToAddCancel(MapEditor.Control.Map.MapInfos.RealMapName, MapEditor.Control.GetGlobalPortion(portionToUpdate));
        }

        // -------------------------------------------------------------------
        // CanDraw
        // -------------------------------------------------------------------

        public bool CanDraw(Mountain mountain)
        {
            return (mountain != null && WANOK.GetPixelHeight(SquareHeight, PixelHeight) <= WANOK.GetPixelHeight(mountain.SquareHeight, mountain.PixelHeight));
        }
    }
}
