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
        GraphicsDevice device;
        List<VertexPositionTexture> vertexList;
        List<int> indexesList;
        VertexBuffer vb;
        IndexBuffer ib;
        int[] indexes;
        public Game_map_portion[] portions;
        public int[] Size = new int[2];


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Map(GraphicsDevice device, string mapName)
        {
            this.device = device;

            // Size
            Size[0] = (int)(WANOK.PORTION_RADIUS * WANOK.SQUARESIZE);
            Size[1] = (int)(WANOK.PORTION_RADIUS * WANOK.SQUARESIZE);

            // Init portions
            portions = new Game_map_portion[WANOK.PORTION_RADIUS*WANOK.PORTION_RADIUS];
            int k = 0;
            for (int i = 0; i < WANOK.PORTION_RADIUS; i++)
            {
                for (int j = 0; j < WANOK.PORTION_RADIUS; j++)
                {
                    /*
                    portions[k] = new Game_map_portion(i*WANOK.PORTIONSIZE,j*WANOK.PORTIONSIZE);
                    string json = JsonConvert.SerializeObject(portions[k]);
                    FileStream fs = new FileStream("Content/Datas/Maps/MAP0001/"+i+"-"+j+".JSON", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(json);
                    sw.Close();
                    fs.Close();
                    */
                    FileStream fs = new FileStream("Content/Datas/Maps/MAP0001/" + i + "-" + j + ".JSON", FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string json = sr.ReadToEnd();
                    portions[k] = JsonConvert.DeserializeObject<Game_map_portion>(json);
                    k++;
                }
            }

            // Building vertex buffer indexed
            this.vertexList = new List<VertexPositionTexture>();
            this.indexesList = new List<int>();
            this.indexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };
            int offset = 0;
            k = 0;
            for (int i = 0; i < WANOK.PORTION_RADIUS; i++)
            {
                for (int j = 0; j < WANOK.PORTION_RADIUS; j++)
                {
                    for (int l = 0; l < portions[k].texture_floors.Count; l++)
                    {
                        for (int m = 0; m < portions[k].floors[l].Count; m++)
                        {
                            foreach (VertexPositionTexture vertex in CreateFloorWithTex(portions[k].floors[l][m][0], portions[k].floors[l][m][1], portions[k].texture_floors[l]))
                            {
                                vertexList.Add(vertex);
                            }
                            for (int n = 0; n < 6; n++)
                            {
                                indexesList.Add(indexes[n] + offset);
                            }
                            offset += 4;
                        }
                    }
                    k++;
                }
            }

            this.ib = new IndexBuffer(this.device, IndexElementSize.ThirtyTwoBits, indexesList.Count, BufferUsage.None);
            this.ib.SetData(indexesList.ToArray());
            vb = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, vertexList.Count, BufferUsage.None);
            vb.SetData(vertexList.ToArray());
        }

        // -------------------------------------------------------------------
        // CreateFloorWithTex : coords = [x,y,width,height]
        // -------------------------------------------------------------------

        private VertexPositionTexture[] CreateFloorWithTex(int x, int z, int[] coords)
        {
            // Texture coords
            float left = ((float)coords[0]) / MapEditor.currentFloorTex.Width;
            float top = ((float)coords[1]) / MapEditor.currentFloorTex.Height;
            float bot = ((float)(coords[1]+coords[3])) / MapEditor.currentFloorTex.Height;
            float right = ((float)(coords[0] + coords[2])) / MapEditor.currentFloorTex.Width;

            // Adjust in order to limit risk of textures flood
            float width = left + right;
            float height = top + bot;
            int coef = 10000;
            left += width / coef;
            right -= width / coef;
            top += height / coef;
            bot -= height / coef;

            // Vertex Position and Texture
            return new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(x, 0, z), new Vector2(left, top)),
                new VertexPositionTexture(new Vector3(x+1, 0, z), new Vector2(right, top)),
                new VertexPositionTexture(new Vector3(x+1, 0, z+1), new Vector2(right, bot)),
                new VertexPositionTexture(new Vector3(x, 0, z+1), new Vector2(left, bot))
            };
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GameTime gameTime, BasicEffect effect)
        {
            // Effeect settings
            effect.Texture = MapEditor.currentFloorTex;
            effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARESIZE, 1.0f, WANOK.SQUARESIZE);

            // Converting to array
            VertexPositionTexture[] verticesArray = this.vertexList.ToArray();
            int[] indexesArray = this.indexesList.ToArray();


            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, verticesArray, 0, verticesArray.Length, indexesArray, 0, verticesArray.Length / 2);
            }
        }
    }
}
