using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected List<int[]> PortionsToUpdate = new List<int[]>();
        protected List<int[]> PortionsToSave = new List<int[]>();



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
            PortionsToUpdate = new List<int[]>();
            PortionsToSave = new List<int[]>();
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        public void Update(GraphicsDevice graphicsDevice, Camera camera)
        {
            // Raycasting
            Ray ray = WANOK.CalculateRay(new Vector2(MouseBeforeUpdate.X, MouseBeforeUpdate.Y), camera.View, camera.Projection, graphicsDevice.Viewport);
            float distance = (GridHeight - camera.Position.Y) / ray.Direction.Y;
            PointOnPlane = WANOK.GetCorrectPointOnRay(ray, camera, distance);

            // Update portions
            UpdatePortions();
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

                if ((Map.Portions.ContainsKey(PortionsToSave[i]) && Map.Portions[PortionsToSave[i]].IsEmpty()) || !Map.Portions.ContainsKey(PortionsToSave[i]))
                {
                    Map.Portions.Remove(PortionsToSave[i]);
                    if (File.Exists(path)) File.Delete(path);
                }
                else
                {
                    var lol = Map.Portions[PortionsToSave[i]];
                    Map.Portions[PortionsToSave[i]].SaveDatas(path);
                }
            }

            for (int i = 0; i < PortionsToUpdate.Count; i++)
            {
                Map.GenFloor(PortionsToUpdate[i]);
            }

            ClearPortions();
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
            int[] coords, portion;

            if (isMouse)
            {
                coords = GetCoordsMouse();
                portion = GetPortionMouse();
            }
            else
            {
                coords = GetCoordsCursor();
                portion = GetPortionCursor();
            }

            StockFloor(coords, portion);
        }

        // -------------------------------------------------------------------
        // StockFloor
        // -------------------------------------------------------------------

        public void StockFloor(int[] coords, int[] portion)
        {
            if (IsInArea(coords) && IsInPortions(portion))
            {
                if (!Map.Portions.ContainsKey(portion)) Map.Portions[portion] = new GameMapPortion();
                Map.Portions[portion].AddFloor(coords, CurrentTexture);
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
        // GetPortionMouse
        // -------------------------------------------------------------------

        public int[] GetPortionMouse()
        {
            return new int[]
            {
                ((int)PointOnPlane.X / WANOK.PORTION_SIZE) - (CursorEditor.GetX() / WANOK.PORTION_SIZE),
                ((int)PointOnPlane.Z / WANOK.PORTION_SIZE) - (CursorEditor.GetZ() / WANOK.PORTION_SIZE)
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
        // GetPortionCursor
        // -------------------------------------------------------------------

        public int[] GetPortionCursor()
        {
            return new int[]
            {
                CursorEditor.GetX() / WANOK.PORTION_SIZE,
                CursorEditor.GetZ() / WANOK.PORTION_SIZE
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

    }
}
