using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RPG_Paper_Maker
{
    public class DialogConditionControl : INotifyPropertyChanged
    {
        public Condition Model;

        

        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public DialogConditionControl(object[] obj)
        {
            
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
        // DefaultObjects
        // -------------------------------------------------------------------

        public static object[] DefaultObjects()
        {
            return new object[] { };
        }

        // -------------------------------------------------------------------
        // ToString
        // -------------------------------------------------------------------

        public static string ToString(object[] obj)
        {
            return "";
        }
    }
}
