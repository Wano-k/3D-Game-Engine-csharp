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
        public Dictionary<int[], int[]> Floors;
        public Dictionary<int, Autotiles> Autotiles;

        // Floors
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
            Autotiles = new Dictionary<int, Autotiles>();
        }

        // -------------------------------------------------------------------
        // IsEmpty
        // -------------------------------------------------------------------

        public bool IsEmpty()
        {
            return (Floors.Count == 0 && Autotiles.Count == 0);
        }

        // -------------------------------------------------------------------
        // ContainsFloor
        // -------------------------------------------------------------------

        public int[] ContainsFloor(int[] coords)
        {
            if (Floors.ContainsKey(coords)) return Floors[coords];

            return new int[] { 0, 0, 0, 0 };
        }

        // -------------------------------------------------------------------
        // ContainsAutotiles
        // -------------------------------------------------------------------

        public int ContainsAutotiles(int[] coords)
        {
            foreach (KeyValuePair<int, Autotiles> entry in Autotiles)
            {
                if (entry.Value.Tiles.ContainsKey(coords)) return entry.Key;
            }

            return -1;
        }

        // -------------------------------------------------------------------
        // AddFloor
        // -------------------------------------------------------------------

        public bool AddFloor(int[] coords, int[] newTexture)
        {
            bool modified = false;
            int[] beforeTexture = ContainsFloor(coords);
            if (beforeTexture.SequenceEqual(new int[] { 0, 0, 0, 0 }))
            {
                modified = true;
                int beforeId = ContainsAutotiles(coords);
                if (beforeId != -1)
                {
                    Autotiles[beforeId].Remove(coords);
                }
            }

            // Adding the new floor
            Floors[coords] = newTexture;

            return modified || !beforeTexture.SequenceEqual(newTexture);
        }

        // -------------------------------------------------------------------
        // AddAutotile
        // -------------------------------------------------------------------

        public bool AddAutotile(int[] coords, int newId, bool update)
        {
            bool modified = false;
            int beforeId = ContainsAutotiles(coords);
            if (beforeId == -1)
            {
                modified = true;
                int[] beforeTexture = ContainsFloor(coords);
                if (!beforeTexture.SequenceEqual(new int[] { 0, 0, 0, 0 }))
                {
                    Floors.Remove(coords);
                }
            }
            else
            {
                Autotiles[beforeId].Remove(coords, update);
            }

            // Adding the new autotile
            if (!Autotiles.ContainsKey(newId)) Autotiles[newId] = new Autotiles(newId);
            Autotiles[newId].Add(coords, update);

            return modified || beforeId != newId;
        }

        // -------------------------------------------------------------------
        // RemoveFloor
        // -------------------------------------------------------------------

        public bool RemoveFloor(int[] coords)
        {
            bool modified = false;

            // Floors
            if (!ContainsFloor(coords).SequenceEqual(new int[] { 0, 0, 0, 0 }))
            {
                modified = true;
                Floors.Remove(coords);
            }

            // Autotiles
            if (!modified)
            {
                int beforeId = ContainsAutotiles(coords);
                if (beforeId != -1)
                {
                    modified = true;
                    Autotiles[beforeId].Remove(coords);
                }
            }

            return modified;
        }

        // -------------------------------------------------------------------
        // Generate Buffers
        // -------------------------------------------------------------------

        #region Floors

        public void GenFloor(GraphicsDevice device, Texture2D texture)
        {
            DisposeBuffersFloor(device);
            if (Floors.Count > 0) CreatePortionFloor(device, texture);
        }

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
                foreach (VertexPositionTexture vertex in CreateFloorWithTex(texture, entry.Key[0], (entry.Key[1] * WANOK.SQUARE_SIZE) + entry.Key[2], entry.Key[3], entry.Value))
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

        protected VertexPositionTexture[] CreateFloorWithTex(Texture2D texture, int x, int y, int z, int[] coords)
        {
            // Texture coords
            float left = ((float)coords[0] * WANOK.SQUARE_SIZE) / texture.Width;
            float top = ((float)coords[1] * WANOK.SQUARE_SIZE) / texture.Height;
            float bot = ((float)(coords[1] + coords[3]) * WANOK.SQUARE_SIZE) / texture.Height;
            float right = ((float)(coords[0] + coords[2]) * WANOK.SQUARE_SIZE) / texture.Width;

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
                new VertexPositionTexture(new Vector3(x, y, z), new Vector2(left, top)),
                new VertexPositionTexture(new Vector3(x+1, y, z), new Vector2(right, top)),
                new VertexPositionTexture(new Vector3(x+1, y, z+1), new Vector2(right, bot)),
                new VertexPositionTexture(new Vector3(x, y, z+1), new Vector2(left, bot))
            };
        }

        #endregion

        #region Autotiles

        public void GenAutotiles(GraphicsDevice device)
        {
            foreach (Autotiles autotiles in Autotiles.Values)
            {
                autotiles.GenAutotiles(device);
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, BasicEffect effect, Texture2D texture)
        {
            // Drawing Floors
            if (VBFloor != null)
            {
                effect.Texture = texture;

                device.SetVertexBuffer(VBFloor);
                device.Indices = IBFloor;
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, VerticesFloorArray, 0, VerticesFloorArray.Length, IndexesFloorArray, 0, VerticesFloorArray.Length / 2);
                }
            }

            // Drawing Autotiles
            foreach (KeyValuePair<int, Autotiles> entry in Autotiles)
            {
                entry.Value.Draw(device, effect);
            }
        }

        // -------------------------------------------------------------------
        // UpdateDictionaries
        // -------------------------------------------------------------------

        public void UpdateDictionaries(GraphicsDevice device)
        {
            // Autotiles
            List<int> autotilesToDelete = new List<int>();
            foreach (int id in Autotiles.Keys)
            {
                if (Autotiles[id].IsEmpty()) autotilesToDelete.Add(id);
            }
            for (int i = 0; i < autotilesToDelete.Count; i++)
            {
                Autotiles[autotilesToDelete[i]].DisposeBuffers(device);
                Autotiles.Remove(autotilesToDelete[i]);
            }
        }

        // -------------------------------------------------------------------
        // DisposeBuffers
        // -------------------------------------------------------------------

        public void DisposeBuffers(GraphicsDevice device, bool nullable = true)
        {
            DisposeBuffersFloor(device, nullable);
            foreach (Autotiles autotiles in Autotiles.Values)
            {
                autotiles.DisposeBuffers(device, nullable);
            }
        }

        // -------------------------------------------------------------------
        // DisposeBuffersFloor
        // -------------------------------------------------------------------

        public void DisposeBuffersFloor(GraphicsDevice device, bool nullable = true)
        {
            if (VBFloor != null)
            {
                device.SetVertexBuffer(null);
                device.Indices = null;
                VBFloor.Dispose();
                IBFloor.Dispose();
                if (nullable)
                {
                    VBFloor = null;
                    IBFloor = null;
                }
            }
        }
    }
}
