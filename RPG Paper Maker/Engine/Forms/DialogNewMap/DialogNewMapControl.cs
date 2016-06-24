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
        public int SkyColor
        {
            get { return Model.SkyColor; }
            set
            {
                Model.SkyColor = value;
                NotifyPropertyChanged("SkyColor");
            }
        }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogNewMapControl()
        {
            Model = new MapInfos(GenerateMapName(), DEFAULT_SIZE, DEFAULT_SIZE);
        }

        public DialogNewMapControl(MapInfos mapInfos)
        {
            Model = mapInfos;
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
        // MapNameExists
        // -------------------------------------------------------------------

        public bool MapNameExists(string mapName)
        {
            string[] dirPaths = Directory.GetDirectories(WANOK.MapsDirectoryPath);
            for (int i = 0; i < dirPaths.Length; i++)
            {
                if (Path.GetFileName(dirPaths[i]) == mapName) return true;
            }
            return false;
        }

        // -------------------------------------------------------------------
        // GenerateMapName
        // -------------------------------------------------------------------

        public string GenerateMapName()
        {
            string mapName = "";
            int nbMaps = Directory.GetDirectories(WANOK.MapsDirectoryPath).Length;
            for (int i = 0; i <= nbMaps; i++)
            {
                mapName = string.Format("MAP{0:D4}", (i + 1));
                if (!MapNameExists(mapName)) break;
            }
            return mapName;
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
                WANOK.SaveBinaryDatas(Model, Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp", "infos.map"));
            }
        }
    }
}
