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


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogNewMapControl(string mapName)
        {
            Model = new MapInfos(mapName, DEFAULT_SIZE, DEFAULT_SIZE);
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
        // CreateMap
        // -------------------------------------------------------------------

        public void CreateMap()
        {
            Directory.CreateDirectory(Path.Combine(WANOK.MapsDirectoryPath, RealMapName));
            Directory.CreateDirectory(Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "temp"));
            WANOK.SaveDatas(Model, Path.Combine(WANOK.MapsDirectoryPath, RealMapName, "infos.map"));
        }
    }
}
