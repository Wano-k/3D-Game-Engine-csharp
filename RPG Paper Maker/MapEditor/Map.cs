using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RPG_Paper_Maker
{
    class Map
    {
        GraphicsDevice Device;
        VertexPositionColor[] GridVerticesArray;
        VertexBuffer vb;
        IndexBuffer ib;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Map(GraphicsDevice Device, string mapName)
        {
            this.Device = Device;

            // Create grid
            int size = 20;
            List<VertexPositionColor> gridVerticesList = new List<VertexPositionColor>();
            for (int i = 0; i <= size; i++)
            {
                foreach (VertexPositionColor vertex in CreateGridLine(0,i,size,i))
                {
                    gridVerticesList.Add(vertex);
                }
            }
            for (int i = 0; i <= size; i++)
            {
                foreach (VertexPositionColor vertex in CreateGridLine(i,0,i,size))
                {
                    gridVerticesList.Add(vertex);
                }
            }

            this.GridVerticesArray = gridVerticesList.ToArray();
            vb = new VertexBuffer(this.Device, VertexPositionTexture.VertexDeclaration, this.GridVerticesArray.Length, BufferUsage.None);
            vb.SetData(this.GridVerticesArray);
        }

        // -------------------------------------------------------------------
        // CreateGridLine
        // -------------------------------------------------------------------

        private VertexPositionColor[] CreateGridLine(int x1, int z1, int x2, int z2)
        {
            // Vertex Position and Texture
            return new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(x1, 0, z1), Color.White),
                new VertexPositionColor(new Vector3(x2, 0, z2), Color.White)
            };
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GameTime gameTime, BasicEffect effect)
        {
            // Effect settings
            effect.VertexColorEnabled = true;
            effect.World = Matrix.Identity * Matrix.CreateScale(16.0f,1.0f,16.0f);

            // Drawing grid
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.Device.DrawUserPrimitives(PrimitiveType.LineList, this.GridVerticesArray, 0, this.GridVerticesArray.Count<VertexPositionColor>() / 2);
            }
        }
    }
}
