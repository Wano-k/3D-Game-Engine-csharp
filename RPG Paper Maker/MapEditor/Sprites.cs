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
    class Sprites
    {
        public Dictionary<int[], Sprite> ListSprites; // Coords => sprite

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

        public Sprites()
        {
            ListSprites = new Dictionary<int[], Sprite>(new IntArrayComparer());
        }

        // -------------------------------------------------------------------
        // IsEmpty
        // -------------------------------------------------------------------

        public bool IsEmpty()
        {
            return ListSprites.Count == 0;
        }

        // -------------------------------------------------------------------
        // ContainsCoords
        // -------------------------------------------------------------------

        public Sprite ContainsCoords(int[] coords)
        {
            if (ListSprites.ContainsKey(coords)) return ListSprites[coords];

            return null;
        }

        // -------------------------------------------------------------------
        // Add
        // -------------------------------------------------------------------

        public void Add(int[] coords, Sprite sprite)
        {
            ListSprites[coords] = sprite;
        }

        // -------------------------------------------------------------------
        // Remove
        // -------------------------------------------------------------------

        public void Remove(int[] coords)
        {
            ListSprites.Remove(coords);
        }

        // -------------------------------------------------------------------
        // GenSprites
        // -------------------------------------------------------------------

        public void GenSprites(GraphicsDevice device, int[] texture)
        {
            DisposeBuffers(device);
            CreatePortion(device, texture);

        }

        // -------------------------------------------------------------------
        // CreatePortion
        // -------------------------------------------------------------------

        public void CreatePortion(GraphicsDevice device, int[] texture)
        {
            // Building vertex buffer indexed
            List<VertexPositionTexture> verticesList = new List<VertexPositionTexture>();
            List<int> indexesList = new List<int>();
            int[] indexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };
            if (texture[2] * WANOK.SQUARE_SIZE <= MapEditor.TexTileset.Width && texture[3] * WANOK.SQUARE_SIZE <= MapEditor.TexTileset.Height)
            {
                foreach (VertexPositionTexture vertex in CreateTex(MapEditor.TexTileset, texture))
                {
                    verticesList.Add(vertex);
                }
                for (int n = 0; n < 6; n++)
                {
                    indexesList.Add(indexes[n]);
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

        protected VertexPositionTexture[] CreateTex(Texture2D texture, int[] coords)
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
                new VertexPositionTexture(new Vector3(0, coords[3], 0), new Vector2(left, top)),
                new VertexPositionTexture(new Vector3(coords[2], coords[3], 0), new Vector2(right, top)),
                new VertexPositionTexture(new Vector3(coords[2], 0, 0), new Vector2(right, bot)),
                new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(left, bot))
            };
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, AlphaTestEffect effect, Camera camera, int width)
        {
            if (VB != null)
            {
                effect.Texture = MapEditor.TexTileset;
                device.SetVertexBuffer(VB);
                device.Indices = IB;
                foreach (int[] coords in ListSprites.Keys)
                {
                    ListSprites[coords].Draw(device, effect, VerticesArray, IndexesArray, camera, coords, width);
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
