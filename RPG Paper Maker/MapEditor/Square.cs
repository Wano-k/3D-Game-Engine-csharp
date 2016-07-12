using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class Square
    {
        private VertexBuffer VB;
        private VertexPositionTexture[] Vertices;
        private IndexBuffer IB;
        private int[] Indexes;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Square(GraphicsDevice device, Texture2D texture, int[] coordsTexture)
        {
            // Init buffers
            VB = new VertexBuffer(device, typeof(VertexPositionTexture), 4, BufferUsage.WriteOnly);
            IB = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, 6, BufferUsage.WriteOnly);

            // Create texture
            CreateTex(coordsTexture, texture);
        }

        // -------------------------------------------------------------------
        // CreateTex : coords = [x,y,width,height]
        // -------------------------------------------------------------------

        public void CreateTex(int[] coords, Texture2D texture)
        {
            // Texture coords
            float left = ((float)coords[0]) / texture.Width;
            float top = ((float)coords[1]) / texture.Height;
            float bot = ((float)(coords[1] + coords[3])) / texture.Height;
            float right = ((float)(coords[0] + coords[2])) / texture.Width;

            // Adjust in order to limit risk of textures flood
            float width = left + right;
            float height = top + bot;
            int coef = 10000;
            left += width / coef;
            right -= width / coef;
            top += height / coef;
            bot -= height / coef;

            // Vertex Position and Texture
            Vertices = new VertexPositionTexture[]
           {
               new VertexPositionTexture(WANOK.VERTICESFLOOR[0], new Vector2(left,top)),
               new VertexPositionTexture(WANOK.VERTICESFLOOR[1], new Vector2(right,top)),
               new VertexPositionTexture(WANOK.VERTICESFLOOR[2], new Vector2(right,bot)),
               new VertexPositionTexture(WANOK.VERTICESFLOOR[3], new Vector2(left,bot))
           };

            // Vertex Indexes
            Indexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };

            // Update buffers
            VB.SetData(Vertices);
            IB.SetData(Indexes);
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, GameTime gameTime, AlphaTestEffect effect, Texture2D texture, Vector3 position)
        {
            // Setting effect
            effect.Texture = texture;
            effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, 1.0f, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation(position.X, position.Y + 0.1f, position.Z);

            device.SetVertexBuffer(VB);
            device.Indices = IB;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, Vertices, 0, Vertices.Length, Indexes, 0, 2);
            }
        }

        // -------------------------------------------------------------------
        // DisposeBuffers
        // -------------------------------------------------------------------

        public void DisposeBuffers(GraphicsDevice device)
        {
            device.SetVertexBuffer(null);
            device.Indices = null;
            VB.Dispose();
            IB.Dispose();
        }
    }
}
