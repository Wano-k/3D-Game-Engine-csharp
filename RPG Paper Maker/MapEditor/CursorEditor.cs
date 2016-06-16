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
        GraphicsDevice Device;
        public Vector3 Position;
        public Vector2 Size;
        private VertexPositionTexture[] vertices;
        private VertexBuffer VB;
        private IndexBuffer IB;
        private int[] indexes;
        private int Frame = 0, FrameTick = 0, CursorWait = 0;
        private const int CursorWaitDuration = 3, FrameDuration = 100;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public CursorEditor(GraphicsDevice device)
        {
            Device = device;

            // Position and size
            Position = new Vector3(0,0,0);
            Size = new Vector2(32, 32);

            // Init buffers
            VB = new VertexBuffer(Device, typeof(VertexPositionTexture), 4, BufferUsage.WriteOnly);
            IB = new IndexBuffer(Device, IndexElementSize.ThirtyTwoBits, 6, BufferUsage.WriteOnly);
            Device.SetVertexBuffer(VB);
        }

        // -------------------------------------------------------------------
        // Reset
        // -------------------------------------------------------------------

        public void Reset()
        {
            Position = new Vector3(0, 0, 0);
        }

        // -------------------------------------------------------------------
        // GetX
        // -------------------------------------------------------------------

        public int GetX()
        {
            return (int)((Position.X + 1) / WANOK.SQUARE_SIZE);
        }

        // -------------------------------------------------------------------
        // GetZ
        // -------------------------------------------------------------------

        public int GetZ()
        {
            return (int)((Position.Z + 1) / WANOK.SQUARE_SIZE);
        }

        // -------------------------------------------------------------------
        // GetPortion
        // -------------------------------------------------------------------

        public int[] GetPortion()
        {
            return new int[] { GetX() / WANOK.PORTION_SIZE, GetZ() / WANOK.PORTION_SIZE };
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

        public void Update(GameTime gameTime, Camera camera, MapInfos map)
        {
            double angle = camera.HorizontalAngle;
            int x = GetX(), y = GetZ(), x_plus, z_plus;

            if (WANOK.KeyboardManager.IsButtonDown(WANOK.Settings.KeyboardAssign.EditorMoveUp)
                || WANOK.KeyboardManager.IsButtonDown(WANOK.Settings.KeyboardAssign.EditorMoveDown)
                || WANOK.KeyboardManager.IsButtonDown(WANOK.Settings.KeyboardAssign.EditorMoveLeft)
                || WANOK.KeyboardManager.IsButtonDown(WANOK.Settings.KeyboardAssign.EditorMoveRight))
            {
                CursorWait = CursorWaitDuration;
            }

            if (WANOK.KeyboardManager.IsButtonDownFirstAndRepeat(WANOK.Settings.KeyboardAssign.EditorMoveUp, CursorWait)) // Up
            {
                x_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Cos(angle * Math.PI / 180.0))));
                z_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Sin(angle * Math.PI / 180.0))));
                if ((y > 0 && z_plus < 0) || (y < map.Height-1 && z_plus > 0)) Position.Z += z_plus;
                if (z_plus == 0 && ((x > 0 && x_plus < 0) || (x < map.Width-1 && x_plus > 0))) Position.X += x_plus;
            }  
            if (WANOK.KeyboardManager.IsButtonDownFirstAndRepeat(WANOK.Settings.KeyboardAssign.EditorMoveDown, CursorWait)) // Down
            {
                x_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Cos(angle * Math.PI / 180.0))));
                z_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Sin(angle * Math.PI / 180.0))));
                if ((y < map.Height - 1 && z_plus < 0) || (y > 0 && z_plus > 0)) Position.Z -= z_plus;
                if (z_plus == 0 && ((x < map.Width-1 && x_plus < 0) || (x > 0 && x_plus > 0))) Position.X -= x_plus;
            }
            if (WANOK.KeyboardManager.IsButtonDownFirstAndRepeat(WANOK.Settings.KeyboardAssign.EditorMoveLeft, CursorWait)) // Left
            {
                x_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Cos((angle - 90.0) * Math.PI / 180.0))));
                z_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Sin((angle - 90.0) * Math.PI / 180.0))));
                if ((x > 0 && x_plus < 0) || (x < map.Width-1 && x_plus > 0)) Position.X += x_plus;
                if (x_plus == 0 && ((y > 0 && z_plus < 0) || (y < map.Height-1 && z_plus > 0))) Position.Z += z_plus;
            }
            if (WANOK.KeyboardManager.IsButtonDownFirstAndRepeat(WANOK.Settings.KeyboardAssign.EditorMoveRight, CursorWait)) // Right
            {
                x_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Cos((angle - 90.0) * Math.PI / 180.0))));
                z_plus = (int)(WANOK.SQUARE_SIZE * (Math.Round(Math.Sin((angle - 90.0) * Math.PI / 180.0))));
                if ((x < map.Width - 1 && x_plus < 0) || (x > 0 && x_plus > 0)) Position.X -= x_plus;
                if (x_plus == 0 && ((y < map.Height-1 && z_plus < 0) || (y > 0 && z_plus > 0))) Position.Z -= z_plus;
            }
              
            // Frames update  
            FrameTick += gameTime.ElapsedGameTime.Milliseconds;
            if (FrameTick >= FrameDuration)
            {
                Frame += 1;
                if (Frame > 3) Frame = 0;
                FrameTick = 0;
            }

            if (CursorWait > 0) CursorWait--;
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
            effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, 1.0f, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation(Position.X, Position.Y, Position.Z); ;

            CreateTex(new int[] { WANOK.BASIC_SQUARE_SIZE * Frame, 0, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE }, MapEditor.TexCursor);

            Device.SetVertexBuffer(VB);
            Device.Indices = IB;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indexes, 0, 2);
            }
        }
    }
}
