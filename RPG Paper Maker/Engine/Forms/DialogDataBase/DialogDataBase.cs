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

            // System
            ControlSystem = new DialogDataBaseSystemControl(WANOK.LoadBinaryDatas<SystemDatas>(WANOK.SystemPath));
            ViewModelBindingSource.DataSource = ControlSystem;
            ComboBoxResolution.SelectedIndex = ControlSystem.GetFullScreenIndex();
            toolTipSquareSize.SetToolTip(buttonSquareSize, "This option set the maps displaying, it is recommended to put multiple 8 numbers.\nNote that the pixel height addings are not modified.");
            textBoxLangGameName.GetTextBox().Items.Add(ControlSystem.GameName[ControlSystem.Langs[0]]);
            textBoxLangGameName.AllNames = ControlSystem.GameName;


            InitializeDataBindings();
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            numericWidth.DataBindings.Add("Value", ViewModelBindingSource, "ScreenWidth", true);
            numericHeight.DataBindings.Add("Value", ViewModelBindingSource, "ScreenHeight", true);
        }

        // -------------------------------------------------------------------
        // ComboBoxResolution_SelectedIndexChanged
        // -------------------------------------------------------------------

        private void ComboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlSystem.SetFullScreen(ComboBoxResolution.SelectedIndex);
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
