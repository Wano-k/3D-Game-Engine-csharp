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

            LoadMap(mapName);

            vb = new VertexBuffer(this.Device, VertexPositionTexture.VertexDeclaration, this.GridVerticesArray.Length, BufferUsage.None);
            vb.SetData(this.GridVerticesArray);
        }

        // -------------------------------------------------------------------
        // ReLoadMap
        // -------------------------------------------------------------------

        public void LoadMap(String mapName)
        {
            // Create grid

            int width = 20;
            int height = 20;
            List<VertexPositionColor> gridVerticesList = new List<VertexPositionColor>();
            // Columns
            for (int i = 0; i <= width; i++)
            {
                foreach (VertexPositionColor vertex in CreateGridLine(i, 0, i, height))
                {
                    gridVerticesList.Add(vertex);
                }
            }
            // Rows
            for (int i = 0; i <= height; i++)
            {
                foreach (VertexPositionColor vertex in CreateGridLine(0, i, width, i))
                {
                    gridVerticesList.Add(vertex);
                }
            } 
            this.GridVerticesArray = gridVerticesList.ToArray();
        }

        // -------------------------------------------------------------------
        // CreateGridLine
        // -------------------------------------------------------------------

        private VertexPositionColor[] CreateGridLine(int x1, int z1, int x2, int z2)
        {
            // Vertex Position and Texture
            return new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(x1, MapEditor.Height, z1), Color.White),
                new VertexPositionColor(new Vector3(x2, MapEditor.Height, z2), Color.White)
            };
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GameTime gameTime, BasicEffect effect)
        {
            // Effect settings
            effect.VertexColorEnabled = true;
            effect.TextureEnabled = false;
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
