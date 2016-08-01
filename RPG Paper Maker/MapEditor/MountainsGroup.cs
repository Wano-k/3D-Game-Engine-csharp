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
    class MountainsGroup
    {
        public Dictionary<int[], Mountain> Tiles = new Dictionary<int[], Mountain>(new IntArrayComparer());

        [NonSerialized()]
        VertexBuffer VB;
        [NonSerialized()]
        VertexPositionTexture[] VerticesArray;
        [NonSerialized()]
        IndexBuffer IB;
        [NonSerialized()]
        int[] IndexesArray;


        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public MountainsGroup CreateCopy()
        {
            MountainsGroup newMountains = new MountainsGroup();
            foreach (KeyValuePair<int[], Mountain> entry in Tiles)
            {
                newMountains.Tiles[entry.Key] = Tiles[entry.Key].CreateCopy();
            }

            return newMountains;
        }

        // -------------------------------------------------------------------
        // GenMountains
        // -------------------------------------------------------------------

        public void GenMountains(GraphicsDevice device, int id)
        {
            DisposeBuffers(device);
            CreatePortion(device, id);
        }

        // -------------------------------------------------------------------
        // CreatePortion
        // -------------------------------------------------------------------

        public void CreatePortion(GraphicsDevice device, int id)
        {
            List<VertexPositionTexture> verticesList = new List<VertexPositionTexture>();
            List<int> indexesList = new List<int>();
            int[] indexes = new int[] { 0, 1, 2, 0, 2, 3 };
            int offset = 0;

            foreach (KeyValuePair<int[], Mountain> entry in Tiles)
            {
                List<VertexPositionTexture> vertexPositionTextures = CreateTex(MapEditor.TexReliefs[id], entry.Key, entry.Value);
                foreach (VertexPositionTexture vertex in vertexPositionTextures)
                {
                    verticesList.Add(vertex);
                }
                int count = vertexPositionTextures.Count * indexes.Length / 4;
                for (int n = 0; n < count; n++)
                {
                    indexesList.Add(indexes[n % indexes.Length] + (n / indexes.Length * 4) + offset);
                }
                offset += vertexPositionTextures.Count;
            }

            if (verticesList.Count > 0)
            {
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

        protected List<VertexPositionTexture> CreateTex(Texture2D texture, int[] coords, Mountain mountain)
        {
            int x = coords[0], y = coords[1] * WANOK.SQUARE_SIZE + coords[2], z = coords[3];
            float top = 0;
            float bot = ((float)WANOK.SQUARE_SIZE) / texture.Height;


            List<VertexPositionTexture> res = new List<VertexPositionTexture>();
            if (mountain.DrawTop)
            {
                FillTexture(res, bot, top, texture.Width, mountain.SquareHeight, mountain.PixelHeight, x, x + 1, x + 1, x, z, z, z, z, y);
            }
            if (mountain.DrawBot)
            {
                FillTexture(res, bot, top, texture.Width, mountain.SquareHeight, mountain.PixelHeight, x, x + 1, x + 1, x, z + 1, z + 1, z + 1, z + 1, y);
            }
            if (mountain.DrawLeft)
            {
                FillTexture(res, bot, top, texture.Width, mountain.SquareHeight, mountain.PixelHeight, x, x, x, x, z, z + 1, z + 1, z, y);
            }
            if (mountain.DrawRight)
            {
                FillTexture(res, bot, top, texture.Width, mountain.SquareHeight, mountain.PixelHeight, x + 1, x + 1, x + 1, x + 1, z, z + 1, z + 1, z, y);
            }

            return res;
        }

        public float GetHorizontalTexture(int i, int height)
        {
            return ((float)WANOK.SQUARE_SIZE * i) / height;
        }

        public void FillTexture(List<VertexPositionTexture> res, float bot, float top, int width, int height, int heightPlus, int x1, int x2, int x3, int x4, int z1, int z2, int z3, int z4, int y)
        {
            float left;
            float right;

            if (height == 1 && heightPlus == 0)
            {
                left = GetHorizontalTexture(0, width);
                right = GetHorizontalTexture(1, width);
            }
            else
            {
                left = GetHorizontalTexture(1, width);
                right = GetHorizontalTexture(2, width);
            }

            if (height > 0)
            {
                res.Add(new VertexPositionTexture(new Vector3(x1, y + WANOK.SQUARE_SIZE, z1), new Vector2(left, top)));
                res.Add(new VertexPositionTexture(new Vector3(x2, y + WANOK.SQUARE_SIZE, z2), new Vector2(right, top)));
                res.Add(new VertexPositionTexture(new Vector3(x3, y, z3), new Vector2(right, bot)));
                res.Add(new VertexPositionTexture(new Vector3(x4, y, z4), new Vector2(left, bot)));

                if (height > 2)
                {
                    left = GetHorizontalTexture(2, width);
                    right = GetHorizontalTexture(3, width);
                    for (int i = 1; i < height - 1; i++)
                    {
                        res.Add(new VertexPositionTexture(new Vector3(x1, y + (WANOK.SQUARE_SIZE * (i + 1)), z1), new Vector2(left, top)));
                        res.Add(new VertexPositionTexture(new Vector3(x2, y + (WANOK.SQUARE_SIZE * (i + 1)), z2), new Vector2(right, top)));
                        res.Add(new VertexPositionTexture(new Vector3(x3, y + (WANOK.SQUARE_SIZE * i), z3), new Vector2(right, bot)));
                        res.Add(new VertexPositionTexture(new Vector3(x4, y + (WANOK.SQUARE_SIZE * i), z4), new Vector2(left, bot)));
                    }
                }

                if (height > 1)
                {
                    if (heightPlus == 0)
                    {
                        left = GetHorizontalTexture(3, width);
                        right = GetHorizontalTexture(4, width);
                    }
                    else
                    {
                        left = GetHorizontalTexture(2, width);
                        right = GetHorizontalTexture(3, width);
                    }
                    res.Add(new VertexPositionTexture(new Vector3(x1, y + (WANOK.SQUARE_SIZE * height), z1), new Vector2(left, top)));
                    res.Add(new VertexPositionTexture(new Vector3(x2, y + (WANOK.SQUARE_SIZE * height), z2), new Vector2(right, top)));
                    res.Add(new VertexPositionTexture(new Vector3(x3, y + (WANOK.SQUARE_SIZE * (height - 1)), z3), new Vector2(right, bot)));
                    res.Add(new VertexPositionTexture(new Vector3(x4, y + (WANOK.SQUARE_SIZE * (height - 1)), z4), new Vector2(left, bot)));
                }
            }

            if (heightPlus > 0)
            {
                bot = ((float)heightPlus) / WANOK.SQUARE_SIZE;
                left = GetHorizontalTexture(3, width);
                right = GetHorizontalTexture(4, width);
                res.Add(new VertexPositionTexture(new Vector3(x1, y + (WANOK.SQUARE_SIZE * height) + heightPlus, z1), new Vector2(left, top)));
                res.Add(new VertexPositionTexture(new Vector3(x2, y + (WANOK.SQUARE_SIZE * height) + heightPlus, z2), new Vector2(right, top)));
                res.Add(new VertexPositionTexture(new Vector3(x3, y + (WANOK.SQUARE_SIZE * height), z3), new Vector2(right, bot)));
                res.Add(new VertexPositionTexture(new Vector3(x4, y + (WANOK.SQUARE_SIZE * height), z4), new Vector2(left, bot)));
            }
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, AlphaTestEffect effect, int id)
        {
            if (VB != null)
            {
                if (!MapEditor.TexReliefs.ContainsKey(id)) effect.Texture = MapEditor.TexNone;
                else effect.Texture = MapEditor.TexReliefs[id];

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
