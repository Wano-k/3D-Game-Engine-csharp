using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class Autotile
    {
        public int[] Coords, Tiles = new int[4];


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Autotile(int[] coords)
        {
            Coords = coords;
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(Autotiles autotiles, int[] portion)
        {
            int num = 0;

            // Top left
            if (!autotiles.TileOnLeft(Coords, portion) && !autotiles.TileOnTop(Coords, portion)) num = 2;
            else if (!autotiles.TileOnTop(Coords, portion) && autotiles.TileOnLeft(Coords, portion)) num = 4;
            else if (!autotiles.TileOnLeft(Coords, portion) && autotiles.TileOnTop(Coords, portion)) num = 5;
            else if (autotiles.TileOnLeft(Coords, portion) && autotiles.TileOnTop(Coords, portion) && autotiles.TileOnTopLeft(Coords, portion)) num = 3;
            else num = 1;
            Tiles[0] = Autotiles.AutotileBorder["A" + num.ToString()];

            // Top right
            if (!autotiles.TileOnRight(Coords, portion) && !autotiles.TileOnTop(Coords, portion)) num = 2;
            else if (!autotiles.TileOnTop(Coords, portion) && autotiles.TileOnRight(Coords, portion)) num = 4;
            else if (!autotiles.TileOnRight(Coords, portion) && autotiles.TileOnTop(Coords, portion)) num = 5;
            else if (autotiles.TileOnRight(Coords, portion) && autotiles.TileOnTop(Coords, portion) && autotiles.TileOnTopRight(Coords, portion)) num = 3;
            else num = 1;
            Tiles[1] = Autotiles.AutotileBorder["B" + num.ToString()];

            // Bottom left
            if (!autotiles.TileOnLeft(Coords, portion) && !autotiles.TileOnBottom(Coords, portion)) num = 2;
            else if (!autotiles.TileOnBottom(Coords, portion) && autotiles.TileOnLeft(Coords, portion)) num = 4;
            else if (!autotiles.TileOnLeft(Coords, portion) && autotiles.TileOnBottom(Coords, portion)) num = 5;
            else if (autotiles.TileOnLeft(Coords, portion) && autotiles.TileOnBottom(Coords, portion) && autotiles.TileOnBottomLeft(Coords, portion)) num = 3;
            else num = 1;
            Tiles[2] = Autotiles.AutotileBorder["C" + num.ToString()];

            // Bottom right
            if (!autotiles.TileOnRight(Coords, portion) && !autotiles.TileOnBottom(Coords, portion)) num = 2;
            else if (!autotiles.TileOnBottom(Coords, portion) && autotiles.TileOnRight(Coords, portion)) num = 4;
            else if (!autotiles.TileOnRight(Coords, portion) && autotiles.TileOnBottom(Coords, portion)) num = 5;
            else if (autotiles.TileOnRight(Coords, portion) && autotiles.TileOnBottom(Coords, portion) && autotiles.TileOnBottomRight(Coords, portion)) num = 3;
            else num = 1;
            Tiles[3] = Autotiles.AutotileBorder["D" + num.ToString()];

            // Update & save update
            int[] portionToUpdate = MapEditor.Control.GetPortion(Coords[0], Coords[3]);
            MapEditor.Control.AddPortionToUpdate(portionToUpdate);
            MapEditor.Control.AddPortionToSave(portionToUpdate);
        }
    }
}
