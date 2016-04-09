using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class MapEditor : WinFormsGraphicsDevice.MapEditorControl
    {
        public static Texture2D currentFloorTex;

        // NOTE : Temporary drawing a triangle, only 3D tests

        VertexPositionColor[] vertices;
        private void CreateTriangle()
        {
            this.vertices = new VertexPositionColor[]
            {
               new VertexPositionColor( new Vector3(-1,-1,0), Color.Red),
               new VertexPositionColor( new Vector3(0,1,0), Color.Green),
               new VertexPositionColor( new Vector3(1,-1,0), Color.Blue),
            };
        }

        Matrix View;
        Matrix Projection;
        Matrix World;

        Vector3 CameraPosition = new Vector3(0.0f, 0.0f, 5.0f);
        Vector3 CameraTarget = Vector3.Zero;
        Vector3 CameraUp = Vector3.Up;
        BasicEffect effect;

        private void CreateCamera()
        {
            View = Matrix.CreateLookAt(CameraPosition, CameraTarget, CameraUp);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, this.GraphicsDevice.Viewport.Width / this.GraphicsDevice.Viewport.Height, 0.01f, 1000.0f);
            World = Matrix.Identity;
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.CreateTriangle();
            this.CreateCamera();
            this.effect = new BasicEffect(this.GraphicsDevice);
        }

        protected override void Draw()
        {
            base.Draw();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            effect.View = View;
            effect.Projection = Projection;
            effect.World = World;
            effect.VertexColorEnabled = true;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length / 3);
            }
        }
    }
}
