using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker.Controls
{
    public class DialogNewMapControl : INotifyPropertyChanged
    {
        public const int DEFAULT_SIZE = 25;
        public MapInfos Model;
        public int PreviousWidth, PreviousHeight;
        public string RealMapName
        {
            get { return Model.RealMapName; }
        }
        public string MapName
        {
            get { return Model.MapName; }
            set
            {
                Model.MapName = value;
                NotifyPropertyChanged("MapName");
            }
        }
        public int Width
        {
            get { return Model.Width; }
            set
            {
                Model.Width = value;
                NotifyPropertyChanged("Width");
            }
        }
        public int Height
        {
            get { return Model.Height; }
            set
            {
                Model.Height = value;
                NotifyPropertyChanged("Height");
            }
        }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogNewMapControl()
        {
            Model = new MapInfos(WANOK.GenerateMapName(), DEFAULT_SIZE, DEFAULT_SIZE);
        }

        public DialogNewMapControl(MapInfos mapInfos)
        {
            Model = mapInfos;
            PreviousWidth = Model.Width;
            PreviousHeight = Model.Height;
        }

        // -------------------------------------------------------------------
        // INotifyPropertyChanged
        // -------------------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        // -------------------------------------------------------------------
        // SetTileset
        // -------------------------------------------------------------------

        public void SetTileset(int index)
        {
            Model.Tileset = WANOK.Game.Tilesets.TilesetsList[index].Id;
        }

        // -------------------------------------------------------------------
        // SetSkyColor
        // -------------------------------------------------------------------

        public void SetSkyColor(int index)
        {
            Model.SkyColor = WANOK.Game.System.Colors[index].Id;
        }

        // -------------------------------------------------------------------
        // ResizingMap 120 120 75 20
        // -------------------------------------------------------------------

        public void ResizingMap()
        {
            int difWidth = PreviousWidth - Width, difHeight = PreviousHeight - Height;

            if (difWidth > 0 || difHeight > 0)
            {
                Events events = WANOK.LoadEvents(RealMapName);
                int portionMaxX = (PreviousWidth - 1) / 16, portionMaxY = (PreviousHeight - 1) / 16;
                int newPortionMaxX = (Width - 1) / 16, newPortionMaxY = (Height - 1) / 16;

                for (int i = newPortionMaxX + 1; i <= portionMaxX; i++)
                {
                    for (int j = 0; j <= portionMaxY; j++)
                    {
                        DeleteMapComplete(events, i, j);
                    }
                }
                for (int j = newPortionMaxY + 1; j <= portionMaxY; j++)
                {
                    for (int i = 0; i <= portionMaxX; i++)
                    {
                        DeleteMapComplete(events, i, j);
                    }
                }

                for (int i = 0; i <= newPortionMaxX; i++)
                {
                    DeleteMapItems(i, newPortionMaxY);
                    DeleteMapEvents(events, i, newPortionMaxY);
                }
                for (int j = 0; j <= newPortionMaxY; j++)
                {
                    DeleteMapItems(newPortionMaxX, j);
                    DeleteMapEvents(events, newPortionMaxX, j);
                }

                WANOK.SaveEventsMap(events, RealMapName);
                WANOK.LoadCancel(Model.RealMapName);
            }
        }

        // -------------------------------------------------------------------
        // DeleteMapComplete
        // -------------------------------------------------------------------

        public void DeleteMapComplete(Events events, int i, int j)
        {
            string path = Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp", i + "-" + j + ".pmap");
            int[] portion = new int[] { i, j };
            if (File.Exists(path))
            {
                WANOK.AddPortionsToAddCancel(Model.RealMapName, portion, 0);
                File.Delete(path);
            }
            if (events.CompleteList.ContainsKey(portion))
            {
                WANOK.AddPortionsToAddCancel(Model.RealMapName, portion, 1);
                events.CompleteList.Remove(portion);
            }
        }

        // -------------------------------------------------------------------
        // DeleteMapItems
        // -------------------------------------------------------------------

        public void DeleteMapItems(int i, int j)
        {
            string path = Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp", i + "-" + j + ".pmap");
            if (File.Exists(path))
            {
                GameMapPortion gamePortion = WANOK.LoadBinaryDatas<GameMapPortion>(path);
                WANOK.AddPortionsToAddCancel(Model.RealMapName, new int[] { i, j }, 0);


                // Floors
                List<int[]> coordsFloors = new List<int[]>();
                foreach (int[] coords in gamePortion.Floors.Keys)
                {
                    if (coords[0] >= Width || coords[3] >= Height) coordsFloors.Add(coords);
                }
                for (int k = 0; k < coordsFloors.Count; k++)
                {
                    gamePortion.Floors.Remove(coordsFloors[k]);
                }

                // Autotiles
                Dictionary<int, List<int[]>> coordsAutotiles = new Dictionary<int, List<int[]>>();
                foreach (KeyValuePair<int, Autotiles> entry in gamePortion.Autotiles)
                {
                    coordsAutotiles[entry.Key] = new List<int[]>();
                    foreach (int[] coords in entry.Value.Tiles.Keys)
                    {
                        if (coords[0] >= Width || coords[3] >= Height) coordsAutotiles[entry.Key].Add(coords);
                    }
                }
                foreach (int id in coordsAutotiles.Keys)
                {
                    for (int k = 0; k < coordsAutotiles[id].Count; k++)
                    {
                        gamePortion.Autotiles[id].Tiles.Remove(coordsAutotiles[id][k]);
                    }
                    if (gamePortion.Autotiles[id].IsEmpty()) gamePortion.Autotiles.Remove(id);
                }

                // Sprites
                Dictionary<int[], List<int[]>> coordsSprites = new Dictionary<int[], List<int[]>>(new IntArrayComparer());
                foreach (KeyValuePair<int[], Sprites> entry in gamePortion.Sprites)
                {
                    coordsSprites[entry.Key] = new List<int[]>();
                    foreach (int[] coords in entry.Value.ListSprites.Keys)
                    {
                        if (coords[0] >= Width || coords[3] >= Height) coordsSprites[entry.Key].Add(coords);
                    }
                }
                foreach (int[] texture in coordsSprites.Keys)
                {
                    for (int k = 0; k < coordsSprites[texture].Count; k++)
                    {
                        gamePortion.Sprites[texture].ListSprites.Remove(coordsSprites[texture][k]);
                    }
                    if (gamePortion.Sprites[texture].IsEmpty()) gamePortion.Sprites.Remove(texture);
                }

                // Mountains
                Dictionary<int, Dictionary<int, List<int[]>>> coordsMountains = new Dictionary<int, Dictionary<int, List<int[]>>>();
                foreach (KeyValuePair<int, Mountains> entry in gamePortion.Mountains)
                {
                    coordsMountains[entry.Key] = new Dictionary<int, List<int[]>>();
                    foreach (KeyValuePair<int, MountainsGroup> entry2 in entry.Value.Groups)
                    {
                        coordsMountains[entry.Key][entry2.Key] = new List<int[]>();
                        foreach (int[] coords in entry2.Value.Tiles.Keys)
                        {
                            if (coords[0] >= Width || coords[3] >= Height) coordsMountains[entry.Key][entry2.Key].Add(coords);
                        }
                    }
                }
                foreach (int height in coordsMountains.Keys)
                {
                    foreach (int id in coordsMountains[height].Keys)
                    {
                        for (int k = 0; k < coordsMountains[height][id].Count; k++)
                        {
                            gamePortion.Mountains[height].Groups[id].Tiles.Remove(coordsMountains[height][id][k]);
                        }
                        if (gamePortion.Mountains[height].Groups[id].Tiles.Count == 0) gamePortion.Mountains[height].Groups.Remove(id);
                    }
                    if (gamePortion.Mountains[height].IsEmpty()) gamePortion.Mountains.Remove(height);
                }


                // Saving
                if (gamePortion.IsEmpty()) File.Delete(path);
                else WANOK.SaveBinaryDatas(gamePortion, path);
            }
        }

        // -------------------------------------------------------------------
        // DeleteMapEvents
        // -------------------------------------------------------------------

        public void DeleteMapEvents(Events events, int i, int j)
        {
            int[] portion = new int[] { i, j };
            if (events.CompleteList.ContainsKey(portion))
            {
                Dictionary<int[], SystemEvent> dictionary = events.CompleteList[portion];

                if (dictionary.Count > 0)
                {
                    WANOK.AddPortionsToAddCancel(Model.RealMapName, portion, 1);

                    List<int[]> coordsEvents = new List<int[]>();
                    foreach (int[] coords in dictionary.Keys)
                    {
                        if (coords[0] >= Width || coords[3] >= Height) coordsEvents.Add(coords);
                    }
                    for (int k = 0; k < coordsEvents.Count; k++)
                    {
                        dictionary.Remove(coordsEvents[k]);
                    }
                    if (dictionary.Count == 0) events.CompleteList.Remove(portion);
                }
            }
        }

        // -------------------------------------------------------------------
        // CreateMap
        // -------------------------------------------------------------------

        public void CreateMap()
        {
            if (!Directory.Exists(Path.Combine(WANOK.MapsDirectoryPath, RealMapName)))
            {
                Directory.CreateDirectory(Path.Combine(WANOK.MapsDirectoryPath, RealMapName));
                Directory.CreateDirectory(Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp"));
                WANOK.SaveBinaryDatas(Model, Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "infos.map"));
                WANOK.SaveBinaryDatas(new Events(), Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "events.map"));
            }
            else
            {
                WANOK.CreateCancel(Model.RealMapName, true);
                ResizingMap();
                WANOK.AddPortionsToAddCancel(Model.RealMapName, new int[] { 0 }, 2);
                WANOK.SaveBinaryDatas(Model, Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp", "infos.map"));
                WANOK.LoadCancel(Model.RealMapName);
            }
        }
    }
}
