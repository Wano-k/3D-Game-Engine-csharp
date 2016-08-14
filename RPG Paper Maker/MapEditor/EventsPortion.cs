using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public class EventsPortion
    {
        [NonSerialized()]
        private VertexBuffer SquaresVB;
        [NonSerialized()]
        private VertexPositionTexture[] SquaresVertices;
        [NonSerialized()]
        private IndexBuffer SquaresIB;
        [NonSerialized()]
        private int[] SquaresIndexes;


        // -------------------------------------------------------------------
        // GenEvents
        // -------------------------------------------------------------------

        public void GenEvents(GraphicsDevice device, Dictionary<SystemGraphic, Dictionary<int[], SystemEvent>> dictionary)
        {
            DisposeBuffers(device);
            if (dictionary.Count > 0) CreatePortion(device, dictionary);
        }

        // -------------------------------------------------------------------
        // CreatePortion
        // -------------------------------------------------------------------

        public void CreatePortion(GraphicsDevice device, Dictionary<SystemGraphic, Dictionary<int[], SystemEvent>> dictionary)
        {
            // Square
            List<VertexPositionTexture> squareVerticesList = new List<VertexPositionTexture>();
            List<int> squareIndexesList = new List<int>();
            int[] squareIndexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };

            int squareOffset = 0;
            foreach (KeyValuePair<SystemGraphic, Dictionary<int[], SystemEvent>> entry in dictionary)
            {
                foreach (KeyValuePair<int[], SystemEvent> entry2 in entry.Value)
                {
                    foreach (VertexPositionTexture vertex in CreateSquareTex(entry2.Key[0], (entry2.Key[1] * WANOK.SQUARE_SIZE) + entry2.Key[2], entry2.Key[3]))
                    {
                        squareVerticesList.Add(vertex);
                    }
                    for (int n = 0; n < 6; n++)
                    {
                        squareIndexesList.Add(squareIndexes[n] + squareOffset);
                    }
                    squareOffset += 4;
                }
            }

            SquaresVertices = squareVerticesList.ToArray();
            SquaresIndexes = squareIndexesList.ToArray();
            SquaresIB = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, SquaresIndexes.Length, BufferUsage.None);
            SquaresIB.SetData(SquaresIndexes);
            SquaresVB = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, SquaresVertices.Length, BufferUsage.None);
            SquaresVB.SetData(SquaresVertices);
        }

        // -------------------------------------------------------------------
        // CreateTex
        // -------------------------------------------------------------------

        protected VertexPositionTexture[] CreateSquareTex(int x, int y, int z)
        {
            // Texture coords
            float left = 0;
            float top = 0;
            float bot = 1;
            float right = 1;


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
                new VertexPositionTexture(new Vector3(x + 1, y, z), new Vector2(right, top)),
                new VertexPositionTexture(new Vector3(x + 1, y, z + 1), new Vector2(right, bot)),
                new VertexPositionTexture(new Vector3(x, y, z + 1), new Vector2(left, bot))
            };
        }

        // -------------------------------------------------------------------
        // DrawSquares
        // -------------------------------------------------------------------

        public void DrawSquares(GraphicsDevice device, AlphaTestEffect effect)
        {
            if (SquaresVB != null)
            {
                effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, 1.0f, WANOK.SQUARE_SIZE);
                effect.Texture = MapEditor.TexEventCursor;

                device.SetVertexBuffer(SquaresVB);
                device.Indices = SquaresIB;
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, SquaresVertices, 0, SquaresVertices.Length, SquaresIndexes, 0, SquaresVertices.Length / 2);
                }
            }
        }

        // -------------------------------------------------------------------
        // DisposeBuffersFloor
        // -------------------------------------------------------------------

        public void DisposeBuffers(GraphicsDevice device, bool nullable = true)
        {
            if (SquaresVB != null)
            {
                device.SetVertexBuffer(null);
                device.Indices = null;
                SquaresVB.Dispose();
                SquaresIB.Dispose();
                if (nullable)
                {
                    SquaresVB = null;
                    SquaresIB = null;
                }
            }
        }
    }
}
