using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace RPG_Paper_Maker
{
    class Map
    {
        GraphicsDevice Device;
        VertexPositionColor[] GridVerticesArray;
        VertexBuffer VBGrid;
        public MapInfos MapInfos { get; set; }
        public Dictionary<int[], GameMapPortion> Portions;
        public bool DisplayGrid = true;
        public int[] GridHeight = new int[] { 0, 0 };
        private Square StartSquare = null;
        private int[] Startposition;
        public bool Saved = true;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Map(GraphicsDevice device, string mapName)
        {
            Device = device;
            Stopwatch sw = new Stopwatch();
            bool dialogOpened = false, newTemp = false;
            sw.Start();

            // Temp files + mapInfos
            string pathTemp = Path.Combine(WANOK.MapsDirectoryPath, mapName, "temp");
            if (Directory.GetFiles(pathTemp).Length == 0)
            {
                newTemp = true;
                string[] filePaths = Directory.GetFiles(Path.Combine(WANOK.MapsDirectoryPath, mapName));
                foreach (string filePath in filePaths) File.Copy(filePath, Path.Combine(pathTemp, Path.GetFileName(filePath)));
            }
            
            MapInfos = WANOK.LoadBinaryDatas<MapInfos>(Path.Combine(WANOK.MapsDirectoryPath, mapName, "temp", "infos.map"));
            Saved = !WANOK.ListMapToSave.Contains(mapName);
            if (newTemp) WANOK.CreateCancelMap(MapInfos.RealMapName);

            // Start position

            if (mapName == WANOK.Game.System.StartMapName)
            {
                SetStartInfos(WANOK.Game.System, WANOK.Game.System.StartPosition);
            }

            // Dispose textures
            if (MapEditor.TexTileset != null)
            {
                MapEditor.TexTileset.Dispose();
                MapEditor.TexTileset = null;
            }
            foreach (int i in MapEditor.TexAutotiles.Keys)
            {
                MapEditor.TexAutotiles[i].Dispose();
            }
            MapEditor.TexAutotiles.Clear();
            foreach (int i in MapEditor.TexReliefs.Keys)
            {
                MapEditor.TexReliefs[i].Dispose();
            }
            MapEditor.TexReliefs.Clear();

            // Loading textures
            Tileset tileset = WANOK.Game.Tilesets.GetTilesetById(MapInfos.Tileset);
            MapEditor.TexTileset = tileset.Graphic.LoadTexture(device);
            for (int i = 0; i < tileset.Autotiles.Count; i++)
            {
                MapEditor.TexAutotiles[tileset.Autotiles[i]] = WANOK.Game.Tilesets.GetAutotileById(tileset.Autotiles[i]).Graphic.LoadTexture(Device);
            }
            for (int i = 0; i < tileset.Reliefs.Count; i++)
            {
                MapEditor.TexReliefs[tileset.Reliefs[i]] = WANOK.Game.Tilesets.GetReliefById(tileset.Reliefs[i]).Graphic.LoadTexture(Device);
            }

            // Grid
            CreateGrid(MapInfos.Width, MapInfos.Height);

            // Map
            Portions = new Dictionary<int[], GameMapPortion>(new IntArrayComparer());
            int count = 0;
            double coef = (WANOK.PORTION_RADIUS + 1) * (WANOK.PORTION_RADIUS + 1) * 4;
            for (int i = -WANOK.PORTION_RADIUS - 1; i <= WANOK.PORTION_RADIUS + 1; i++)
            {
                for (int j = -WANOK.PORTION_RADIUS - 1; j <= WANOK.PORTION_RADIUS + 1; j++)
                {
                    if (!dialogOpened)
                    {
                        if (sw.ElapsedMilliseconds > 50)
                        {
                            WANOK.StartProgressBar("Loading map infos...", 0);
                            dialogOpened = true;
                        }
                    }
                    else
                    {
                        int v = (int)((count / coef) * 100);
                        if (v <= 100) WANOK.DialogProgressBar.SetValue(v);
                    }
                    LoadPortion(i, j, i, j);
                    count++;
                }
            }
            sw.Stop();
            if (dialogOpened) WANOK.DisposeProgressBar();
        }

        // -------------------------------------------------------------------
        // LoadPortion
        // -------------------------------------------------------------------

        public void LoadPortion(int real_i, int real_j, int i, int j)
        {
            int[] portion = new int[] { i, j };
            Portions[portion] = WANOK.LoadPortionMap(MapInfos.RealMapName, real_i, real_j);
            if (Portions[portion] != null)
            {
                GenTextures(Portions[portion]);
            }
        }

        // -------------------------------------------------------------------
        // SetStartInfos
        // -------------------------------------------------------------------

        public void SetStartInfos(int[] startPosition)
        {
            if (StartSquare != null) StartSquare.DisposeBuffers(Device);
            StartSquare = new Square(Device, MapEditor.TexStartCursor, new int[] { 0, 0, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE });
            Startposition = startPosition;
        }

        // -------------------------------------------------------------------
        // SetStartInfos
        // -------------------------------------------------------------------

        public void SetStartInfos(SystemDatas system, int[] startPosition)
        {
            if (startPosition[0] >= 0 && startPosition[0] < MapInfos.Width && startPosition[3] >= 0 && startPosition[3] < MapInfos.Height)
            {
                SetStartInfos(startPosition);
            }
            // If not into the portions, delete it
            else
            {
                system.NoStart();
                WANOK.SaveBinaryDatas(system, WANOK.SystemPath);
            }
        }

        // -------------------------------------------------------------------
        // CreateGrid
        // -------------------------------------------------------------------

        public void CreateGrid(int width, int height)
        {
            List<VertexPositionColor> gridVerticesList = new List<VertexPositionColor>();
            // Columns
            for (int i = 0; i <= width; i++)
            {
                foreach (VertexPositionColor vertex in CreateGridLine(i, 0, i, height))
                {
                    gridVerticesList.Add(vertex);
                }
            }
            // Rows
            for (int i = 0; i <= height; i++)
            {
                foreach (VertexPositionColor vertex in CreateGridLine(0, i, width, i))
                {
                    gridVerticesList.Add(vertex);
                }
            } 
            GridVerticesArray = gridVerticesList.ToArray();
            VBGrid = new VertexBuffer(Device, typeof(VertexPositionColor), GridVerticesArray.Length, BufferUsage.WriteOnly);
            VBGrid.SetData(GridVerticesArray);
        }

        // -------------------------------------------------------------------
        // UpdatePortions
        // -------------------------------------------------------------------

        public void UpdatePortions(int[] portion)
        {
            Portions[portion].UpdateDictionaries(Device);
            if (Portions[portion].IsEmpty()) DisposeBuffers(portion);
            else GenTextures(portion);
        }

        // -------------------------------------------------------------------
        // GenTextures
        // -------------------------------------------------------------------

        public void GenTextures(int[] portion)
        {
            if (Portions[portion] != null)
            {
                GenTextures(Portions[portion]);
            }
        }

        public void GenTextures(GameMapPortion portion)
        {
            portion.GenFloor(Device, MapEditor.TexTileset);
            portion.GenAutotiles(Device);
            portion.GenSprites(Device);
            portion.GenMountains(Device);
        }

        // -------------------------------------------------------------------
        // CreateGridLine
        // -------------------------------------------------------------------

        private VertexPositionColor[] CreateGridLine(int x1, int z1, int x2, int z2)
        {
            // Vertex Position and Texture
            return new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(x1, 0, z1), Color.White),
                new VertexPositionColor(new Vector3(x2, 0, z2), Color.White)
            };
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GameTime gameTime, AlphaTestEffect effect, Camera camera)
        {
            // Drawing Items
            for(int i = -WANOK.PORTION_RADIUS; i <= WANOK.PORTION_RADIUS; i++)
            {
                for (int j = -WANOK.PORTION_RADIUS; j <= WANOK.PORTION_RADIUS; j++)
                {
                    GameMapPortion gameMap = Portions[new int[] { i, j }];
                    if (gameMap != null) gameMap.Draw(Device, effect, MapEditor.TexTileset, camera);
                }
            }
            
            // Drawing Start position
            if (StartSquare != null)
            {
                StartSquare.Draw(Device, gameTime, effect, MapEditor.TexStartCursor, new Vector3(Startposition[0] * WANOK.SQUARE_SIZE, Startposition[1] * WANOK.SQUARE_SIZE + Startposition[2], Startposition[3] * WANOK.SQUARE_SIZE));
            }

            // Drawing grid
            effect.Texture = MapEditor.TexGrid;
            Device.BlendState = BlendState.Additive;
            effect.World = Matrix.Identity * Matrix.CreateScale(WANOK.SQUARE_SIZE, 1.0f, WANOK.SQUARE_SIZE) * Matrix.CreateTranslation(0, WANOK.GetPixelHeight(GridHeight) + 0.1f, 0);
            if (DisplayGrid)
            {
                Device.SetVertexBuffer(VBGrid);
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    Device.DrawPrimitives(PrimitiveType.LineList, 0, GridVerticesArray.Length / 2);
                }
            }

            Device.BlendState = BlendState.NonPremultiplied;
        }

        // -------------------------------------------------------------------
        // DisposeBuffers
        // -------------------------------------------------------------------

        public void DisposeBuffers(int[] portion, bool nullable = true)
        {
            Portions[portion].DisposeBuffers(Device, nullable);
        }

        // -------------------------------------------------------------------
        // DisposeVertexBuffer
        // -------------------------------------------------------------------

        public void DisposeVertexBuffer()
        {
            Device.SetVertexBuffer(null);

            if (VBGrid != null) VBGrid.Dispose();

            foreach (KeyValuePair<int[], GameMapPortion> entry in Portions)
            {
                if (entry.Value != null) DisposeBuffers(entry.Key);
            }

            if (StartSquare != null) StartSquare.DisposeBuffers(Device);
        }
    }
}
