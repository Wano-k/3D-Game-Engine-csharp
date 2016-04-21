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
        private VertexPositionTexture[] verticesHero;
        private VertexBuffer vb;
        private IndexBuffer ib;
        private int[] indexes;
        private int Frame = 0, FrameInactive = 0, FrameTick = 0, FrameTickInactive = 0, FrameDuration = 200, FrameDurationInactive = 200;
        private int Frame_inactive = 0;
        private bool Act = true;


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
            this.vb = new VertexBuffer(this.device, typeof(VertexPositionTexture), 4, BufferUsage.WriteOnly);
            this.ib = new IndexBuffer(this.device, IndexElementSize.ThirtyTwoBits, 6, BufferUsage.WriteOnly);
            this.device.SetVertexBuffer(this.vb);
        }

        // -------------------------------------------------------------------
        // GetX
        // -------------------------------------------------------------------

        public int GetX()
        {
            return (int)((Position.X + 1) / WANOK.SQUARESIZE);
        }

        // -------------------------------------------------------------------
        // GetY
        // -------------------------------------------------------------------

        public int GetY()
        {
            return (int)((Position.Z + 1) / WANOK.SQUARESIZE);
        }

        // -------------------------------------------------------------------
        // CreateHeroWithTex : coords = [x,y,width,height]
        // -------------------------------------------------------------------

        private void CreateHeroWithTex(int[] coords, Texture2D texture)
        {
            // Texture coords
            float left = ((float)coords[0]) / texture.Width;
            float top = ((float)coords[1]) / texture.Height;
            float bot = ((float)(coords[1] + coords[3])) / texture.Height;
            float right = ((float)(coords[0] + coords[2])) / texture.Width;

            // Vertex Position and Texture
            this.verticesHero = new VertexPositionTexture[]
           {
               new VertexPositionTexture(WANOK.VERTICESSPRITE[0], new Vector2(left,top)),
               new VertexPositionTexture(WANOK.VERTICESSPRITE[1], new Vector2(right,top)),
               new VertexPositionTexture(WANOK.VERTICESSPRITE[2], new Vector2(right,bot)),
               new VertexPositionTexture(WANOK.VERTICESSPRITE[3], new Vector2(left,bot))
           };

            // Vertex Indexes
            this.indexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };

            // Update buffers
            this.vb.SetData(this.verticesHero);
            this.ib.SetData(this.indexes);
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(GameTime gameTime, Camera camera, Map map, KeyboardState kb)
        {
            double angle = camera.HorizontalAngle;
            int x = GetX(), y = GetY(), x_plus, z_plus;
            double speed = 1.1 * camera.RotateVelocity * (gameTime.ElapsedGameTime.Milliseconds) / 1000.0;

            // Updating diag speed
            if (kb.IsKeyDown(Keys.W) || kb.IsKeyDown(Keys.S)) // Up / Down
            {
                if (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.D)) // Left / Right
                {
                    speed *= 0.7;
                }
            }

            float previous_x = Position.X, previous_y = Position.Y, previous_z = Position.Z;
            if (kb.IsKeyDown(Keys.W))
            {
                x_plus = (int)(speed * (Math.Cos(angle * Math.PI / 180.0)));
                z_plus = (int)(speed * (Math.Sin(angle * Math.PI / 180.0)));
                Position.Z += z_plus;
                Position.X += x_plus;
                //if ((y > 0 && y_plus < 0) || (y < map.Size[1] && y_plus > 0)) Position.Y += y_plus;
                //if (y_plus == 0 && ((x > 0 && x_plus < 0) || (x < map.Size[0] && x_plus > 0))) Position.X += x_plus;
            }
            if (kb.IsKeyDown(Keys.S))
            {
                x_plus = (int)(speed * (Math.Cos(angle * Math.PI / 180.0)));
                z_plus = (int)(speed * (Math.Sin(angle * Math.PI / 180.0)));
                Position.Z -= z_plus;
                Position.X -= x_plus;
            }
            if (kb.IsKeyDown(Keys.A))
            {
                x_plus = (int)(speed * (Math.Cos((angle - 90.0) * Math.PI / 180.0)));
                z_plus = (int)(speed * (Math.Sin((angle - 90.0) * Math.PI / 180.0)));
                Position.Z += z_plus;
                Position.X += x_plus;
            }
            if (kb.IsKeyDown(Keys.D))
            {
                x_plus = (int)(speed * (Math.Cos((angle - 90.0) * Math.PI / 180.0)));
                z_plus = (int)(speed * (Math.Sin((angle - 90.0) * Math.PI / 180.0)));
                Position.Z -= z_plus;
                Position.X -= x_plus;
            }

            // Frame update
            if (previous_x != Position.X || previous_y != Position.Y || previous_z != Position.Z)
            {
                FrameInactive = 0;
                Act = false;
                FrameTick += gameTime.ElapsedGameTime.Milliseconds;
                if (FrameTick >= FrameDuration)
                {
                    Frame += 1;
                    if (Frame > 3) Frame = 0;
                    FrameTick = 0;
                }
            }
            else
            {
                Frame = 0;
                Act = true;
                FrameTickInactive += gameTime.ElapsedGameTime.Milliseconds;
                if (FrameTickInactive >= FrameDurationInactive)
                {
                    FrameInactive += 1;
                    if (FrameInactive > 3) FrameInactive = 0;
                    FrameTickInactive = 0;
                }
            }
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GameTime gameTime, Camera camera, BasicEffect effect)
        {
            
        }
    }
}
