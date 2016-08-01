using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    class Mountain
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
            DrawTop = !CanDraw(mountains.TileOnTop(coords, portion, height));
            DrawBot = !CanDraw(mountains.TileOnBottom(coords, portion, height));
            DrawLeft = !CanDraw(mountains.TileOnLeft(coords, portion, height));
            DrawRight = !CanDraw(mountains.TileOnRight(coords, portion, height));

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

        // -------------------------------------------------------------------
        // GetDistanceIntersection
        // -------------------------------------------------------------------

        public float? GetDistanceIntersection(Ray ray, Camera camera, int[] coords)
        {
            BoundingBox box = new BoundingBox(new Vector3(0, 0, 0), new Vector3(1, WANOK.GetPixelHeight(SquareHeight, PixelHeight), 1));
            Matrix inverse = Matrix.Invert(Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, 1.0f, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation(coords[0] * WANOK.SQUARE_SIZE, WANOK.GetCoordsPixelHeight(coords), coords[3] * WANOK.SQUARE_SIZE));
            ray.Position = Vector3.Transform(ray.Position, inverse);
            ray.Direction = Vector3.TransformNormal(ray.Direction, inverse);
            ray.Direction.Normalize();
            return ray.Intersects(box);
        }
    }
}
