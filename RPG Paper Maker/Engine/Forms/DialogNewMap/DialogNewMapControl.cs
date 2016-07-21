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
            Model.Tileset = WANOK.SystemDatas.Tilesets[index].Id;
        }

        // -------------------------------------------------------------------
        // SetSkyColor
        // -------------------------------------------------------------------

        public void SetSkyColor(int index)
        {
            Model.SkyColor = WANOK.SystemDatas.Colors[index].Id;
        }

        // -------------------------------------------------------------------
        // ResizingMap 120 120 75 20
        // -------------------------------------------------------------------

        public void ResizingMap()
        {
            int difWidth = PreviousWidth - Width, difHeight = PreviousHeight - Height;

            if (difWidth > 0 || difHeight > 0)
            {
                int portionMaxX = (PreviousWidth - 1) / 16, portionMaxY = (PreviousHeight - 1) / 16;
                int newPortionMaxX = (Width - 1) / 16, newPortionMaxY = (Height - 1) / 16;

                for (int i = newPortionMaxX + 1; i <= portionMaxX; i++)
                {
                    for (int j = 0; j <= portionMaxY; j++)
                    {
                        string path = Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp", i + "-" + j + ".pmap");
                        if (File.Exists(path)) File.Delete(path);
                    }
                }
                for (int j = newPortionMaxY + 1; j <= portionMaxY; j++)
                {
                    for (int i = 0; i <= portionMaxX; i++)
                    {
                        string path = Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp", i + "-" + j + ".pmap");
                        if (File.Exists(path)) File.Delete(path);
                    }
                }
                for (int i = 0; i <= newPortionMaxX; i++)
                {
                    DeleteMapItems(i, newPortionMaxY);
                }
                for (int j = 0; j <= newPortionMaxY; j++)
                {
                    DeleteMapItems(newPortionMaxX, j);
                }
            }

            WANOK.CreateCancelMap(RealMapName);
        }

        // -------------------------------------------------------------------
        // DeleteMapItems
        // -------------------------------------------------------------------

        public void DeleteMapItems(int i, int j)
        {
            string path = Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp", i + "-" + j + ".pmap");
            if (File.Exists(path))
            {
                // Loading
                GameMapPortion gamePortion = WANOK.LoadBinaryDatas<GameMapPortion>(path);

                // Floors
                List<int[]> coordsFloors = new List<int[]>();
                foreach (int[] coords in gamePortion.Floors.Keys)
                {
                    coordsFloors.Add(coords);
                }
                for (int k = 0; k < coordsFloors.Count; k++)
                {
                    if (coordsFloors[k][0] >= Width || coordsFloors[k][3] >= Height) gamePortion.Floors.Remove(coordsFloors[k]);
                }

                // Autotiles
                Dictionary<int, List<int[]>> coordsAutotiles = new Dictionary<int, List<int[]>>();
                foreach (KeyValuePair<int, Autotiles> entry in gamePortion.Autotiles)
                {
                    coordsAutotiles[entry.Key] = new List<int[]>();
                    foreach (int[] coords in entry.Value.Tiles.Keys)
                    {
                        coordsAutotiles[entry.Key].Add(coords);
                    }
                }
                foreach (int id in coordsAutotiles.Keys)
                {
                    for (int k = 0; k < coordsAutotiles[id].Count; k++)
                    {
                        if (coordsAutotiles[id][k][0] >= Width || coordsAutotiles[id][k][3] >= Height) gamePortion.Autotiles[id].Tiles.Remove(coordsAutotiles[id][k]);
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
                        coordsSprites[entry.Key].Add(coords);
                    }
                }
                foreach (int[] texture in coordsSprites.Keys)
                {
                    for (int k = 0; k < coordsSprites[texture].Count; k++)
                    {
                        if (coordsSprites[texture][k][0] >= Width || coordsSprites[texture][k][3] >= Height) gamePortion.Sprites[texture].ListSprites.Remove(coordsSprites[texture][k]);
                    }
                    if (gamePortion.Sprites[texture].IsEmpty()) gamePortion.Sprites.Remove(texture);
                }

                // Saving
                if (gamePortion.IsEmpty()) File.Delete(path);
                else WANOK.SaveBinaryDatas(gamePortion, path);
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
            }
            else
            {
                ResizingMap();
                WANOK.SaveBinaryDatas(Model, Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp", "infos.map"));
            }
        }
    }
}
