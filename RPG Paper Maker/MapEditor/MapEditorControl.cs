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
        public Vector3 PointOnPlane;
        public int GridHeight = 0;
        public int[] CurrentTexture = new int[] { 0, 0, WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE };
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
            float distance = (GridHeight - camera.Position.Y) / ray.Direction.Y;
            PointOnPlane = WANOK.GetCorrectPointOnRay(ray, camera, distance);

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
            if (!PortionsToUpdate.Contains(portion)) PortionsToUpdate.Add(portion);
        }

        public void AddPortionToSave(int[] portion)
        {
            if (!PortionsToSave.Contains(portion)) PortionsToSave.Add(portion);
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

            for (int i = 0; i < PortionsToUpdate.Count; i++)
            {
                Map.GenFloor(PortionsToUpdate[i]);
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
            if (WANOK.MapMouseManager.IsButtonUp(MouseButtons.Left))
            {
                PreviousMouseCoords = null;
            }
        }

        // -------------------------------------------------------------------
        // Add
        // -------------------------------------------------------------------

        public void Add(bool isMouse)
        {
            switch (SelectedDrawType)
            {
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
        // AddFloor
        // -------------------------------------------------------------------

        public void AddFloor(bool isMouse)
        {
            // Getting coords
            int[] coords;
            if (isMouse)
            {
                coords = GetCoordsMouse();
            }
            else
            {
                coords = GetCoordsCursor();
            }

            // Drawing squares
            if (isMouse) {
                if (PreviousMouseCoords != null) TraceLine(PreviousMouseCoords, coords, StockFloor, CurrentTexture);
            }
            StockFloor(coords, CurrentTexture);

            // Updating previous selected
            if (isMouse) PreviousMouseCoords = coords;
            else PreviousCursorCoords = coords;
        }

        // -------------------------------------------------------------------
        // StockFloor
        // -------------------------------------------------------------------

        public void StockFloor(int[] coords, params object[] args)
        {
            int[] portion = GetPortion(coords[0], coords[2]);

            if (IsInArea(coords) && IsInPortions(portion))
            {
                int[] texture = (int[])args[0];

                if (Map.Portions[portion] == null) Map.Portions[portion] = new GameMapPortion();
                Map.Portions[portion].AddFloor(coords, texture);
                AddPortionToSave(portion);
                AddPortionToUpdate(portion);
            }
        }

        // -------------------------------------------------------------------
        // RemoveFloor
        // -------------------------------------------------------------------

        public void RemoveFloor(bool isMouse)
        {

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
                (int)PointOnPlane.Y,
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
                GridHeight,
                CursorEditor.GetZ()
            };
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
            return (coords[0] >= 0 && coords[0] < Map.MapInfos.Width && coords[2] >= 0 && coords[2] < Map.MapInfos.Height);
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
            int y = coords[1];
            int z1 = previousCoords[2], z2 = coords[2];
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
                                    stock(new int[] { x1, y, z1 }, args);
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
                                    stock(new int[] { x1, y, z1 }, args);
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
                                    stock(new int[] { x1, y, z1 }, args);
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
                                    stock(new int[] { x1, y, z1 }, args);
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
                            stock(new int[] { x1, y, z1 }, args);
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
                                    stock(new int[] { x1, y, z1 }, args);
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
                                    stock(new int[] { x1, y, z1 }, args);
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
                                    stock(new int[] { x1, y, z1 }, args);
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
                                    stock(new int[] { x1, y, z1 }, args);
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
                            stock(new int[] { x1, y, z1 }, args);
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
                            stock(new int[] { x1, y, z1 }, args);
                            z1++;
                        }
                    }
                    else
                    {
                        while (z1 != z2)
                        {
                            stock(new int[] { x1, y, z1 }, args);
                            z1--;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
