using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class EventsPortion
    {
        public Dictionary<SystemGraphic, Dictionary<int[], Sprites>> Sprites = new Dictionary<SystemGraphic, Dictionary<int[], Sprites>>();


        private VertexBuffer SquaresVB;
        private VertexPositionTexture[] SquaresVertices;
        private IndexBuffer SquaresIB;
        private int[] SquaresIndexes;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public EventsPortion(Dictionary<int[], SystemEvent> dictionary)
        {
            if (dictionary != null)
            {
                foreach (KeyValuePair<int[], SystemEvent> entry in dictionary)
                {
                    AddSprite(entry.Key, entry.Value);
                }
            }
        }

        // -------------------------------------------------------------------
        // AddSprite
        // -------------------------------------------------------------------

        public void AddSprite(int[] coords, SystemEvent ev)
        {
            if (!Sprites.ContainsKey(ev.Pages[0].Graphic)) Sprites[ev.Pages[0].Graphic] = new Dictionary<int[], Sprites>(new IntArrayComparer());
            if (ev.Pages[0].Graphic.IsTileset())
            {
                int[] texture = new int[] { (int)ev.Pages[0].Graphic.Options[(int)SystemGraphic.OptionsEnum.TilesetX],
                                            (int)ev.Pages[0].Graphic.Options[(int)SystemGraphic.OptionsEnum.TilesetY],
                                            (int)ev.Pages[0].Graphic.Options[(int)SystemGraphic.OptionsEnum.TilesetWidth],
                                            (int)ev.Pages[0].Graphic.Options[(int)SystemGraphic.OptionsEnum.TilesetHeight] };

                if (!Sprites[ev.Pages[0].Graphic].ContainsKey(texture)) Sprites[ev.Pages[0].Graphic][texture] = new Sprites();
                Sprites[ev.Pages[0].Graphic][texture].Add(coords, new Sprite(ev.Pages[0].GraphicDrawType, new int[] { 0, 0 }, 0));
            }
            else
            {
                int frames = (int)ev.Pages[0].Graphic.Options[(int)SystemGraphic.OptionsEnum.Frames];
                int index = (int)ev.Pages[0].Graphic.Options[(int)SystemGraphic.OptionsEnum.Index];
                MapEditor.LoadSystemGraphic(ev.Pages[0].Graphic, WANOK.MapEditor.GraphicsDevice);
                int width = MapEditor.TexCharacters[ev.Pages[0].Graphic].Width / frames;
                int height = MapEditor.TexCharacters[ev.Pages[0].Graphic].Height / frames;
                int[] texture = new int[] { (index % frames) * width, (index / frames) * height, width, height };

                if (!Sprites[ev.Pages[0].Graphic].ContainsKey(texture)) Sprites[ev.Pages[0].Graphic][texture] = new Sprites();
                Sprites[ev.Pages[0].Graphic][texture].Add(coords, new Sprite(ev.Pages[0].GraphicDrawType, new int[] { 0, 0 }, 0));
            }
        }

        // -------------------------------------------------------------------
        // RemoveSprite
        // -------------------------------------------------------------------

        public void RemoveSprite(int[] coords)
        {
            foreach (KeyValuePair<SystemGraphic, Dictionary<int[], Sprites>> entry in Sprites)
            {
                foreach (KeyValuePair<int[], Sprites> entry2 in entry.Value)
                {
                    foreach (int[] coords2 in entry2.Value.ListSprites.Keys)
                    {
                        if (coords.SequenceEqual(coords2))
                        {
                            entry2.Value.Remove(coords);
                            return;
                        }
                    }
                }
            }
        }

        // -------------------------------------------------------------------
        // GenEvents
        // -------------------------------------------------------------------

        public void GenEvents(GraphicsDevice device, Dictionary<int[], SystemEvent> dictionary)
        {
            DisposeBuffers(device);
            if (dictionary.Count > 0) CreatePortion(device, dictionary);
            if (Sprites.Count > 0)
            {
                foreach (KeyValuePair<SystemGraphic, Dictionary<int[], Sprites>> entry in Sprites)
                {
                    foreach (KeyValuePair<int[], Sprites> entry2 in entry.Value)
                    {
                        entry2.Value.GenSprites(device, entry.Key.IsTileset() ? MapEditor.TexTileset : MapEditor.TexCharacters[entry.Key], entry2.Key, entry.Key.IsTileset());
                    }
                }
            }
        }

        // -------------------------------------------------------------------
        // CreatePortion
        // -------------------------------------------------------------------

        public void CreatePortion(GraphicsDevice device, Dictionary<int[], SystemEvent> dictionary)
        {
            // Square
            List<VertexPositionTexture> squareVerticesList = new List<VertexPositionTexture>();
            List<int> squareIndexesList = new List<int>();
            int[] squareIndexes = new int[]
            {
                0, 1, 2, 0, 2, 3
            };

            int squareOffset = 0;
            foreach (KeyValuePair<int[], SystemEvent> entry in dictionary)
            {
                foreach (VertexPositionTexture vertex in CreateSquareTex(entry.Key[0], (entry.Key[1] * WANOK.SQUARE_SIZE) + entry.Key[2], entry.Key[3]))
                {
                    squareVerticesList.Add(vertex);
                }
                for (int n = 0; n < 6; n++)
                {
                    squareIndexesList.Add(squareIndexes[n] + squareOffset);
                }
                squareOffset += 4;
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
        // DrawSprites
        // -------------------------------------------------------------------

        public void DrawSprites(GraphicsDevice device, AlphaTestEffect effect, Camera camera)
        {
            foreach (KeyValuePair<SystemGraphic, Dictionary<int[], Sprites>> entry in Sprites)
            {
                SystemGraphic graphic = entry.Key.IsTileset() ? null : entry.Key;
                foreach (KeyValuePair<int[], Sprites> entry2 in entry.Value)
                {
                    entry2.Value.Draw(device, effect, camera, graphic == null ? entry2.Key[2] * WANOK.SQUARE_SIZE : entry2.Key[2], graphic == null ? entry2.Key[3] * WANOK.SQUARE_SIZE : entry2.Key[3], graphic);
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

            foreach (Dictionary<int[], Sprites> entry in Sprites.Values)
            {
                foreach (Sprites sprites in entry.Values)
                {
                    sprites.DisposeBuffers(device, nullable);
                }
            }
        }
    }
}
