using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public class DialogSystemColorControl : INotifyPropertyChanged
    {
        public SystemColor Model;
        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; NotifyPropertyChanged("Name"); }
        }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogSystemColorControl(SystemColor color)
        {
            Model = color;
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
        // SetColor
        // -------------------------------------------------------------------

        public void SetColor(int r, int g, int b)
        {
            Model.Color = new int[] { r, g, b };
        }
    }
}
