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
