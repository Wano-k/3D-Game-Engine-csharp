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
        public GameDatas Model = new GameDatas();
        public Dictionary<string, string> GameName { get { return Model.System.GameName; } }
        public List<string> Langs { get { return Model.System.Langs; } }
        public int ScreenWidth
        {
            get { return Model.System.ScreenWidth; }
            set { Model.System.ScreenWidth = value; NotifyPropertyChanged("ScreenWidth"); }
        }
        public int ScreenHeight
        {
            get { return Model.System.ScreenHeight; }
            set { Model.System.ScreenHeight = value; NotifyPropertyChanged("ScreenHeight"); }
        }
        public bool FullScreen
        {
            get { return Model.System.FullScreen; }
            set { Model.System.FullScreen = value; NotifyPropertyChanged("FullScreen"); }
        }
        public int SquareSize
        {
            get { return Model.System.SquareSize; }
            set { Model.System.SquareSize = value; NotifyPropertyChanged("SquareSize"); }
        }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogDataBaseControl()
        {
            Model.LoadDatas();
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
            Model.System.FullScreen = index == 1;
        }

        // -------------------------------------------------------------------
        // GetFullScreenIndex
        // -------------------------------------------------------------------

        public int GetFullScreenIndex()
        {
            return Model.System.FullScreen ? 1 : 0;
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
            WANOK.Game = Model;
            Model.SaveDatas();
        }
    }
}
