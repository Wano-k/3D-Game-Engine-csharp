using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    class Sprite
    {
        DrawType Type;
        int[] Position = new int[] { 0, 0 };
        int Orientation = 0;



        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Sprite(DrawType type, int[] position, int orientation)
        {
            Type = type;
            Position = position;
            Orientation = orientation;
        }

        // -------------------------------------------------------------------
        // Equals
        // -------------------------------------------------------------------

        public override bool Equals(object obj)
        {
            return Type == ((Sprite)obj).Type && Position.SequenceEqual(((Sprite)obj).Position) && Orientation == ((Sprite)obj).Orientation;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // -------------------------------------------------------------------
        // GetWorldEffect
        // -------------------------------------------------------------------

        public Matrix GetFirstQuadWorldEffect(Camera camera, int[] coords, int widthSprite, int heightSprite, float height)
        {
            switch (Type)
            {
                case DrawType.FaceSprite:
                    return Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation(-widthSprite * WANOK.SQUARE_SIZE / 2, 0, 0) * Matrix.CreateRotationY((float)((-camera.HorizontalAngle - 90) * Math.PI / 180.0)) * Matrix.CreateTranslation(coords[0] * WANOK.SQUARE_SIZE + (WANOK.SQUARE_SIZE / 2), height, coords[3] * WANOK.SQUARE_SIZE + (WANOK.SQUARE_SIZE / 2));
                case DrawType.OnFloorSprite:
                    return Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation(0, 0, 0) * Matrix.CreateRotationX((float)(90 * Math.PI / 180)) * Matrix.CreateTranslation((-widthSprite * WANOK.SQUARE_SIZE / 2) + (coords[0] * WANOK.SQUARE_SIZE) + (WANOK.SQUARE_SIZE / 2), height + 0.1f, (-heightSprite * WANOK.SQUARE_SIZE / 2) + (coords[3] * WANOK.SQUARE_SIZE) + (WANOK.SQUARE_SIZE / 2));
                default:
                    return Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation((-widthSprite * WANOK.SQUARE_SIZE / 2) + (coords[0] * WANOK.SQUARE_SIZE) + (WANOK.SQUARE_SIZE / 2), height, coords[3] * WANOK.SQUARE_SIZE + (WANOK.SQUARE_SIZE / 2));
            }
        }

        public Matrix GetOtherQuadWorldEffect(int[] coords, int width, float height, int rotation)
        {
            return Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation(-width * WANOK.SQUARE_SIZE / 2, 0, 0) * Matrix.CreateRotationY((float)((rotation) * Math.PI / 180.0)) * Matrix.CreateTranslation(coords[0] * WANOK.SQUARE_SIZE + (WANOK.SQUARE_SIZE / 2), height, coords[3] * WANOK.SQUARE_SIZE + (WANOK.SQUARE_SIZE / 2));
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, AlphaTestEffect effect, VertexPositionTexture[] VerticesArray, int[] IndexesArray, Camera camera, int[]coords, int widthSprite, int heightSprite)
        {
            float height = coords[1] * WANOK.SQUARE_SIZE + coords[2];
            effect.World = GetFirstQuadWorldEffect(camera, coords, widthSprite, heightSprite, height);
            DrawOneSprite(device, effect, VerticesArray, IndexesArray);

            if (Type == DrawType.DoubleSprite ||Type == DrawType.QuadraSprite)
            {
                effect.World = GetOtherQuadWorldEffect(coords, widthSprite, height, 90);
                DrawOneSprite(device, effect, VerticesArray, IndexesArray);

                if (Type == DrawType.QuadraSprite)
                {
                    effect.World = GetOtherQuadWorldEffect(coords, widthSprite, height, 45);
                    DrawOneSprite(device, effect, VerticesArray, IndexesArray);
                    effect.World = GetOtherQuadWorldEffect(coords, widthSprite, height, -45);
                    DrawOneSprite(device, effect, VerticesArray, IndexesArray);
                }
            }
        }

        // -------------------------------------------------------------------
        // DrawOneSprite
        // -------------------------------------------------------------------

        public void DrawOneSprite(GraphicsDevice device, AlphaTestEffect effect, VertexPositionTexture[] VerticesArray, int[] IndexesArray)
        {
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, VerticesArray, 0, VerticesArray.Length, IndexesArray, 0, VerticesArray.Length / 2);
            }
        }

        // -------------------------------------------------------------------
        // GetCompleteDistanceIntersection
        // -------------------------------------------------------------------

        public float? GetCompleteDistanceIntersection(Ray ray, Camera camera, int[] coords, int widthSprite, int heightSprite)
        {
            float height = coords[1] * WANOK.SQUARE_SIZE + coords[2];
            float? newDistance = GetDistanceIntersection(new Ray(ray.Position, ray.Direction), camera, coords, widthSprite, heightSprite, height, GetFirstQuadWorldEffect(camera, coords, widthSprite, heightSprite, height));

            if (newDistance == null && (Type == DrawType.DoubleSprite || Type == DrawType.QuadraSprite))
            {
                newDistance = GetDistanceIntersection(new Ray(ray.Position, ray.Direction), camera, coords, widthSprite, heightSprite, height, GetOtherQuadWorldEffect(coords, widthSprite, height, 90));
                if (Type == DrawType.QuadraSprite)
                {
                   if (newDistance == null) newDistance = GetDistanceIntersection(new Ray(ray.Position, ray.Direction), camera, coords, widthSprite, heightSprite, height, GetOtherQuadWorldEffect(coords, widthSprite, height, 45));
                   if (newDistance == null) newDistance = GetDistanceIntersection(new Ray(ray.Position, ray.Direction), camera, coords, widthSprite, heightSprite, height, GetOtherQuadWorldEffect(coords, widthSprite, height, -45));
                }
            }

            return newDistance;
        }

        // -------------------------------------------------------------------
        // GetDistanceIntersection
        // -------------------------------------------------------------------

        public float? GetDistanceIntersection(Ray ray, Camera camera, int[] coords, int widthSprite, int heightSprite, float height, Matrix world)
        {
            BoundingBox box = new BoundingBox(new Vector3(0, 0, 0), new Vector3(widthSprite, heightSprite, 1));
            Matrix inverse = Matrix.Invert(world);
            ray.Position = Vector3.Transform(ray.Position, inverse);
            ray.Direction = Vector3.TransformNormal(ray.Direction, inverse);
            ray.Direction.Normalize();
            return ray.Intersects(box);
        }
    }
}
