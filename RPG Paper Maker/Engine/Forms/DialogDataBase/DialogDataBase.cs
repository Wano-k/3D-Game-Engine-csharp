using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class DialogDataBase : Form
    {
        protected DialogDataBaseSystemControl ControlSystem;
        protected BindingSource ViewModelBindingSource = new BindingSource();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogDataBase()
        {
            InitializeComponent();

            // Control
            ControlSystem = new DialogDataBaseSystemControl(WANOK.LoadBinaryDatas<SystemDatas>(WANOK.SystemPath));
            ViewModelBindingSource.DataSource = ControlSystem;
            InitializeDataBindings();

            // Other settings
            ComboBoxResolution.SelectedItem = ComboBoxResolution.Items[0];
            toolTipSquareSize.SetToolTip(buttonSquareSize, "This option set the maps displaying, it is recommended to put multiple 8 numbers.\nNote that the pixel height addings are not modified.");

            // Lang
            textBoxLangGameName.GetTextBox().Items.Add(ControlSystem.GameName[ControlSystem.Langs[0]]);
            textBoxLangGameName.AllNames = ControlSystem.GameName;
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            /*
            TextBoxName.DataBindings.Add("Text", ViewModelBindingSource, "MapName", true);
            NumericWidth.DataBindings.Add("Value", ViewModelBindingSource, "Width", true);
            NumericHeight.DataBindings.Add("Value", ViewModelBindingSource, "Height", true);*/
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            ControlSystem.Save();
            Close();
        }
    }
}
