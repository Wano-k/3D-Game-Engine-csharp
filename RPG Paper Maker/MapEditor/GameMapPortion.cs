using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    class GameMapPortion
    {
        public Dictionary<int[], int[]> Floors = new Dictionary<int[], int[]>(new IntArrayComparer());
        [NonSerialized()]
        VertexBuffer VBFloor;
        [NonSerialized()]
        VertexPositionTexture[] VerticesFloorArray;
        [NonSerialized()]
        IndexBuffer IBFloor;
        [NonSerialized()]
        int[] IndexesFloorArray;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public GameMapPortion()
        {
            Floors = new Dictionary<int[], int[]>(new IntArrayComparer());
        }

        // -------------------------------------------------------------------
        // IsEmpty
        // -------------------------------------------------------------------

        public bool IsEmpty()
        {
            return (Floors.Count == 0);
        }

        // -------------------------------------------------------------------
        // AddList
        // -------------------------------------------------------------------

        public void AddList<T,V>(Dictionary<T,List<V>> d, T key, V val)
        {
            if (!d.ContainsKey(key)) d[key] = new List<V>();
            d[key].Add(val);
        }

        // -------------------------------------------------------------------
        // GetFloorTexture
        // -------------------------------------------------------------------

        public int[] GetFloorTexture(int[] coords)
        {
            if (Floors.ContainsKey(coords)) return Floors[coords];

            return null;
        }

        // -------------------------------------------------------------------
        // AddFloor
        // -------------------------------------------------------------------

        public void AddFloor(int[] coords, int[] newTexture)
        {
            // Adding the new floor
            Floors[coords] = newTexture;
        }

        // -------------------------------------------------------------------
        // GenFloor
        // -------------------------------------------------------------------

        public void GenFloor(GraphicsDevice device, Texture2D texture)
        {
            if (VBFloor != null)
            {
                device.SetVertexBuffer(null);
                device.Indices = null;
                VBFloor.Dispose();
                IBFloor.Dispose();
            }
            CreatePortionFloor(device, texture);
        }

        // -------------------------------------------------------------------
        // CreatePortionFloor
        // -------------------------------------------------------------------

        public void CreatePortionFloor(GraphicsDevice device, Texture2D texture)
        {
            // Building vertex buffer indexed
            List<VertexPositionTexture> verticesList = new List<VertexPositionTexture>();
            List<int> indexesList = new List<int>();
            int[] indexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };
            int offset = 0;
            foreach (KeyValuePair<int[], int[]> entry in Floors)
            {
                foreach (VertexPositionTexture vertex in CreateFloorWithTex(texture, entry.Key[0], entry.Key[2], entry.Value))
                {
                    verticesList.Add(vertex);
                }
                for (int n = 0; n < 6; n++)
                {
                    indexesList.Add(indexes[n] + offset);
                }
                offset += 4;
            }

            VerticesFloorArray = verticesList.ToArray();
            IndexesFloorArray = indexesList.ToArray();
            IBFloor = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, IndexesFloorArray.Length, BufferUsage.None);
            IBFloor.SetData(IndexesFloorArray);
            VBFloor = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, VerticesFloorArray.Length, BufferUsage.None);
            VBFloor.SetData(VerticesFloorArray);
        }

        // -------------------------------------------------------------------
        // CreateFloorWithTex : coords = [x,y,width,height]
        // -------------------------------------------------------------------

        protected VertexPositionTexture[] CreateFloorWithTex(Texture2D texture, int x, int z, int[] coords)
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

        public void Draw(GraphicsDevice device, BasicEffect effect, Texture2D texture)
        {
            if (VBFloor != null)
            {
                // Effect settings
                effect.Texture = texture;

                // Drawing
                device.SetVertexBuffer(VBFloor);
                device.Indices = IBFloor;
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, VerticesFloorArray, 0, VerticesFloorArray.Length, IndexesFloorArray, 0, VerticesFloorArray.Length / 2);
                }
            }
        }

        // -------------------------------------------------------------------
        // DisposeBuffers
        // -------------------------------------------------------------------

        public void DisposeBuffers(GraphicsDevice device)
        {
            if (VBFloor != null)
            {
                device.SetVertexBuffer(null);
                device.Indices = null;
                VBFloor.Dispose();
                IBFloor.Dispose();
            }
        }
    }
}
