using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RPG_Paper_Maker
{
    public class DialogEventControl : INotifyPropertyChanged
    {
        public SystemEvent Model;

        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; NotifyPropertyChanged("Name"); }
        }

        public int CurrentPage
        {
            get { return Model.CurrentPage; }
            set { Model.CurrentPage = value; NotifyPropertyChanged("CurrentPage"); }
        }

        public bool MoveAnimation
        {
            get { return Model.Pages[Model.CurrentPage].Options.MoveAnimation; }
            set { Model.Pages[Model.CurrentPage].Options.MoveAnimation = value; NotifyPropertyChanged("MoveAnimation"); }
        }

        public bool StopAnimation
        {
            get { return Model.Pages[Model.CurrentPage].Options.StopAnimation; }
            set { Model.Pages[Model.CurrentPage].Options.StopAnimation = value; NotifyPropertyChanged("StopAnimation"); }
        }

        public bool DirectionFix
        {
            get { return Model.Pages[Model.CurrentPage].Options.DirectionFix; }
            set { Model.Pages[Model.CurrentPage].Options.DirectionFix = value; NotifyPropertyChanged("DirectionFix"); }
        }

        public bool Through
        {
            get { return Model.Pages[Model.CurrentPage].Options.Through; }
            set { Model.Pages[Model.CurrentPage].Options.Through = value; NotifyPropertyChanged("Through"); }
        }

        public bool SetWithCamera
        {
            get { return Model.Pages[Model.CurrentPage].Options.SetWithCamera; }
            set { Model.Pages[Model.CurrentPage].Options.SetWithCamera = value; NotifyPropertyChanged("SetWithCamera"); }
        }

        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogEventControl(SystemEvent ev)
        {
            Model = ev;
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

    }
}
