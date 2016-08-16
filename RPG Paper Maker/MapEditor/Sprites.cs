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
        // CreateCopy
        // -------------------------------------------------------------------

        public Sprites CreateCopy()
        {
            Sprites newSprites = new Sprites();
            newSprites.ListSprites = new Dictionary<int[], Sprite>(ListSprites, new IntArrayComparer());

            return newSprites;
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

        public void GenSprites(GraphicsDevice device, Texture2D texture2D, int[] texture, bool isTileset = true)
        {
            DisposeBuffers(device);
            CreatePortion(device, texture2D, texture, isTileset);
        }

        // -------------------------------------------------------------------
        // CreatePortion
        // -------------------------------------------------------------------

        public void CreatePortion(GraphicsDevice device, Texture2D texture2D, int[] texture, bool isTileset)
        {
            // Building vertex buffer indexed
            List<VertexPositionTexture> verticesList = new List<VertexPositionTexture>();
            List<int> indexesList = new List<int>();
            int[] indexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };
            if (!isTileset || (texture[2] * WANOK.SQUARE_SIZE <= MapEditor.TexTileset.Width && texture[3] * WANOK.SQUARE_SIZE <= MapEditor.TexTileset.Height))
            {
                foreach (VertexPositionTexture vertex in CreateTex(texture2D, texture, isTileset))
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

        protected VertexPositionTexture[] CreateTex(Texture2D texture, int[] coords, bool isTileset)
        {
            float pixelX = coords[0], pixelY = coords[1], pixelWidth = coords[2], pixelHeight = coords[3];
            if (isTileset)
            {
                pixelX *= WANOK.SQUARE_SIZE;
                pixelY *= WANOK.SQUARE_SIZE;
                pixelWidth *= WANOK.SQUARE_SIZE;
                pixelHeight *= WANOK.SQUARE_SIZE;
            }

            // Texture coords
            float left = pixelX / texture.Width;
            float top = pixelY / texture.Height;
            float bot = (pixelY + pixelHeight) / texture.Height;
            float right = (pixelX + pixelWidth) / texture.Width;

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
                new VertexPositionTexture(new Vector3(0, pixelHeight, 0), new Vector2(left, top)),
                new VertexPositionTexture(new Vector3(pixelWidth, pixelHeight, 0), new Vector2(right, top)),
                new VertexPositionTexture(new Vector3(pixelWidth, 0, 0), new Vector2(right, bot)),
                new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(left, bot))
            };
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, AlphaTestEffect effect, Camera camera, int width, int height, SystemGraphic characterGraphic = null)
        {
            if (VB != null)
            {
                if (characterGraphic == null) effect.Texture = MapEditor.TexTileset;
                else effect.Texture = MapEditor.TexCharacters[characterGraphic];
                device.SetVertexBuffer(VB);
                device.Indices = IB;
                foreach (int[] coords in ListSprites.Keys)
                {
                    ListSprites[coords].Draw(device, effect, VerticesArray, IndexesArray, camera, coords, width, height);
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
