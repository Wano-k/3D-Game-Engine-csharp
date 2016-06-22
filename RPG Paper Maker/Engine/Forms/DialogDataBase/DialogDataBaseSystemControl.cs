using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RPG_Paper_Maker
{
    public class DialogDataBaseSystemControl : INotifyPropertyChanged
    {
        public SystemDatas Model;
        public Dictionary<string, string> GameName { get { return Model.GameName; } }
        public List<string> Langs { get { return Model.Langs; } }
        public int ScreenWidth
        {
            get { return Model.ScreenWidth; }
            set { Model.ScreenWidth = value; NotifyPropertyChanged("ScreenWidth"); }
        }
        public int ScreenHeight
        {
            get { return Model.ScreenHeight; }
            set { Model.ScreenHeight = value; NotifyPropertyChanged("ScreenHeight"); }
        }
        public bool FullScreen
        {
            get { return Model.FullScreen; }
            set { Model.FullScreen = value; NotifyPropertyChanged("FullScreen"); }
        }

        
        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogDataBaseSystemControl(SystemDatas system)
        {
            Model = system;
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
        // SetFullScreen
        // -------------------------------------------------------------------

        public void SetFullScreen(int index)
        {
            Model.FullScreen = index == 1;
        }

        // -------------------------------------------------------------------
        // GetFullScreenIndex
        // -------------------------------------------------------------------

        public int GetFullScreenIndex()
        {
            return Model.FullScreen ? 1 : 0;
        }

        // -------------------------------------------------------------------
        // SetGameName
        // -------------------------------------------------------------------

        public void SetGameName(string name)
        {
            foreach(string lang in GameName.Keys)
            {
                GameName[lang] = name;
            }
        }

        // -------------------------------------------------------------------
        // Save
        // -------------------------------------------------------------------

        public void Save()
        {
            WANOK.SaveBinaryDatas(Model, WANOK.SystemPath);
        }
    }
}
