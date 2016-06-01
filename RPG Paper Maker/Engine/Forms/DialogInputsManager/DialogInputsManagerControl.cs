using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public class DialogInputsManagerControl : INotifyPropertyChanged
    {
        public KeyboardAssign Model;


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
        // Constructor
        // -------------------------------------------------------------------

        public DialogInputsManagerControl(KeyboardAssign model)
        {
            Model = model;
        }

        // -------------------------------------------------------------------
        // SetKeyEditor
        // -------------------------------------------------------------------

        public void SetKeyEditor(string label, Keys k)
        {
            Model.KeyboardEditorAssign[label] = k;
        }

        // -------------------------------------------------------------------
        // SetKeyEditor
        // -------------------------------------------------------------------

        public void SaveDatas()
        {
            WANOK.Settings.KeyboardAssign = Model;
            WANOK.SaveDatas(WANOK.Settings, WANOK.PATHSETTINGS);
        }
    }
}
