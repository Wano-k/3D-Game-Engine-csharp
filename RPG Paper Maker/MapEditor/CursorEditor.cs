using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class CursorEditor
    {
        GraphicsDevice device;
        public Vector3 Position;
        public Vector2 Size;
        private VertexPositionTexture[] vertices;
        private VertexBuffer VB;
        private IndexBuffer IB;
        private int[] indexes;
        private int Frame = 0, FrameTick = 0, FrameDuration = 100;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public CursorEditor(GraphicsDevice device)
        {
            this.device = device;

            // Position and size
            this.Position = new Vector3(0,0,0);
            this.Size = new Vector2(32, 32);

            // Init buffers
            VB = new VertexBuffer(this.device, typeof(VertexPositionTexture), 4, BufferUsage.WriteOnly);
            IB = new IndexBuffer(this.device, IndexElementSize.ThirtyTwoBits, 6, BufferUsage.WriteOnly);
            this.device.SetVertexBuffer(VB);
        }

        // -------------------------------------------------------------------
        // GetX
        // -------------------------------------------------------------------

        public int GetX()
        {
            return (int)((Position.X + 1) / WANOK.SQUARE_SIZE);
        }

        // -------------------------------------------------------------------
        // GetY
        // -------------------------------------------------------------------

        public int GetY()
        {
            return (int)((Position.Z + 1) / WANOK.SQUARE_SIZE);
        }

        // -------------------------------------------------------------------
        // CreateTex : coords = [x,y,width,height]
        // -------------------------------------------------------------------

        protected void CreateTex(int[] coords, Texture2D texture)
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
            this.vertices = new VertexPositionTexture[]
           {
               new VertexPositionTexture(WANOK.VERTICESFLOOR[0], new Vector2(left,top)),
               new VertexPositionTexture(WANOK.VERTICESFLOOR[1], new Vector2(right,top)),
               new VertexPositionTexture(WANOK.VERTICESFLOOR[2], new Vector2(right,bot)),
               new VertexPositionTexture(WANOK.VERTICESFLOOR[3], new Vector2(left,bot))
           };

            // Vertex Indexes
            this.indexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };

            // Update buffers
            VB.SetData(this.vertices);
            IB.SetData(this.indexes);
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(GameTime gameTime)
        {
            FrameTick += gameTime.ElapsedGameTime.Milliseconds;
            if (FrameTick >= FrameDuration)
            {
                Frame += 1;
                if (Frame > 3) Frame = 0;
                FrameTick = 0;
            }
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GameTime gameTime, BasicEffect effect)
        {
            // Setting effect
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            effect.Texture = MapEditor.TexCursor;
            effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, 1.0f, WANOK.SQUARE_SIZE);

            CreateTex(new int[] { WANOK.BASIC_SQUARE_SIZE * Frame, 0, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE }, MapEditor.TexCursor);

            device.SetVertexBuffer(VB);
            device.Indices = IB;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indexes, 0, 2);
            }
        }
    }
}
