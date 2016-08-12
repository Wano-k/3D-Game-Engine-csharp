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
        public Dictionary<int[], int[]> Floors; // Coords => texture
        public Dictionary<int, Autotiles> Autotiles; // Id => Autotiles (= list autotile)
        public Dictionary<int[], Sprites> Sprites; // Texture => Sprites
        public Dictionary<int, Mountains> Mountains; // Height => Mountains

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
            Sprites = new Dictionary<int[], Sprites>(new IntArrayComparer());
            Mountains = new Dictionary<int, Mountains>();
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public GameMapPortion CreateCopy()
        {
            GameMapPortion newGameMap = new GameMapPortion();
            newGameMap.Floors = new Dictionary<int[], int[]>(Floors, new IntArrayComparer());
            foreach (KeyValuePair<int, Autotiles> entry in Autotiles)
            {
                newGameMap.Autotiles[entry.Key] = entry.Value.CreateCopy();
            }
            foreach (KeyValuePair<int[], Sprites> entry in Sprites)
            {
                newGameMap.Sprites[entry.Key] = entry.Value.CreateCopy();
            }
            foreach (KeyValuePair<int, Mountains> entry in Mountains)
            {
                newGameMap.Mountains[entry.Key] = entry.Value.CreateCopy();
            }

            return newGameMap;
        }

        // -------------------------------------------------------------------
        // IsEmpty
        // -------------------------------------------------------------------

        public bool IsEmpty()
        {
            return (Floors.Count == 0 && Autotiles.Count == 0 && Sprites.Count == 0 && Mountains.Count == 0);
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
        // ContainsSprite
        // -------------------------------------------------------------------

        public object[] ContainsSprite(int[] coords)
        {
            foreach (KeyValuePair<int[], Sprites> entry in Sprites)
            {
                Sprite sprite = entry.Value.ContainsCoords(coords);
                if (sprite != null) return new object[] { entry.Key, sprite };
            }

            return null;
        }

        // -------------------------------------------------------------------
        // ContainsMountain
        // -------------------------------------------------------------------

        public object[] ContainsMountain(int height, int[] coords)
        {
            if (Mountains.ContainsKey(height))
            {
                object[] obj = Mountains[height].ContainsInGroup(coords);
                if (obj != null) return obj;
            }

            return null;
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
        // AddSprite
        // -------------------------------------------------------------------

        public bool AddSprite(int[] coords, int[] newTexture, Sprite newSprite)
        {
            bool modified = false;
            object[] before = ContainsSprite(coords); // drawtype, positionOrientation, texture

            // Remplacing
            if (before == null)
            {
                modified = true;
            }
            else
            {
                int[] beforeTexture = (int[])before[0];
                Sprite beforeSprite = (Sprite)before[1];
                if (!beforeTexture.SequenceEqual(newTexture) || !beforeSprite.Equals(newSprite)) modified = true;

                Sprites[beforeTexture].Remove(coords);
            }

            if (!Sprites.ContainsKey(newTexture)) Sprites[newTexture] = new Sprites();
            Sprites[newTexture].Add(coords, newSprite);

            return modified;
        }

        // -------------------------------------------------------------------
        // RemoveSprite
        // -------------------------------------------------------------------

        public bool RemoveSprite(int[] coords)
        {
            bool modified = false;

            object[] before = ContainsSprite(coords);
            if (before != null)
            {
                modified = true;
                Sprites[(int[])before[0]].Remove(coords);
            }

            return modified;
        }

        // -------------------------------------------------------------------
        // AddMountain
        // -------------------------------------------------------------------

        public bool AddMountain(int[] coords, int newId, Mountain newMountain)
        {
            bool modified = false;
            int height = WANOK.GetCoordsPixelHeight(coords);
            object[] before = ContainsMountain(height, coords);

            // Remplacing
            if (before == null)
            {
                modified = true;
            }
            else
            {
                int beforeId = (int)before[0];
                Mountain beforeMountain = (Mountain)before[1];
                if (beforeId != newId) modified = true;
                Mountains[height].Remove(coords, beforeId, height);
            }

            if (!Mountains.ContainsKey(height)) Mountains[height] = new Mountains();
            Mountains[height].Add(coords, newId, newMountain, height);

            SystemTileset tileset = MapEditor.GetMapTileset();
            object[] reliefTop = tileset.ReliefTop[tileset.Reliefs.IndexOf(newId)];


            switch ((DrawType)reliefTop[0])
            {
                case DrawType.Floors:
                    AddFloor(new int[] { coords[0], coords[1] + newMountain.SquareHeight, coords[2] + newMountain.PixelHeight, coords[3] }, (int[])reliefTop[1]);
                    break;
                case DrawType.Autotiles:
                    int id = ((int[])reliefTop[1])[0];
                    if (id > 0) AddAutotile(new int[] { coords[0], coords[1] + newMountain.SquareHeight, coords[2] + newMountain.PixelHeight, coords[3] }, id, true);
                    break;
            }


            return modified;
        }

        // -------------------------------------------------------------------
        // RemoveMountain
        // -------------------------------------------------------------------

        public bool RemoveMountain(int[] coords)
        {
            bool modified = false;
            int height = WANOK.GetCoordsPixelHeight(coords);
            object[] before = ContainsMountain(height, coords);
            if (before != null)
            {
                modified = true;
                int beforeId = (int)before[0];
                Mountain beforeMountain = (Mountain)before[1];
                Mountains[height].Remove(coords, beforeId, height);
                RemoveFloor(new int[] { coords[0], coords[1] + beforeMountain.SquareHeight, coords[2] + beforeMountain.PixelHeight, coords[3] });
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
                if (entry.Value[2] * WANOK.SQUARE_SIZE <= texture.Width && entry.Value[3] * WANOK.SQUARE_SIZE <= texture.Height)
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
            }

            if (verticesList.Count > 0)
            {
                VerticesFloorArray = verticesList.ToArray();
                IndexesFloorArray = indexesList.ToArray();
                IBFloor = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, IndexesFloorArray.Length, BufferUsage.None);
                IBFloor.SetData(IndexesFloorArray);
                VBFloor = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, VerticesFloorArray.Length, BufferUsage.None);
                VBFloor.SetData(VerticesFloorArray);
            }
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
            left += width / WANOK.COEF_BORDER_TEX;
            right -= width / WANOK.COEF_BORDER_TEX;
            top += height / WANOK.COEF_BORDER_TEX;
            bot -= height / WANOK.COEF_BORDER_TEX;

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

        #region Sprites

        public void GenSprites(GraphicsDevice device)
        {
            foreach (KeyValuePair<int[], Sprites> entry in Sprites)
            {
                entry.Value.GenSprites(device, entry.Key);
            }
        }

        #endregion

        #region Mountains

        public void GenMountains(GraphicsDevice device)
        {
            foreach (Mountains mountains in Mountains.Values)
            {
                mountains.GenMountains(device);
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, AlphaTestEffect effect, Texture2D texture, Camera camera)
        {
            // Drawing Sprites & montains
            effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, 1.0f, WANOK.SQUARE_SIZE);
            foreach (Mountains mountains in Mountains.Values)
            {
                mountains.Draw(device, effect);
            }

            effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, 1.0f, WANOK.SQUARE_SIZE);

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

            foreach (KeyValuePair<int[], Sprites> entry in Sprites)
            {
                entry.Value.Draw(device, effect, camera, entry.Key[2], entry.Key[3]);
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

            // Sprites
            List<int[]> spritesToDelete = new List<int[]>();
            foreach (int[] texture in Sprites.Keys)
            {
                if (Sprites[texture].IsEmpty()) spritesToDelete.Add(texture);
            }
            for (int i = 0; i < spritesToDelete.Count; i++)
            {
                Sprites[spritesToDelete[i]].DisposeBuffers(device);
                Sprites.Remove(spritesToDelete[i]);
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
            foreach (Sprites sprites in Sprites.Values)
            {
                sprites.DisposeBuffers(device, nullable);
            }
            foreach (Mountains mountains in Mountains.Values)
            {
                mountains.DisposeBuffers(device, nullable);
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
