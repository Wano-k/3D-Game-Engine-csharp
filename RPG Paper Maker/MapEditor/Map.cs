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
        VertexBuffer VBGrid;
        MapInfos MapInfos;
        //VertexBuffer VBMap;
        //IndexBuffer IBMap;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Map(GraphicsDevice device, string mapName)
        {
            Device = device;
            MapInfos = WANOK.LoadDatas<MapInfos>(WANOK.CurrentDir + "\\Content\\Datas\\Maps\\" + mapName + "\\infos.map");

            CreateGrid(MapInfos.Width, MapInfos.Height);
        }

        // -------------------------------------------------------------------
        // CreateGrid
        // -------------------------------------------------------------------

        public void CreateGrid(int width, int height)
        {
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
            VBGrid = new VertexBuffer(Device, typeof(VertexPositionColor), GridVerticesArray.Length, BufferUsage.WriteOnly);
            VBGrid.SetData(GridVerticesArray);
        }

        // -------------------------------------------------------------------
        // DisposeVertexBuffer
        // -------------------------------------------------------------------

        public void DisposeVertexBuffer()
        {
            Device.SetVertexBuffer(null);
            VBGrid.Dispose();
        }

        // -------------------------------------------------------------------
        // CreateGridLine
        // -------------------------------------------------------------------

        private VertexPositionColor[] CreateGridLine(int x1, int z1, int x2, int z2)
        {
            // Vertex Position and Texture
            return new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(x1, MapEditor.GridHeight, z1), Color.White),
                new VertexPositionColor(new Vector3(x2, MapEditor.GridHeight, z2), Color.White)
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
            Device.SetVertexBuffer(VBGrid);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Device.DrawPrimitives(PrimitiveType.LineList, 0, this.GridVerticesArray.Length / 2);
            }
        }
    }
}
