using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    class MapEditorControl
    {
        public Camera Camera;
        public Map Map = null;
        public CursorEditor CursorEditor;
        public bool IsMapReloading = false;
        public Point MouseBeforeUpdate = WANOK.MapMouseManager.GetPosition();
        public string SelectedDrawType = "ItemFloor";
        public DrawMode DrawMode = DrawMode.Pencil;
        public Vector3 PointOnPlane;
        public int[] GridHeight { get { return Map.GridHeight; } set { Map.GridHeight = value; } }
        public int[] CurrentTexture = new int[] { 0, 0, 1, 1 };
        public int[] CurrentPortion = new int[] { 0, 0 };
        protected List<int[]> PortionsToUpdate = new List<int[]>();
        protected List<int[]> PortionsToSave = new List<int[]>();
        public int[] PreviousMouseCoords = null;
        public int[] PreviousCursorCoords = null;

        public delegate void MethodStock(int[] coords, params object[] args);

        // -------------------------------------------------------------------
        // OPTIONS
        // -------------------------------------------------------------------

        #region Options

        // -------------------------------------------------------------------
        // Options
        // -------------------------------------------------------------------

        public void Options()
        {
            if (WANOK.KeyboardManager.IsButtonDown(WANOK.Settings.KeyboardAssign.EditorShowGrid))
            {
                DisplayGrid();
            }
        }

        // -------------------------------------------------------------------
        // DisplayGrid
        // -------------------------------------------------------------------

        public void DisplayGrid()
        {
            Map.DisplayGrid = !Map.DisplayGrid;
        }

        #endregion

        // -------------------------------------------------------------------
        // UPDATE (main, portions)
        // -------------------------------------------------------------------

        #region Update

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(GraphicsDevice graphicsDevice, Camera camera)
        {
            // Raycasting
            Ray ray = WANOK.CalculateRay(new Vector2(MouseBeforeUpdate.X, MouseBeforeUpdate.Y), camera.View, camera.Projection, graphicsDevice.Viewport);
            int height = WANOK.GetPixelHeight(GridHeight);
            float distance = (height - camera.Position.Y) / ray.Direction.Y;
            PointOnPlane = WANOK.GetCorrectPointOnRay(ray, camera, distance, height);

            // Portion moving
            int[] newPortion = CursorEditor.GetPortion();
            if (newPortion[0] != CurrentPortion[0] || newPortion[1] != CurrentPortion[1])
            {
                UpdateMovingPortion(newPortion, CurrentPortion);
            }
            CurrentPortion = newPortion;

            // Update portions
            UpdatePortions();
        }

        // -------------------------------------------------------------------
        // Portions
        // -------------------------------------------------------------------

        public void AddPortionToUpdate(int[] portion)
        {
            for (int i = PortionsToUpdate.Count-1; i >= 0; i--)
            {
                if (PortionsToUpdate[i].SequenceEqual(portion)) return;
            }
            PortionsToUpdate.Add(portion);
        }

        public void AddPortionToSave(int[] portion)
        {
            for (int i = PortionsToSave.Count - 1; i >= 0; i--)
            {
                if (PortionsToSave[i].SequenceEqual(portion)) return;
            }
            PortionsToSave.Add(portion);
        }

        public void ClearPortions()
        {
            PortionsToUpdate.Clear();
            PortionsToSave.Clear();
        }

        // -------------------------------------------------------------------
        // UpdateMovingPortion
        // -------------------------------------------------------------------

        public void UpdateMovingPortion(int[] currentPortion, int[] previousPortion)
        {
            // If cursor going to right side
            if (currentPortion[0] > previousPortion[0])
            {
                for (int j = -WANOK.PORTION_RADIUS; j <= WANOK.PORTION_RADIUS; j++)
                {
                    for (int i = -WANOK.PORTION_RADIUS; i < WANOK.PORTION_RADIUS; i++)
                    {
                        SetPortion(i, j, i + 1, j);
                    }
                    LoadPortion(currentPortion, WANOK.PORTION_RADIUS, j);
                }
            }
            // If cursor going to left side
            else if (currentPortion[0] < previousPortion[0])
            {
                for (int j = -WANOK.PORTION_RADIUS; j <= WANOK.PORTION_RADIUS; j++)
                {
                    for (int i = WANOK.PORTION_RADIUS; i > -WANOK.PORTION_RADIUS; i--)
                    {
                        SetPortion(i, j, i - 1, j);
                    }
                    LoadPortion(currentPortion, -WANOK.PORTION_RADIUS, j);
                }
            }
            // If cursor going to up side
            if (currentPortion[1] > previousPortion[1])
            {
                for (int i = -WANOK.PORTION_RADIUS; i <= WANOK.PORTION_RADIUS; i++)
                {
                    for (int j = -WANOK.PORTION_RADIUS; j < WANOK.PORTION_RADIUS; j++)
                    {
                        SetPortion(i, j, i, j + 1);
                    }
                    LoadPortion(currentPortion, i, WANOK.PORTION_RADIUS);
                }
            }
            // If cursor going to down side
            else if (currentPortion[1] < previousPortion[1])
            {
                for (int i = -WANOK.PORTION_RADIUS; i <= WANOK.PORTION_RADIUS; i++)
                {
                    for (int j = WANOK.PORTION_RADIUS; j > -WANOK.PORTION_RADIUS; j--)
                    {
                        SetPortion(i, j, i, j - 1);
                    }
                    LoadPortion(currentPortion, i, -WANOK.PORTION_RADIUS);
                }
            }
        }

        // -------------------------------------------------------------------
        // SetPortion
        // -------------------------------------------------------------------

        public void SetPortion(int i, int j, int k, int l)
        {
            DisposeBuffers(i, j);
            Map.Portions[new int[] { i, j }] = Map.Portions[new int[] { k, l }];
        }

        // -------------------------------------------------------------------
        // LoadPortion
        // -------------------------------------------------------------------

        public void LoadPortion(int[] currentPortion, int i, int j)
        {
            DisposeBuffers(i, j);
            Map.LoadPortion(currentPortion[0] + i, currentPortion[1] + j, i, j);
        }

        // -------------------------------------------------------------------
        // UpdatePortions
        // -------------------------------------------------------------------

        public void UpdatePortions()
        {
            for (int i = 0; i < PortionsToUpdate.Count; i++)
            {
                if (Map.Portions[PortionsToUpdate[i]].IsEmpty()) Map.DisposeBuffers(PortionsToUpdate[i]);
                else Map.GenFloor(PortionsToUpdate[i]);
            }

            for (int i = 0; i < PortionsToSave.Count; i++)
            {
                int x = PortionsToSave[i][0] + (CursorEditor.GetX() / WANOK.PORTION_SIZE);
                int y = PortionsToSave[i][1] + (CursorEditor.GetZ() / WANOK.PORTION_SIZE);
                string path = Path.Combine(WANOK.MapsDirectoryPath, Map.MapInfos.RealMapName, "temp", x + "-" + y + ".pmap");

                if (((Map.Portions[PortionsToSave[i]] != null) && Map.Portions[PortionsToSave[i]].IsEmpty()) || Map.Portions[PortionsToSave[i]] == null)
                {
                    Map.Portions[PortionsToSave[i]] = null;
                    if (File.Exists(path)) File.Delete(path);
                }
                else
                {
                    WANOK.SaveBinaryDatas(Map.Portions[PortionsToSave[i]], path);
                }
            }

            ClearPortions();
        }

        #endregion

        // -------------------------------------------------------------------
        // ADD / REMOVE
        // -------------------------------------------------------------------

        #region AddRemove

        // -------------------------------------------------------------------
        // ButtonUp
        // -------------------------------------------------------------------

        public void ButtonUp()
        {
            if (WANOK.MapMouseManager.IsButtonUp(MouseButtons.Left) || WANOK.MapMouseManager.IsButtonUp(MouseButtons.Right))
            {
                PreviousMouseCoords = null;
            }
            if (WANOK.KeyboardManager.IsButtonUp(WANOK.Settings.KeyboardAssign.EditorDrawCursor))
            {
                PreviousCursorCoords = null;
            }
        }

        // -------------------------------------------------------------------
        // SetToNoSaved
        // -------------------------------------------------------------------

        public void SetToNoSaved()
        {
            if (Map.Saved)
            {
                Map.Saved = false;
                WANOK.ListMapToSave.Add(Map.MapInfos.RealMapName);
                WANOK.SelectedNode.Text = ((TreeTag)WANOK.SelectedNode.Tag).MapName + " *";
            }
        }

        // -------------------------------------------------------------------
        // Add
        // -------------------------------------------------------------------

        public void Add(bool isMouse)
        {
            switch (SelectedDrawType)
            {
                case "ItemStart":
                    AddStart(isMouse);
                    break;
                case "ItemFloor":
                    AddFloor(isMouse);
                    break;
            }
        }

        // -------------------------------------------------------------------
        // Remove
        // -------------------------------------------------------------------

        public void Remove(bool isMouse)
        {
            switch (SelectedDrawType)
            {
                case "ItemFloor":
                    RemoveFloor(isMouse);
                    break;
            }
        }

        // -------------------------------------------------------------------
        // AddStart
        // -------------------------------------------------------------------

        public void AddStart(bool isMouse)
        {
            // Getting coords
            int[] coords = GetCoords(isMouse);
            if (coords == null) return;

            if (IsInArea(coords)){
                // Saving
                SystemDatas system = WANOK.LoadBinaryDatas<SystemDatas>(WANOK.SystemPath);
                system.StartMapName = Map.MapInfos.RealMapName;
                system.StartPosition = coords;
                WANOK.SaveBinaryDatas(system, WANOK.SystemPath);

                // Updating in map
                Map.SetStartInfos(coords);
            }
        }

        // -------------------------------------------------------------------
        // AddFloor
        // -------------------------------------------------------------------

        public void AddFloor(bool isMouse)
        {
            // Getting coords
            int[] coords = GetCoords(isMouse);
            if (coords == null) return;

            // Drawing squares
            switch (DrawMode){
                case DrawMode.Pencil:
                    if (CurrentTexture[2] == 1 && CurrentTexture[3] == 1)
                    {
                        if (isMouse) if (PreviousMouseCoords != null) TraceLine(PreviousMouseCoords, coords, StockFloor, CurrentTexture);
                        else if (PreviousCursorCoords != null) TraceLine(PreviousCursorCoords, coords, StockFloor, CurrentTexture);
                    }

                    for (int i = 0; i < CurrentTexture[2]; i++)
                    {
                        for (int j = 0; j < CurrentTexture[3]; j++)
                        {
                            if ((coords[0] + i) > Map.MapInfos.Width || (coords[3] + j) > Map.MapInfos.Height) break;
                            int[] shortTexture = new int[] { i + CurrentTexture[0], j + CurrentTexture[1], 1, 1 };
                            int[] shortCoords = new int[] { coords[0] + i, coords[1], coords[2], coords[3] + j };
                            StockFloor(shortCoords, shortTexture);
                        }
                    }
                    break;
                case DrawMode.Tin:
                    AddTin(StockFloor, coords, CurrentTexture);
                    break;
            }
            
            // Updating previous selected
            if (isMouse) PreviousMouseCoords = coords;
            else PreviousCursorCoords = coords;
        }

        // -------------------------------------------------------------------
        // StockFloor
        // -------------------------------------------------------------------

        public void StockFloor(int[] coords, params object[] args)
        {
            int[] portion = GetPortion(coords[0], coords[3]);
            if (IsInArea(coords) && IsInPortions(portion))
            {
                int[] texture = (int[])args[0];

                if (Map.Portions[portion] == null) Map.Portions[portion] = new GameMapPortion();
                if (Map.Portions[portion].AddFloor(coords, texture) && Map.Saved) SetToNoSaved();
                AddPortionToSave(portion);
                AddPortionToUpdate(portion);
            }
        }

        // -------------------------------------------------------------------
        // RemoveFloor
        // -------------------------------------------------------------------

        public void RemoveFloor(bool isMouse)
        {
            // Getting coords
            int[] coords = GetCoords(isMouse);
            if (coords == null) return;

            // Removing squares
            switch (DrawMode)
            {
                case DrawMode.Pencil:
                    if (CurrentTexture[2] == 1 && CurrentTexture[3] == 1)
                    {
                        if (isMouse) if (PreviousMouseCoords != null) TraceLine(PreviousMouseCoords, coords, EraseFloor);
                        else if (PreviousCursorCoords != null) TraceLine(PreviousCursorCoords, coords, EraseFloor);
                        EraseFloor(coords);
                    }
                    break;
                case DrawMode.Tin:
                    DeleteTin(EraseFloor, coords, null);
                    break;
            }

            // Updating previous selected
            if (isMouse) PreviousMouseCoords = coords;
            else PreviousCursorCoords = coords;
        }

        // -------------------------------------------------------------------
        // EraseFloor
        // -------------------------------------------------------------------

        public void EraseFloor(int[] coords, params object[] args)
        {
            int[] portion = GetPortion(coords[0], coords[3]);
            if (IsInArea(coords) && IsInPortions(portion))
            {
                if (Map.Portions[portion] == null) Map.Portions[portion] = new GameMapPortion();
                if (Map.Portions[portion].RemoveFloor(coords) && Map.Saved) SetToNoSaved();
                AddPortionToSave(portion);
                AddPortionToUpdate(portion);
            }
        }

        // -------------------------------------------------------------------
        // PaintTin
        // -------------------------------------------------------------------

        public void PaintTin(MethodStock stock, MethodStock erase, int[] coords, int[] textureAfter)
        {
            int[] portion = GetPortion(coords[0], coords[3]);
            if (IsInArea(coords) && IsInPortions(portion))
            {
                int[] textureBefore = (Map.Portions[portion] == null) ? null : Map.Portions[portion].GetFloorTexture(coords);
                int[] textureAfterReduced = (textureAfter == null) ? null : new int[] { textureAfter[0], textureAfter[1], 1, 1 };

                if (textureBefore == null && textureAfter == null) return;

                if ((textureBefore == null && textureAfter != null) || (textureBefore != null && textureAfter == null) || !textureBefore.SequenceEqual(textureAfter))
                {
                    List<int[]> tab = new List<int[]>();
                    tab.Add(coords);
                    if (textureAfterReduced == null) erase(coords);
                    else stock(coords, textureAfterReduced);

                    int[][] adjacent;

                    while (tab.Count != 0)
                    {
                        adjacent = new int[][]
                        {
                        new int[] { tab[0][0] - 1, tab[0][1], tab[0][2], tab[0][3] },
                        new int[] { tab[0][0] + 1, tab[0][1], tab[0][2], tab[0][3] },
                        new int[] { tab[0][0], tab[0][1], tab[0][2], tab[0][3] + 1 },
                        new int[] { tab[0][0], tab[0][1], tab[0][2], tab[0][3] - 1 }
                        };
                        tab.RemoveAt(0);
                        for (int i = 0; i < adjacent.Length; i++)
                        {
                            int localX = adjacent[i][0] - coords[0], localZ = adjacent[i][3] - coords[3];
                            textureAfterReduced = (textureAfter == null) ? null : new int[] { textureAfter[0] + WANOK.Mod(localX, textureAfter[2]), textureAfter[1] + WANOK.Mod(localZ, textureAfter[3]), 1, 1 };
                            portion = GetPortion(adjacent[i][0], adjacent[i][3]);
                            if (IsInPortions(portion))
                            {
                                int[] textureHere = (Map.Portions[portion] == null) ? null : Map.Portions[portion].GetFloorTexture(adjacent[i]);

                                if ((textureBefore == null || textureHere != null) && (textureBefore != null || textureHere == null) & IsInArea(adjacent[i]) && ((textureBefore == null && textureHere == null) || textureHere.SequenceEqual(textureBefore)))
                                {
                                    if (textureAfterReduced == null) erase(adjacent[i]);
                                    else stock(adjacent[i], textureAfterReduced);
                                    tab.Add(adjacent[i]);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AddTin(MethodStock stock, int[] coords, int[] textureAfter)
        {
            PaintTin(stock, null, coords, textureAfter);
        }

        public void DeleteTin(MethodStock erase, int[] coords, int[] textureAfter)
        {
            PaintTin(null, erase, coords, textureAfter);
        }

        #endregion

        // -------------------------------------------------------------------
        // UTILS
        // -------------------------------------------------------------------

        #region Utils

        // -------------------------------------------------------------------
        // GetCoordsMouse
        // -------------------------------------------------------------------

        public int[] GetCoordsMouse()
        {
            return new int[]
            {
                (int)PointOnPlane.X,
                GridHeight[0],
                GridHeight[1],
                (int)PointOnPlane.Z
            };
        }

        // -------------------------------------------------------------------
        // GetCoordsCursor
        // -------------------------------------------------------------------

        public int[] GetCoordsCursor()
        {
            return new int[]
            {
                CursorEditor.GetX(),
                GridHeight[0],
                GridHeight[1],
                CursorEditor.GetZ()
            };
        }

        // -------------------------------------------------------------------
        // GetCoords
        // -------------------------------------------------------------------

        public int[] GetCoords(bool isMouse)
        {
            int[] coords;
            if (isMouse)
            {
                coords = GetCoordsMouse();
                if (PreviousMouseCoords != null && coords.SequenceEqual(PreviousMouseCoords)) return null;
            }
            else
            {
                coords = GetCoordsCursor();
                if (PreviousCursorCoords != null && coords.SequenceEqual(PreviousCursorCoords)) return null;
            }

            return coords;
        }

        // -------------------------------------------------------------------
        // GetPortion
        // -------------------------------------------------------------------

        public int[] GetPortion(int x, int z)
        {
            return new int[]
            {
                (x / WANOK.PORTION_SIZE) - (CursorEditor.GetX() / WANOK.PORTION_SIZE),
                (z / WANOK.PORTION_SIZE) - (CursorEditor.GetZ() / WANOK.PORTION_SIZE)
            };
        }

        // -------------------------------------------------------------------
        // IsInArea
        // -------------------------------------------------------------------

        public bool IsInArea(int[] coords)
        {
            return (coords[0] >= 0 && coords[0] < Map.MapInfos.Width && coords[3] >= 0 && coords[3] < Map.MapInfos.Height);
        }

        // -------------------------------------------------------------------
        // IsInPotion
        // -------------------------------------------------------------------

        public bool IsInPortions(int[] coords)
        {
            return (coords[0] <= WANOK.PORTION_RADIUS && coords[0] >= -WANOK.PORTION_RADIUS && coords[1] <= WANOK.PORTION_RADIUS && coords[1] >= -WANOK.PORTION_RADIUS);
        }

        // -------------------------------------------------------------------
        // DisposeBuffers
        // -------------------------------------------------------------------

        public void DisposeBuffers(int i, int j)
        {
            if (Map.Portions[new int[] { i, j }] != null)
            {
                Map.DisposeBuffers(new int[] { i, j });
            }
        }

        // -------------------------------------------------------------------
        // TraceLine
        // -------------------------------------------------------------------

        public void TraceLine(int[] previousCoords, int[] coords, MethodStock stock, params object[] args)
        {
            int x1 = previousCoords[0], x2 = coords[0];
            int y1 = coords[1], y2 = coords[2];
            int z1 = previousCoords[3], z2 = coords[3];
            int dx = x2 - x1, dz = z2 - z1;
            bool test = true;

            if (dx != 0)
            {
                if (dx > 0)
                {
                    if (dz != 0)
                    {
                        if (dz > 0)
                        {
                            if (dx >= dz)
                            {
                                int e = dx;
                                dx = 2 * e;
                                dz = dz * 2;

                                while (test)
                                {
                                    stock(new int[] { x1, y1, y2, z1 }, args);
                                    x1++;
                                    if (x1 == x2) break;
                                    e -= dz;
                                    if (e < 0)
                                    {
                                        z1++;
                                        e += dx;
                                    }
                                }
                            }
                            else
                            {
                                int e = dz;
                                dz = 2 * e;
                                dx = dx * 2;

                                while (test)
                                {
                                    stock(new int[] { x1, y1, y2, z1 }, args);
                                    z1++;
                                    if (z1 == z2) break;
                                    e -= dx;
                                    if (e < 0)
                                    {
                                        x1++;
                                        e += dz;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dx >= -dz)
                            {
                                int e = dx;
                                dx = 2 * e;
                                dz = dz * 2;

                                while (test)
                                {
                                    stock(new int[] { x1, y1, y2, z1 }, args);
                                    x1++;
                                    if (x1 == x2) break;
                                    e += dz;
                                    if (e < 0)
                                    {
                                        z1--;
                                        e += dx;
                                    }
                                }
                            }
                            else
                            {
                                int e = dz;
                                dz = 2 * e;
                                dx = dx * 2;

                                while (test)
                                {
                                    stock(new int[] { x1, y1, y2, z1 }, args);
                                    z1--;
                                    if (z1 == z2) break;
                                    e += dx;
                                    if (e > 0)
                                    {
                                        x1++;
                                        e += dz;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        while (x1 != x2)
                        {
                            stock(new int[] { x1, y1, y2, z1 }, args);
                            x1++;
                        }
                    }
                }
                else
                {
                    dz = z2 - z1;
                    if (dz != 0)
                    {
                        if (dz > 0)
                        {
                            if (-dx >= dz)
                            {
                                int e = dx;
                                dx = 2 * e;
                                dz = dz * 2;

                                while (test)
                                {
                                    stock(new int[] { x1, y1, y2, z1 }, args);
                                    x1--;
                                    if (x1 == x2) break;
                                    e += dz;
                                    if (e >= 0)
                                    {
                                        z1++;
                                        e += dx;
                                    }
                                }
                            }
                            else
                            {
                                int e = dz;
                                dz = 2 * e;
                                dx = dx * 2;

                                while (test)
                                {
                                    stock(new int[] { x1, y1, y2, z1 }, args);
                                    z1++;
                                    if (z1 == z2) break;
                                    e += dx;
                                    if (e <= 0)
                                    {
                                        x1--;
                                        e += dz;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dx <= dz)
                            {
                                int e = dx;
                                dx = 2 * e;
                                dz = dz * 2;

                                while (test)
                                {
                                    stock(new int[] { x1, y1, y2, z1 }, args);
                                    x1--;
                                    if (x1 == x2) break;
                                    e -= dz;
                                    if (e >= 0)
                                    {
                                        z1--;
                                        e += dx;
                                    }
                                }
                            }
                            else
                            {
                                int e = dz;
                                dz = 2 * e;
                                dx = dx * 2;

                                while (test)
                                {
                                    stock(new int[] { x1, y1, y2, z1 }, args);
                                    z1--;
                                    if (z1 == z2) break;
                                    e -= dx;
                                    if (e >= 0)
                                    {
                                        x1--;
                                        e += dz;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        while (x1 != x2)
                        {
                            stock(new int[] { x1, y1, y2, z1 }, args);
                            x1--;
                        }
                    }
                }
            }
            else
            {
                dz = z2 - z1;
                if (dz != 0)
                {
                    if (dz > 0)
                    {
                        while(z1 != z2)
                        {
                            stock(new int[] { x1, y1, y2, z1 }, args);
                            z1++;
                        }
                    }
                    else
                    {
                        while (z1 != z2)
                        {
                            stock(new int[] { x1, y1, y2, z1 }, args);
                            z1--;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
