using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public class DialogStatisticsControl : INotifyPropertyChanged
    {
        public SystemStatistics Model;
        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; NotifyPropertyChanged("Name"); }
        }
        public int Value
        {
            get { return Model.AllGameOverOptions.Value; }
            set { Model.AllGameOverOptions.Value = value; NotifyPropertyChanged("Value"); }
        }


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogStatisticsControl(SystemStatistics statistics)
        {
            Model = statistics;
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
