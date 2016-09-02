using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
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
        public DrawType SelectedDrawTypeParticular = DrawType.Floors;
        public DrawMode DrawMode = DrawMode.Pencil;
        public int[] PointOnPlane = null;
        public int[] PointOnFloor = null;
        public int[] PointOnSprites = null;
        public int[] PointOnMountains = null;
        public float PlaneDistance = 0;
        public float FloorDistance = 0;
        public float SpriteDistance = 0;
        public float MountainDistance = 0;
        public bool IsGridOnTop = false;
        public int[] GridHeight { get { return Map.GridHeight; } set { Map.GridHeight = value; } }
        public int[] CurrentTexture = new int[] { 0, 0, 1, 1 };
        public int CurrentSpecialItemId = -1;
        public int[] CurrentPosition = new int[] { 0, 0 };
        public int CurrentOrientation = 0;
        public int[] CurrentPortion = new int[] { 0, 0 };
        public int[] CurrentMountainHeight = new int[] { 1, 0 };
        protected HashSet<int[]> PortionsToUpdate = new HashSet<int[]>(new IntArrayComparer());
        protected HashSet<int[]> PortionsAutotileToUpdate = new HashSet<int[]>(new IntArrayComparer());
        protected HashSet<int[]> PortionsMountainToUpdate = new HashSet<int[]>(new IntArrayComparer());
        protected HashSet<int[]> PortionsToSave = new HashSet<int[]>(new IntArrayComparer());
        public int[] PreviousMouseCoords = null;
        public int[] PreviousCursorCoords = null;
        public bool OpenEvent = false;

        public delegate void MethodStock(int[] coords, params object[] args);
        public delegate object MethodReduce(object after, int localX, int localZ);


        // -------------------------------------------------------------------
        // UNDO/REDO
        // -------------------------------------------------------------------

        #region Undo/Redo


        public void Undo()
        {
            if (WANOK.CancelRedoIndex[Map.MapInfos.RealMapName] > 0)
            {
                foreach (KeyValuePair<int[], object> entry in WANOK.CancelRedo[Map.MapInfos.RealMapName][WANOK.CancelRedoIndex[Map.MapInfos.RealMapName] - 1])
                {
                    UndoRedoApplyChanges(entry.Key, entry.Value);
                }
                WANOK.CancelRedoIndex[Map.MapInfos.RealMapName]--;
                SetToNoSaved();
            }
        }

        public void Redo()
        {
            if (WANOK.CancelRedoIndex[Map.MapInfos.RealMapName] < WANOK.CancelRedo[Map.MapInfos.RealMapName].Count - 1)
            {
                foreach (KeyValuePair<int[], object> entry in WANOK.CancelRedo[Map.MapInfos.RealMapName][WANOK.CancelRedoIndex[Map.MapInfos.RealMapName] + 1])
                {
                    UndoRedoApplyChanges(entry.Key, entry.Value);
                }
                WANOK.CancelRedoIndex[Map.MapInfos.RealMapName]++;
                SetToNoSaved();
            }
        }

        public void UndoRedoApplyChanges(int[] portion, object obj)
        {
            if (portion[0] == 0)
            {
                int[] globalPortion = new int[] { portion[1], portion[2] };
                int[] localPortion = GetLocalPortion(globalPortion);
                if (WANOK.IsInPortions(localPortion, 1))
                {
                    Map.DisposePortionBuffers(localPortion, false);
                    Map.Portions[localPortion] = obj == null ? new GameMapPortion() : ((GameMapPortion)obj).CreateCopy();
                    Map.UpdatePortions(localPortion);
                    SavePortion(localPortion);
                }
                else
                {
                    WANOK.SavePortionMap((GameMapPortion)obj, Map.MapInfos.RealMapName, globalPortion[0], globalPortion[1]);
                }
            }
            else if (portion[0] == 1)
            {
                int[] globalPortion = new int[] { portion[1], portion[2] };
                int[] localPortion = GetLocalPortion(globalPortion);
                if (obj == null) Map.Events.CompleteList.Remove(globalPortion);
                else
                {
                    Dictionary<int[], SystemEvent> list = (Dictionary<int[], SystemEvent>)obj;
                    Dictionary<int[], SystemEvent> listCopy = new Dictionary<int[], SystemEvent>(new IntArrayComparer());
                    foreach (int[] coords in list.Keys)
                    {
                        listCopy[coords] = list[coords].CreateCopy();
                    }
                    Map.Events.CompleteList[globalPortion] = listCopy;
                }

                if (WANOK.IsInPortions(localPortion, 1))
                {
                    Map.DisposeEventBuffers(localPortion, false);
                    Map.EventsPortions[localPortion] = new EventsPortion(Map.Events.CompleteList.ContainsKey(globalPortion) ? Map.Events.CompleteList[globalPortion] : null);
                    Map.GenEvent(localPortion, globalPortion);
                }
                WANOK.SaveEventsMap(Map.Events, Map.MapInfos.RealMapName);
            }
            else if (portion[0] == 2)
            {
                WANOK.SaveMapInfos((MapInfos)obj, ((MapInfos)obj).RealMapName);
                WANOK.MapEditor.ReLoadMap(((MapInfos)obj).RealMapName);
            }
        }

        #endregion

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
            float? newDistance;
            Ray ray = WANOK.CalculateRay(new Vector2(MouseBeforeUpdate.X, MouseBeforeUpdate.Y), camera.View, camera.Projection, graphicsDevice.Viewport);
            int height = PreviousMouseCoords == null ? WANOK.GetPixelHeight(GridHeight) : PreviousMouseCoords[1] * WANOK.SQUARE_SIZE + PreviousMouseCoords[2];
            PlaneDistance = (height - camera.Position.Y) / ray.Direction.Y;
            if (PlaneDistance < 0) PointOnPlane = null;
            else
            {
                PointOnPlane = WANOK.GetCorrectPointOnRay(ray, camera, PlaneDistance, height);
            }

            // Raycasting floor
            PointOnFloor = null;
            FloorDistance = 0;
            for (int i = -WANOK.PORTION_RADIUS; i <= WANOK.PORTION_RADIUS; i++)
            {
                for (int j = -WANOK.PORTION_RADIUS; j <= WANOK.PORTION_RADIUS; j++)
                {
                    GameMapPortion gameMapPortion = Map.Portions[new int[] { i, j }];
                    if (gameMapPortion != null)
                    {
                        foreach (int[] coords in gameMapPortion.Floors.Keys)
                        {
                            FloorDistance = RaycastFloor(coords, camera, ray, FloorDistance);
                        }
                        foreach (Autotiles autotiles in gameMapPortion.Autotiles.Values)
                        {
                            foreach (int[] coords in autotiles.Tiles.Keys)
                            {
                                FloorDistance = RaycastFloor(coords, camera, ray, FloorDistance);
                            }
                        }
                    }
                }
            }
            IsGridOnTop = (PointOnPlane == null || PointOnFloor == null || !IsInArea(PointOnPlane)) ? false : PlaneDistance < FloorDistance;
            
            // Raycasting sprites
            PointOnSprites = null;
            SpriteDistance = 0;
            if (SelectedDrawType == "ItemSprite")
            {
                for (int i = -WANOK.PORTION_RADIUS; i <= WANOK.PORTION_RADIUS; i++)
                {
                    for (int j = -WANOK.PORTION_RADIUS; j <= WANOK.PORTION_RADIUS; j++)
                    {
                        GameMapPortion gameMapPortion = Map.Portions[new int[] { i, j }];
                        if (gameMapPortion != null)
                        {
                            foreach (KeyValuePair<int[], Sprites> entry1 in gameMapPortion.Sprites)
                            {
                                foreach (KeyValuePair<int[], Sprite> entry2 in entry1.Value.ListSprites)
                                {
                                    newDistance = entry2.Value.GetCompleteDistanceIntersection(ray, camera, entry2.Key, entry1.Key[2] * WANOK.SQUARE_SIZE, entry1.Key[3] * WANOK.SQUARE_SIZE);

                                    if (newDistance != null)
                                    {
                                        if (PointOnSprites == null || SpriteDistance > newDistance.Value)
                                        {
                                            if (newDistance.Value > 0)
                                            {
                                                PointOnSprites = entry2.Key;
                                                SpriteDistance = newDistance.Value;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Raycasting mountains
            PointOnMountains = null;
            MountainDistance = 0;
            for (int i = -WANOK.PORTION_RADIUS; i <= WANOK.PORTION_RADIUS; i++)
            {
                for (int j = -WANOK.PORTION_RADIUS; j <= WANOK.PORTION_RADIUS; j++)
                {
                    GameMapPortion gameMapPortion = Map.Portions[new int[] { i, j }];
                    if (gameMapPortion != null)
                    {
                        foreach (KeyValuePair<int, Mountains> entry1 in gameMapPortion.Mountains)
                        {
                            foreach (MountainsGroup group in entry1.Value.Groups.Values)
                            {
                                foreach (KeyValuePair<int[], Mountain> entry2 in group.Tiles)
                                {
                                    newDistance = entry2.Value.GetDistanceIntersection(new Ray(ray.Position, ray.Direction), camera, entry2.Key);

                                    if (newDistance != null)
                                    {
                                        if (PointOnMountains == null || MountainDistance > newDistance.Value)
                                        {
                                            if (newDistance.Value > 0)
                                            {
                                                PointOnMountains = entry2.Key;
                                                MountainDistance = newDistance.Value;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Update portions
            UpdatePortions();

            // Portion moving
            int[] newPortion = CursorEditor.GetPortion();
            if (newPortion[0] != CurrentPortion[0] || newPortion[1] != CurrentPortion[1])
            {
                UpdateMovingPortion(newPortion, CurrentPortion);
            }
            CurrentPortion = newPortion;

            // Save portions
            SavePortions();
            

            if (PortionsToUpdate.Count > 0 && DrawMode == DrawMode.Tin && WANOK.DialogProgressBar != null)
            {
                WANOK.DisposeProgressBar();
            }

            ClearPortions();
        }

        // -------------------------------------------------------------------
        // RaycastFloor
        // -------------------------------------------------------------------

        public float RaycastFloor(int[] coords, Camera camera, Ray ray, float floorDistance)
        {
            int height = coords[1] * WANOK.SQUARE_SIZE + coords[2];
            float distance = (height - camera.Position.Y) / ray.Direction.Y;
            int[] c = WANOK.GetCorrectPointOnRay(ray, camera, distance, height);
            if (coords[0] == c[0] && coords[3] == c[3])
            {
                if (PointOnFloor == null || floorDistance > distance)
                {
                    if (distance > 0)
                    {
                        PointOnFloor = coords;
                        floorDistance = distance;
                    }
                }
            }

            return floorDistance;
        }

        // -------------------------------------------------------------------
        // Portions
        // -------------------------------------------------------------------

        public void AddPortionToUpdate(int[] portion)
        {
            PortionsToUpdate.Add(portion);
        }

        public void AddPortionsAutotileToUpdate(int[] portion)
        {
            PortionsAutotileToUpdate.Add(portion);
        }

        public void AddPortionsMountainToUpdate(int[] portion)
        {
            PortionsMountainToUpdate.Add(portion);
        }

        public void AddPortionToSave(int[] portion)
        {
            PortionsToSave.Add(portion);
        }

        public void ClearPortions()
        {
            PortionsToUpdate.Clear();
            PortionsAutotileToUpdate.Clear();
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
                for (int j = -WANOK.PORTION_RADIUS - 1; j <= WANOK.PORTION_RADIUS + 1; j++)
                {
                    for (int i = -WANOK.PORTION_RADIUS - 1; i < WANOK.PORTION_RADIUS + 1; i++)
                    {
                        SetPortion(i, j, i + 1, j);
                    }
                    LoadPortion(currentPortion, WANOK.PORTION_RADIUS + 1, j);
                }
            }
            // If cursor going to left side
            else if (currentPortion[0] < previousPortion[0])
            {
                for (int j = -WANOK.PORTION_RADIUS - 1; j <= WANOK.PORTION_RADIUS + 1; j++)
                {
                    for (int i = WANOK.PORTION_RADIUS + 1; i > -WANOK.PORTION_RADIUS - 1; i--)
                    {
                        SetPortion(i, j, i - 1, j);
                    }
                    LoadPortion(currentPortion, -WANOK.PORTION_RADIUS - 1, j);
                }
            }
            // If cursor going to up side
            if (currentPortion[1] > previousPortion[1])
            {
                for (int i = -WANOK.PORTION_RADIUS - 1; i <= WANOK.PORTION_RADIUS + 1; i++)
                {
                    for (int j = -WANOK.PORTION_RADIUS - 1; j < WANOK.PORTION_RADIUS + 1; j++)
                    {
                        SetPortion(i, j, i, j + 1);
                    }
                    LoadPortion(currentPortion, i, WANOK.PORTION_RADIUS + 1);
                }
            }
            // If cursor going to down side
            else if (currentPortion[1] < previousPortion[1])
            {
                for (int i = -WANOK.PORTION_RADIUS - 1; i <= WANOK.PORTION_RADIUS + 1; i++)
                {
                    for (int j = WANOK.PORTION_RADIUS + 1; j > -WANOK.PORTION_RADIUS - 1; j--)
                    {
                        SetPortion(i, j, i, j - 1);
                    }
                    LoadPortion(currentPortion, i, -WANOK.PORTION_RADIUS - 1);
                }
            }
        }

        // -------------------------------------------------------------------
        // SetPortion
        // -------------------------------------------------------------------

        public void SetPortion(int i, int j, int k, int l)
        {
            DisposeBuffers(i, j, false);
            Map.Portions[new int[] { i, j }] = Map.Portions[new int[] { k, l }];
            Map.EventsPortions[new int[] { i, j }] = Map.EventsPortions[new int[] { k, l }];
        }

        // -------------------------------------------------------------------
        // LoadPortion
        // -------------------------------------------------------------------

        public void LoadPortion(int[] currentPortion, int i, int j)
        {
            DisposeBuffers(i, j, false);
            Map.LoadPortion(currentPortion[0] + i, currentPortion[1] + j, i, j);
        }

        // -------------------------------------------------------------------
        // UpdatePortions
        // -------------------------------------------------------------------

        public void UpdatePortions()
        {
            if (PortionsToUpdate.Count > 0 && WANOK.DialogProgressBar != null && DrawMode == DrawMode.Tin) WANOK.DialogProgressBar.SetValue("Drawing...", 90);

            foreach (int[] portion in PortionsAutotileToUpdate)
            {
                foreach (Autotiles autotiles in Map.Portions[portion].Autotiles.Values)
                {
                    foreach (int[] coords in autotiles.Tiles.Keys)
                    {
                        Map.Portions[portion].Autotiles[autotiles.Id].Update(coords, portion);
                    }
                }
            }

            foreach (int[] portion in PortionsMountainToUpdate)
            {
                foreach (KeyValuePair<int, Mountains> entry in Map.Portions[portion].Mountains)
                {
                    foreach (int id in entry.Value.Groups.Keys)
                    {
                        foreach (KeyValuePair<int[], Mountain> entry2 in entry.Value.Groups[id].Tiles)
                        {
                            entry2.Value.Update(entry.Value, entry2.Key, portion, entry.Key);
                        }
                    }
                }
            }

            foreach (int[] portion in PortionsToUpdate)
            {
                Map.UpdatePortions(portion);
            }
        }

        // -------------------------------------------------------------------
        // SavePortions
        // -------------------------------------------------------------------

        public void SavePortions()
        {
            if (PortionsToSave.Count > 0 && WANOK.DialogProgressBar != null && DrawMode == DrawMode.Tin) WANOK.DialogProgressBar.SetValue("Saving...", 100);
            foreach (int[] portion in PortionsToSave)
            {
                SavePortion(portion);
            }
        }

        public void SavePortion(int[] portion)
        {
            int x = portion[0] + (CursorEditor.GetX() / WANOK.PORTION_SIZE);
            int y = portion[1] + (CursorEditor.GetZ() / WANOK.PORTION_SIZE);
            string path = Path.Combine(WANOK.MapsDirectoryPath, Map.MapInfos.RealMapName, "temp", x + "-" + y + ".pmap");

            if (((Map.Portions[portion] != null) && Map.Portions[portion].IsEmpty()) || Map.Portions[portion] == null)
            {
                Map.Portions[portion] = null;
                if (File.Exists(path)) File.Delete(path);
            }
            else
            {
                WANOK.SaveBinaryDatas(Map.Portions[portion], path);
            }
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
                if (SelectedDrawType != "ItemStart") WANOK.LoadCancel(Map.MapInfos.RealMapName);
            }
            if (WANOK.KeyboardManager.IsButtonUp(WANOK.Settings.KeyboardAssign.EditorDrawCursor) || WANOK.KeyboardManager.IsButtonUp(WANOK.Settings.KeyboardAssign.EditorRemoveCursor))
            {
                PreviousCursorCoords = null;
                if (SelectedDrawType != "ItemStart") WANOK.LoadCancel(Map.MapInfos.RealMapName);
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
        // CreateCancel
        // -------------------------------------------------------------------

        public void CreateCancel(bool flagOK = false)
        {
            WANOK.CreateCancel(Map.MapInfos.RealMapName, flagOK);
        }

        // -------------------------------------------------------------------
        // Add
        // -------------------------------------------------------------------

        public void Add(bool isMouse)
        {
            WANOK.CanStartCancel = true;
            switch (SelectedDrawType)
            {
                case "ItemStart":
                    AddStart(isMouse);
                    break;
                case "ItemEvent":
                    AddEvent(isMouse);
                    break;
                case "ItemFloor":
                    if (SelectedDrawTypeParticular == DrawType.Autotiles && MapEditor.TexAutotiles.Count == 0) return;
                    AddFloor(isMouse);
                    break;
                case "ItemSprite":
                    AddSprite(isMouse);
                    break;
                case "ItemRelief":
                    if (SelectedDrawTypeParticular == DrawType.Montains) AddMontain(isMouse);
                    break;
            }
        }

        // -------------------------------------------------------------------
        // Remove
        // -------------------------------------------------------------------

        public void Remove(bool isMouse)
        {
            WANOK.CanStartCancel = true;
            switch (SelectedDrawType)
            {
                case "ItemFloor":
                    RemoveFloor(isMouse);
                    break;
                case "ItemSprite":
                    RemoveSprite(isMouse);
                    break;
                case "ItemRelief":
                    if (SelectedDrawTypeParticular == DrawType.Montains) RemoveMountain(isMouse);
                    break;
            }
        }

        // -------------------------------------------------------------------
        // START
        // -------------------------------------------------------------------

        #region start

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
                WANOK.Game.System = system;

                // Updating in map
                Map.SetStartInfos(coords);
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // EVENT
        // -------------------------------------------------------------------

        #region event

        // -------------------------------------------------------------------
        // AddEvent
        // -------------------------------------------------------------------

        public void AddEvent(bool isMouse)
        {
            // Getting coords
            int[] coords = GetCoords(isMouse);
            if (coords == null) return;

            if (IsInArea(coords))
            {
                int[] globalPortion = GetGlobalPortion(coords[0], coords[3]);
                int[] evCoords = null;
                
                if (Map.Events.CompleteList.ContainsKey(globalPortion))
                {
                    foreach (int[] coords2 in Map.Events.CompleteList[globalPortion].Keys)
                    {
                        if (coords.SequenceEqual(coords2))
                        {
                            evCoords = coords2;
                            break;
                        }
                    }
                }

                SystemEvent ev = evCoords == null ? new SystemEvent(WANOK.GetStringEvent(Map.Events.Count() + 1)) : Map.Events.CompleteList[globalPortion][evCoords];
                WANOK.KeyboardManager.InitializeKeyboard();
                WANOK.MapMouseManager.InitializeMouse();
                DialogEvent dialog = new DialogEvent(ev);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    CreateCancel(true);
                    int[] portion = GetPortion(coords[0], coords[3]);
                    Map.Events.Add(globalPortion, coords, dialog.GetEvent());
                    Map.EventsPortions[portion].RemoveSprite(coords);
                    if (dialog.GetEvent().Pages[0].GraphicDrawType != DrawType.None)
                    {
                        Map.LoadSpriteTexture(dialog.GetEvent().Pages[0].Graphic);
                        Map.EventsPortions[portion].AddSprite(coords, dialog.GetEvent());
                    }
                    WANOK.AddPortionsToAddCancel(Map.MapInfos.RealMapName, globalPortion, 1);
                    WANOK.SaveBinaryDatas(Map.Events, Path.Combine(WANOK.MapsDirectoryPath, Map.MapInfos.RealMapName, "temp", "events.map"));
                    WANOK.LoadCancel(Map.MapInfos.RealMapName);
                    Map.GenEvent(portion, globalPortion);
                    SetToNoSaved();
                }
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // FLOORS
        // -------------------------------------------------------------------

        #region floors

        // -------------------------------------------------------------------
        // AddFloor
        // -------------------------------------------------------------------

        public void AddFloor(bool isMouse)
        {
            // Texture
            object texture = null;
            switch (SelectedDrawTypeParticular)
            {
                case DrawType.Floors:
                    texture = CurrentTexture;
                    break;
                case DrawType.Autotiles:
                    if (CurrentSpecialItemId == -1)
                    {
                        SystemSounds.Beep.Play();
                        return;
                    }
                    texture = CurrentSpecialItemId;
                    break;
            }

            // Getting coords
            int[] coords = GetCoords(isMouse);
            if (coords == null)
            {
                return;
            }

            // Drawing squares
            switch (DrawMode){
                case DrawMode.Pencil:
                    if (CurrentTexture[2] == 1 && CurrentTexture[3] == 1 || SelectedDrawTypeParticular != DrawType.Floors)
                    {
                        if (isMouse)
                        {
                            if (PreviousMouseCoords != null) TraceLine(PreviousMouseCoords, coords, StockFloor, texture);
                        }
                        else if (PreviousCursorCoords != null) TraceLine(PreviousCursorCoords, coords, StockFloor, texture);
                    }
                    int width = 1, height = 1;
                    if (SelectedDrawTypeParticular == DrawType.Floors)
                    {
                        width = CurrentTexture[2];
                        height = CurrentTexture[3];
                    }

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if ((coords[0] + i) > Map.MapInfos.Width || (coords[3] + j) > Map.MapInfos.Height) break;
                            object shortTexture = null;
                            switch (SelectedDrawTypeParticular)
                            {
                                case DrawType.Floors:
                                    shortTexture = new int[] { i + CurrentTexture[0], j + CurrentTexture[1], 1, 1 };
                                    break;
                                case DrawType.Autotiles:
                                    shortTexture = CurrentSpecialItemId;
                                    break;
                            }
                            int[] shortCoords = new int[] { coords[0] + i, coords[1], coords[2], coords[3] + j };
                            StockFloor(shortCoords, shortTexture);
                        }
                    }
                    break;
                case DrawMode.Tin:
                    PaintTinFloor(coords, texture);
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
            if (IsInArea(coords) && WANOK.IsInPortions(portion))
            {
                CreateCancel();
                if (Map.Portions[portion] == null) Map.Portions[portion] = new GameMapPortion();
                switch (SelectedDrawTypeParticular)
                {
                    case DrawType.Floors:
                        if (Map.Portions[portion].AddFloor(coords, (int[])args[0]) && Map.Saved) SetToNoSaved();
                        break;
                    case DrawType.Autotiles:
                        if (Map.Portions[portion].AddAutotile(coords, (int)args[0], DrawMode != DrawMode.Tin) && Map.Saved) SetToNoSaved();
                        break;
                }
                AddPortionToSave(portion);
                AddPortionToUpdate(portion);
                WANOK.AddPortionsToAddCancel(Map.MapInfos.RealMapName, GetGlobalPortion(portion));
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
                    if (isMouse) if (PreviousMouseCoords != null) TraceLine(PreviousMouseCoords, coords, EraseFloor);
                    else if (PreviousCursorCoords != null) TraceLine(PreviousCursorCoords, coords, EraseFloor);
                    EraseFloor(coords);
                    break;
                case DrawMode.Tin:
                    PaintTinFloor(coords, null);
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
            if (IsInArea(coords) && WANOK.IsInPortions(portion))
            {
                CreateCancel();
                if (Map.Portions[portion] == null) Map.Portions[portion] = new GameMapPortion();
                if (Map.Portions[portion].RemoveFloor(coords) && Map.Saved) SetToNoSaved();
                AddPortionToSave(portion);
                AddPortionToUpdate(portion);
                WANOK.AddPortionsToAddCancel(Map.MapInfos.RealMapName, GetGlobalPortion(portion));
            }
        }

        // -------------------------------------------------------------------
        // PaintTinFloor
        // -------------------------------------------------------------------

        public void PaintTinFloor(int[] coords, object textureAfter)
        {
            int[] portion = GetPortion(coords[0], coords[3]);
            if (IsInArea(coords) && WANOK.IsInPortions(portion))
            {
                object textureBefore = GetCurrentTexture(portion, coords);
                object textureAfterReduced = GetTextureAfterReduced(textureAfter, 0, 0);

                if (!AreTexturesEquals(textureBefore, textureAfterReduced))
                {
                    Stopwatch sw = new Stopwatch();
                    bool t = true;
                    sw.Start();

                    List<int[]> tab = new List<int[]>();
                    tab.Add(coords);
                    if (textureAfter == null) EraseFloor(coords);
                    else StockFloor(coords, textureAfterReduced);
                    int[][] adjacent;

                    while (tab.Count != 0)
                    {
                        if (sw.ElapsedMilliseconds > 50 && t)
                        {
                            WANOK.StartProgressBar("Calculating paint propagation...", 50);
                            t = false;
                        }

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
                            textureAfterReduced = GetTextureAfterReduced(textureAfter, localX, localZ);
                            portion = GetPortion(adjacent[i][0], adjacent[i][3]);

                            if (WANOK.IsInPortions(portion) && IsInArea(adjacent[i]))
                            {
                                object textureHere = GetCurrentTexture(portion, adjacent[i]);

                                if (AreTexturesEquals(textureHere, textureBefore))
                                {
                                    if (textureAfter == null) EraseFloor(adjacent[i]);
                                    else StockFloor(adjacent[i], textureAfterReduced);
                                    tab.Add(adjacent[i]);
                                }
                            }
                        }
                    }
                    sw.Stop();
                }
            }
        }

        // -------------------------------------------------------------------
        // GetCurrentTexture
        // -------------------------------------------------------------------

        public object GetCurrentTexture(int[] portion, int[] coords)
        {
            if (Map.Portions[portion] != null)
            {
                int[] floor = Map.Portions[portion].ContainsFloor(coords);
                if (!floor.SequenceEqual(new int[] { 0, 0, 0, 0 })) return floor;
                int autotile = Map.Portions[portion].ContainsAutotiles(coords);
                if (autotile != -1) return autotile;
            }

            return new int[] { 0, 0, 0, 0 };
        }

        // -------------------------------------------------------------------
        // GetTextureafterReduced
        // -------------------------------------------------------------------

        public object GetTextureAfterReduced(object textureAfter, int localX, int localZ)
        {
            if (textureAfter != null)
            {
                if (textureAfter.GetType() == typeof(int[]))
                {
                    int[] tab = (int[])textureAfter;
                    return new int[] { tab[0] + WANOK.Mod(localX, tab[2]), tab[1] + WANOK.Mod(localZ, tab[3]), 1, 1 };
                }
                if (textureAfter.GetType() == typeof(int)) return textureAfter;
            }

            return new int[] { 0, 0, 0, 0 };
        }

        // -------------------------------------------------------------------
        // AreTexturesEquals
        // -------------------------------------------------------------------

        public bool AreTexturesEquals(object textureA, object textureB)
        {
            if (textureA.GetType() != textureB.GetType()) return false;
            if (textureA.GetType() == typeof(int[]))
            {
                return ((int[])textureA).SequenceEqual((int[])textureB);
            }
            else if (textureA.GetType() == typeof(int))
            {
                return (int)textureA == (int)textureB;
            }

            return false;
        }

        #endregion

        // -------------------------------------------------------------------
        // SPRITES
        // -------------------------------------------------------------------

        #region sprites

        // -------------------------------------------------------------------
        // AddSprite
        // -------------------------------------------------------------------

        public void AddSprite(bool isMouse)
        {
            // Getting coords
            int[] coords = GetCoords(isMouse);
            if (coords == null) return;
            Sprite sprite = new Sprite(SelectedDrawTypeParticular, CurrentPosition, CurrentOrientation);

            // Drawing sprites
            switch (DrawMode)
            {
                case DrawMode.Pencil:
                    if (isMouse)
                    {
                        if (PreviousMouseCoords != null) TraceLine(PreviousMouseCoords, coords, StockSprite, CurrentTexture, sprite);
                    }
                    else if (PreviousCursorCoords != null) TraceLine(PreviousCursorCoords, coords, StockSprite, CurrentTexture, sprite);
                    StockSprite(coords, CurrentTexture, sprite);
                    break;
                case DrawMode.Tin:
                    SystemSounds.Beep.Play();
                    break;
            }

            // Updating previous selected
            if (isMouse) PreviousMouseCoords = coords;
            else PreviousCursorCoords = coords;
        }

        // -------------------------------------------------------------------
        // StockSprite
        // -------------------------------------------------------------------

        public void StockSprite(int[] coords, params object[] args)
        {
            int[] portion = GetPortion(coords[0], coords[3]);
            if (IsInArea(coords) && WANOK.IsInPortions(portion))
            {
                CreateCancel();
                if (Map.Portions[portion] == null) Map.Portions[portion] = new GameMapPortion();
                if (Map.Portions[portion].AddSprite(coords, (int[])args[0], (Sprite)args[1]) && Map.Saved) SetToNoSaved();
                AddPortionToSave(portion);
                AddPortionToUpdate(portion);
                WANOK.AddPortionsToAddCancel(Map.MapInfos.RealMapName, GetGlobalPortion(portion));
            }
        }

        // -------------------------------------------------------------------
        // RemoveSprite
        // -------------------------------------------------------------------

        public void RemoveSprite(bool isMouse)
        {
            // Getting coords
            int[] coords = GetCoords(isMouse, true);
            if (coords == null) return;

            // Removing squares
            switch (DrawMode)
            {
                case DrawMode.Pencil:

                    if (isMouse) if (PreviousMouseCoords != null) TraceLine(PreviousMouseCoords, coords, EraseSprite);
                    else if (PreviousCursorCoords != null) TraceLine(PreviousCursorCoords, coords, EraseSprite);
                    EraseSprite(coords);
                    break;
                case DrawMode.Tin:
                    SystemSounds.Beep.Play();
                    break;
            }

            // Updating previous selected
            if (isMouse) PreviousMouseCoords = coords;
            else PreviousCursorCoords = coords;
        }

        // -------------------------------------------------------------------
        // EraseSprite
        // -------------------------------------------------------------------

        public void EraseSprite(int[] coords, params object[] args)
        {
            int[] portion = GetPortion(coords[0], coords[3]);
            if (IsInArea(coords) && WANOK.IsInPortions(portion))
            {
                CreateCancel();
                if (Map.Portions[portion] == null) Map.Portions[portion] = new GameMapPortion();
                if (Map.Portions[portion].RemoveSprite(coords) && Map.Saved) SetToNoSaved();
                AddPortionToSave(portion);
                AddPortionToUpdate(portion);
                WANOK.AddPortionsToAddCancel(Map.MapInfos.RealMapName, GetGlobalPortion(portion));
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // RELIEF
        // -------------------------------------------------------------------

        #region relief

        // -------------------------------------------------------------------
        // AddMontain
        // -------------------------------------------------------------------

        public void AddMontain(bool isMouse)
        {
            // Getting coords
            int[] coords = GetCoords(isMouse);
            if (coords == null) return;

            Mountain mountain = new Mountain(CurrentMountainHeight[0], CurrentMountainHeight[1], 90);

            // Drawing sprites
            switch (DrawMode)
            {
                case DrawMode.Pencil:
                    if (isMouse)
                    {
                        if (PreviousMouseCoords != null) TraceLine(PreviousMouseCoords, coords, StockMontain, CurrentSpecialItemId, mountain);
                    }
                    else if (PreviousCursorCoords != null) TraceLine(PreviousCursorCoords, coords, StockMontain, CurrentSpecialItemId, mountain);
                    StockMontain(coords, CurrentSpecialItemId, mountain);
                    break;
                case DrawMode.Tin:
                    SystemSounds.Beep.Play();
                    break;
            }

            // Updating previous selected
            if (isMouse) PreviousMouseCoords = coords;
            else PreviousCursorCoords = coords;
        }

        // -------------------------------------------------------------------
        // StockMontain
        // -------------------------------------------------------------------

        public void StockMontain(int[] coords, params object[] args)
        {
            int[] portion = GetPortion(coords[0], coords[3]);
            if (IsInArea(coords) && WANOK.IsInPortions(portion))
            {
                CreateCancel();
                if (Map.Portions[portion] == null) Map.Portions[portion] = new GameMapPortion();
                if (Map.Portions[portion].AddMountain(coords, (int)args[0], (Mountain)args[1]) && Map.Saved) SetToNoSaved();
                AddPortionToSave(portion);
                AddPortionToUpdate(portion);
                WANOK.AddPortionsToAddCancel(Map.MapInfos.RealMapName, GetGlobalPortion(portion));
            }
        }

        // -------------------------------------------------------------------
        // RemoveMountain
        // -------------------------------------------------------------------

        public void RemoveMountain(bool isMouse)
        {
            // Getting coords
            int[] coords = GetCoords(isMouse, true);
            if (coords == null) return;

            // Removing squares
            switch (DrawMode)
            {
                case DrawMode.Pencil:
                    if (isMouse)
                    {
                        if (PreviousMouseCoords != null) TraceLine(PreviousMouseCoords, coords, EraseMountain);
                    }
                    else if (PreviousCursorCoords != null) TraceLine(PreviousCursorCoords, coords, EraseMountain);
                    EraseMountain(coords);
                    break;
                case DrawMode.Tin:
                    SystemSounds.Beep.Play();
                    break;
            }

            // Updating previous selected
            if (isMouse) PreviousMouseCoords = coords;
            else PreviousCursorCoords = coords;
        }

        // -------------------------------------------------------------------
        // EraseMountain
        // -------------------------------------------------------------------

        public void EraseMountain(int[] coords, params object[] args)
        {
            int[] portion = GetPortion(coords[0], coords[3]);
            if (IsInArea(coords) && WANOK.IsInPortions(portion))
            {
                CreateCancel();
                if (Map.Portions[portion] == null) Map.Portions[portion] = new GameMapPortion();
                if (Map.Portions[portion].RemoveMountain(coords) && Map.Saved) SetToNoSaved();
                AddPortionToSave(portion);
                AddPortionToUpdate(portion);
                WANOK.AddPortionsToAddCancel(Map.MapInfos.RealMapName, GetGlobalPortion(portion));
            }
        }

        #endregion

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
            if (PreviousMouseCoords == null)
            {
                return new int[]
                {
                    PointOnPlane[0],
                    GridHeight[0],
                    GridHeight[1],
                    PointOnPlane[3]
                };
            }
            else {
                return new int[]
                {
                    PointOnPlane[0],
                    PreviousMouseCoords[1],
                    PreviousMouseCoords[2],
                    PointOnPlane[3]
                };
            }
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

        public int[] GetCoords(bool isMouse, bool remove = false)
        {
            int[] coords;
            if (isMouse)
            {
                if (SelectedDrawType == "ItemSprite" && remove)
                {
                    if (PointOnSprites != null) coords = PointOnSprites;
                    else return null;
                }
                else if (SelectedDrawType == "ItemRelief" && remove)
                {
                    if (PointOnMountains != null) coords = PointOnMountains;
                    else return null;
                }
                /*
                else if (SelectedDrawType == "ItemRelief")
                {
                    if (PointOnMountains == null || PointOnPlane == null)
                    {
                        if (PointOnMountains != null && PointOnPlane != null) coords = PlaneDistance < MountainDistance ? PointOnPlane : PointOnMountains;
                        else coords = PointOnMountains == null ? PointOnPlane : PointOnMountains;
                    }
                    else return null;
                }*/
                else {
                    if (PreviousMouseCoords == null && PointOnFloor != null && !IsGridOnTop) coords = PointOnFloor;
                    else if (PointOnPlane != null) coords = GetCoordsMouse();
                    else return null;
                }

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
        // GetGlobalPortion
        // -------------------------------------------------------------------

        public int[] GetGlobalPortion(int x, int z)
        {
            return new int[]
            {
                x / WANOK.PORTION_SIZE,
                z / WANOK.PORTION_SIZE
            };
        }

        public int[] GetGlobalPortion(int[] portion)
        {
            return new int[]
            {
                portion[0] + (CursorEditor.GetX() / WANOK.PORTION_SIZE),
                portion[1] + (CursorEditor.GetZ() / WANOK.PORTION_SIZE)
            };
        }

        // -------------------------------------------------------------------
        // GetLocalPortion
        // -------------------------------------------------------------------

        public int[] GetLocalPortion(int[] portion)
        {
            return new int[]
            {
                portion[0] - (CursorEditor.GetX() / WANOK.PORTION_SIZE),
                portion[1] - (CursorEditor.GetZ() / WANOK.PORTION_SIZE)
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
        // DisposeBuffers
        // -------------------------------------------------------------------

        public void DisposeBuffers(int i, int j, bool nullable = true)
        {
            Map.DisposePortionBuffers(new int[] { i, j }, nullable);
            Map.DisposeEventBuffers(new int[] { i, j }, nullable);
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
