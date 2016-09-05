using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class Autotiles
    {
        public int Id;
        public Dictionary<int[], Autotile> Tiles = new Dictionary<int[], Autotile>(new IntArrayComparer());
        public static Dictionary<string, int> AutotileBorder = new Dictionary<string, int>{
            { "A1", 2 },
            { "B1", 3 },
            { "C1", 6 },
            { "D1", 7 },
            { "A2", 8 },
            { "B4", 9 },
            { "A4", 10 },
            { "B2", 11 },
            { "C5", 12 },
            { "D3", 13 },
            { "C3", 14 },
            { "D5", 15 },
            { "A5", 16 },
            { "B3", 17 },
            { "A3", 18 },
            { "B5", 19 },
            { "C2", 20 },
            { "D4", 21 },
            { "C4", 22 },
            { "D2", 23 },
        };
        public static string[] listA = new string[] { "A1", "A2", "A3", "A4", "A5" };
        public static string[] listB = new string[] { "B1", "B2", "B3", "B4", "B5" };
        public static string[] listC = new string[] { "C1", "C2", "C3", "C4", "C5" };
        public static string[] listD = new string[] { "D1", "D2", "D3", "D4", "D5" };

        [NonSerialized()]
        VertexBuffer VB;
        [NonSerialized()]
        VertexPositionTexture[] VerticesArray;
        [NonSerialized()]
        IndexBuffer IB;
        [NonSerialized()]
        int[] IndexesArray;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Autotiles(int id)
        {
            Id = id;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public Autotiles CreateCopy()
        {
            Autotiles newAutotiles = new Autotiles(Id);
            foreach (KeyValuePair<int[], Autotile> entry in Tiles)
            {
                newAutotiles.Tiles[entry.Key] = entry.Value.CreateCopy();
            }

            return newAutotiles;
        }

        // -------------------------------------------------------------------
        // IsEmpty
        // -------------------------------------------------------------------

        public bool IsEmpty()
        {
            return Tiles.Count == 0;
        }

        // -------------------------------------------------------------------
        // Add
        // -------------------------------------------------------------------

        public void Add(int[] coords, bool update = true)
        {
            if (!Tiles.ContainsKey(coords))
            {
                Tiles[coords] = new Autotile();
                UpdateAround(coords[0], coords[1], coords[2], coords[3], update);
            }
        }

        // -------------------------------------------------------------------
        // Remove
        // -------------------------------------------------------------------

        public void Remove(int[] coords, bool update = true)
        {
            if (Tiles.ContainsKey(coords))
            {
                Tiles.Remove(coords);
                UpdateAround(coords[0], coords[1], coords[2], coords[3], update);
            }
        }

        // -------------------------------------------------------------------
        // UpdateAround
        // -------------------------------------------------------------------

        public void UpdateAround(int x, int y1, int y2, int z, bool update)
        {
            int[] portion = MapEditor.Control.GetPortion(x, z); // portion where you are setting autotile
            for (int X = x - 1; X <= x + 1; X++)
            {
                for (int Z = z - 1; Z <= z + 1; Z++)
                {
                    int[] coords = new int[] { X, y1, y2, Z };
                    Autotile autotileAround = TileOnWhatever(coords);
                    if (autotileAround != null)
                    {
                        if (update) autotileAround.Update(this, coords, portion);
                        else
                        {
                            int[] newPortion = MapEditor.Control.GetPortion(X, Z);
                            if (WANOK.IsInPortions(newPortion))
                            {
                                MapEditor.Control.AddPortionsAutotileToUpdate(newPortion);
                                WANOK.AddPortionsToAddCancel(MapEditor.Control.Map.MapInfos.RealMapName, MapEditor.Control.GetGlobalPortion(newPortion));
                            }
                            else autotileAround.Update(this, coords, portion);
                        }
                    }
                }
            }
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(int[] coords, int[] portion)
        {
            Tiles[coords].Update(this, coords, portion);
        }

        // -------------------------------------------------------------------
        // TILES
        // -------------------------------------------------------------------

        public bool TileOnLeft(int[] coords, int[] portion)
        {
            return TileOnWhatever(new int[] { coords[0] - 1, coords[1], coords[2], coords[3] }) != null;
        }

        public bool TileOnRight(int[] coords, int[] portion)
        {
            return TileOnWhatever(new int[] { coords[0] + 1, coords[1], coords[2], coords[3] }) != null;
        }

        public bool TileOnTop(int[] coords, int[] portion)
        {
            return TileOnWhatever(new int[] { coords[0], coords[1], coords[2], coords[3] - 1 }) != null;
        }

        public bool TileOnBottom(int[] coords, int[] portion)
        {
            return TileOnWhatever(new int[] { coords[0], coords[1], coords[2], coords[3] + 1 }) != null;
        }

        public bool TileOnTopLeft(int[] coords, int[] portion)
        {
            return TileOnWhatever(new int[] { coords[0] - 1, coords[1], coords[2], coords[3] - 1 }) != null;
        }

        public bool TileOnTopRight(int[] coords, int[] portion)
        {
            return TileOnWhatever(new int[] { coords[0] + 1, coords[1], coords[2], coords[3] - 1 }) != null;
        }

        public bool TileOnBottomLeft(int[] coords, int[] portion)
        {
            return TileOnWhatever(new int[] { coords[0] - 1, coords[1], coords[2], coords[3] + 1 }) != null;
        }

        public bool TileOnBottomRight(int[] coords, int[] portion)
        {
            return TileOnWhatever(new int[] { coords[0] + 1, coords[1], coords[2], coords[3] + 1 }) != null;
        }

        public Autotile TileOnWhatever(int[] coords)
        {
            int[] portion = MapEditor.Control.GetPortion(coords[0], coords[3]);
            if (MapEditor.Control.Map.Portions.ContainsKey(portion))
            {
                if (MapEditor.Control.Map.Portions[portion] != null && MapEditor.Control.Map.Portions[portion].Autotiles.ContainsKey(Id))
                {
                    if (MapEditor.Control.Map.Portions[portion].Autotiles[Id].Tiles.ContainsKey(coords)) return MapEditor.Control.Map.Portions[portion].Autotiles[Id].Tiles[coords];
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        // -------------------------------------------------------------------
        // GenAutotiles
        // -------------------------------------------------------------------

        public void GenAutotiles(GraphicsDevice device)
        {
            DisposeBuffers(device);
            CreatePortion(device);
        }

        // -------------------------------------------------------------------
        // CreatePortion
        // -------------------------------------------------------------------

        public void CreatePortion(GraphicsDevice device)
        {
            List<VertexPositionTexture> verticesList = new List<VertexPositionTexture>();
            List<int> indexesList = new List<int>();
            int[] indexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };
            int offset = 0;

            if (MapEditor.TexAutotiles.ContainsKey(Id))
            {
                foreach (KeyValuePair<int[], Autotile> entry in Tiles)
                {
                    foreach (VertexPositionTexture vertex in CreateTex(entry.Key, entry.Value))
                    {
                        verticesList.Add(vertex);
                    }
                    for (int n = 0; n < 6; n++)
                    {
                        indexesList.Add(indexes[n] + offset);
                    }
                    offset += 4;
                }

                VerticesArray = verticesList.ToArray();
                IndexesArray = indexesList.ToArray();
                IB = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, IndexesArray.Length, BufferUsage.None);
                IB.SetData(IndexesArray);
                VB = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, VerticesArray.Length, BufferUsage.None);
                VB.SetData(VerticesArray);
            }
        }

        // -------------------------------------------------------------------
        // CreateTex
        // -------------------------------------------------------------------

        protected VertexPositionTexture[] CreateTex(int[] coords, Autotile autotile)
        {
            int x = coords[0], y = coords[1] * WANOK.SQUARE_SIZE + coords[2], z = coords[3];

            int xTile = autotile.TilesId % 125;
            int yTile = autotile.TilesId / 125;

            // Texture coords
            float left = xTile / 125.0f;
            float top = yTile / 5.0f;
            float bot = (yTile + 1) / 5.0f;
            float right = (xTile + 1) / 125.0f;

            // Vertex Position and Texture
            return new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(x, y, z), new Vector2(left, top)),
                new VertexPositionTexture(new Vector3(x+1, y, z), new Vector2(right, top)),
                new VertexPositionTexture(new Vector3(x+1, y, z+1), new Vector2(right, bot)),
                new VertexPositionTexture(new Vector3(x, y, z+1), new Vector2(left, bot))
            };
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, AlphaTestEffect effect)
        {
            if (VB != null)
            {
                if (!MapEditor.TexAutotiles.ContainsKey(Id)) effect.Texture = MapEditor.TexNone;
                else effect.Texture = MapEditor.TexAutotiles[Id];

                device.SetVertexBuffer(VB);
                device.Indices = IB;
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, VerticesArray, 0, VerticesArray.Length, IndexesArray, 0, VerticesArray.Length / 2);
                }
            }
        }

        // -------------------------------------------------------------------
        // DisposeBuffers
        // -------------------------------------------------------------------

        public void DisposeBuffers(GraphicsDevice device, bool nullable = true)
        {
            if (VB != null)
            {
                device.SetVertexBuffer(null);
                device.Indices = null;
                VB.Dispose();
                IB.Dispose();
                if (nullable)
                {
                    VB = null;
                    IB = null;
                }
            }
        }
    }
}
