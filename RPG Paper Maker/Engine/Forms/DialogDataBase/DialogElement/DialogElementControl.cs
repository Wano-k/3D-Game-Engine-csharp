using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public class DialogElementControl : INotifyPropertyChanged
    {
        public SystemElement Model;
        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; NotifyPropertyChanged("Name"); }
        }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogElementControl(SystemElement element)
        {
            Model = element;
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
