using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RPG_Paper_Maker
{
    public class DialogDataBaseControl : INotifyPropertyChanged
    {
        public SystemDatas ModelSystem;
        public Dictionary<string, string> GameName { get { return ModelSystem.GameName; } }
        public List<string> Langs { get { return ModelSystem.Langs; } }
        public int ScreenWidth
        {
            get { return ModelSystem.ScreenWidth; }
            set { ModelSystem.ScreenWidth = value; NotifyPropertyChanged("ScreenWidth"); }
        }
        public int ScreenHeight
        {
            get { return ModelSystem.ScreenHeight; }
            set { ModelSystem.ScreenHeight = value; NotifyPropertyChanged("ScreenHeight"); }
        }
        public bool FullScreen
        {
            get { return ModelSystem.FullScreen; }
            set { ModelSystem.FullScreen = value; NotifyPropertyChanged("FullScreen"); }
        }
        public int SquareSize
        {
            get { return ModelSystem.SquareSize; }
            set { ModelSystem.SquareSize = value; NotifyPropertyChanged("SquareSize"); }
        }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogDataBaseControl(SystemDatas system)
        {
            ModelSystem = system;
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
        // TILESETS
        // -------------------------------------------------------------------

        #region Tilesets

        #endregion

        // -------------------------------------------------------------------
        // SYSTEM
        // -------------------------------------------------------------------

        #region System

        // -------------------------------------------------------------------
        // SetFullScreen
        // -------------------------------------------------------------------

        public void SetFullScreen(int index)
        {
            ModelSystem.FullScreen = index == 1;
        }

        // -------------------------------------------------------------------
        // GetFullScreenIndex
        // -------------------------------------------------------------------

        public int GetFullScreenIndex()
        {
            return ModelSystem.FullScreen ? 1 : 0;
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

        #endregion

        // -------------------------------------------------------------------
        // Save
        // -------------------------------------------------------------------

        public void Save()
        {
            WANOK.SystemDatas = ModelSystem;
            WANOK.SaveBinaryDatas(ModelSystem, WANOK.SystemPath);
        }
    }
}
