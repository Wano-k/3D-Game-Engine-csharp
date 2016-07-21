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
        public int[] Tiles = new int[4];

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public Autotile CreateCopy()
        {
            Autotile newAutotile = new Autotile();
            newAutotile.Tiles[0] = Tiles[0];
            newAutotile.Tiles[1] = Tiles[1];
            newAutotile.Tiles[2] = Tiles[2];
            newAutotile.Tiles[3] = Tiles[3];

            return newAutotile;
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(Autotiles autotiles, int[] coords, int[] portion)
        {
            int num = 0;

            // Top left
            if (!autotiles.TileOnLeft(coords, portion) && !autotiles.TileOnTop(coords, portion)) num = 2;
            else if (!autotiles.TileOnTop(coords, portion) && autotiles.TileOnLeft(coords, portion)) num = 4;
            else if (!autotiles.TileOnLeft(coords, portion) && autotiles.TileOnTop(coords, portion)) num = 5;
            else if (autotiles.TileOnLeft(coords, portion) && autotiles.TileOnTop(coords, portion) && autotiles.TileOnTopLeft(coords, portion)) num = 3;
            else num = 1;
            Tiles[0] = Autotiles.AutotileBorder["A" + num.ToString()];

            // Top right
            if (!autotiles.TileOnRight(coords, portion) && !autotiles.TileOnTop(coords, portion)) num = 2;
            else if (!autotiles.TileOnTop(coords, portion) && autotiles.TileOnRight(coords, portion)) num = 4;
            else if (!autotiles.TileOnRight(coords, portion) && autotiles.TileOnTop(coords, portion)) num = 5;
            else if (autotiles.TileOnRight(coords, portion) && autotiles.TileOnTop(coords, portion) && autotiles.TileOnTopRight(coords, portion)) num = 3;
            else num = 1;
            Tiles[1] = Autotiles.AutotileBorder["B" + num.ToString()];

            // Bottom left
            if (!autotiles.TileOnLeft(coords, portion) && !autotiles.TileOnBottom(coords, portion)) num = 2;
            else if (!autotiles.TileOnBottom(coords, portion) && autotiles.TileOnLeft(coords, portion)) num = 4;
            else if (!autotiles.TileOnLeft(coords, portion) && autotiles.TileOnBottom(coords, portion)) num = 5;
            else if (autotiles.TileOnLeft(coords, portion) && autotiles.TileOnBottom(coords, portion) && autotiles.TileOnBottomLeft(coords, portion)) num = 3;
            else num = 1;
            Tiles[2] = Autotiles.AutotileBorder["C" + num.ToString()];

            // Bottom right
            if (!autotiles.TileOnRight(coords, portion) && !autotiles.TileOnBottom(coords, portion)) num = 2;
            else if (!autotiles.TileOnBottom(coords, portion) && autotiles.TileOnRight(coords, portion)) num = 4;
            else if (!autotiles.TileOnRight(coords, portion) && autotiles.TileOnBottom(coords, portion)) num = 5;
            else if (autotiles.TileOnRight(coords, portion) && autotiles.TileOnBottom(coords, portion) && autotiles.TileOnBottomRight(coords, portion)) num = 3;
            else num = 1;
            Tiles[3] = Autotiles.AutotileBorder["D" + num.ToString()];

            // Update & save update
            int[] portionToUpdate = MapEditor.Control.GetPortion(coords[0], coords[3]);
            MapEditor.Control.AddPortionToUpdate(portionToUpdate);
            MapEditor.Control.AddPortionToSave(portionToUpdate);
        }
    }
}
