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
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, AlphaTestEffect effect, VertexPositionTexture[] VerticesArray, int[] IndexesArray, Camera camera, int[]coords)
        {
            float height = coords[1] * WANOK.SQUARE_SIZE + coords[2];
            switch (Type)
            {
                case DrawType.FaceSprite:
                    effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation(-WANOK.SQUARE_SIZE / 2, 0, 0) * Matrix.CreateRotationY((float)((-camera.HorizontalAngle - 90) * Math.PI / 180.0)) * Matrix.CreateTranslation(coords[0] * WANOK.SQUARE_SIZE, height, coords[3] * WANOK.SQUARE_SIZE);
                    break;
                default:
                    effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation(coords[0] * WANOK.SQUARE_SIZE, height, coords[3] * WANOK.SQUARE_SIZE);
                    break;
            }

            DrawOneSprite(device, effect, VerticesArray, IndexesArray);
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
    }
}
