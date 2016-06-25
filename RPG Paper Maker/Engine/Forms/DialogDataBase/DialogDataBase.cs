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
        public ListBox[] ListBoxes;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogDataBase()
        {
            InitializeComponent();
            ListBoxes = new ListBox[] { textBoxLangGameName.GetTextBox(), listBoxColors.GetListBox() };

            // System
            ControlSystem = new DialogDataBaseSystemControl(WANOK.LoadBinaryDatas<SystemDatas>(WANOK.SystemPath));
            ViewModelBindingSource.DataSource = ControlSystem;
            ComboBoxResolution.SelectedIndex = ControlSystem.GetFullScreenIndex();
            toolTipSquareSize.SetToolTip(buttonSquareSize, "This option set the maps displaying, it is recommended to put multiple 8 numbers.\nNote that the pixel height addings are not modified.");
            textBoxLangGameName.GetTextBox().Items.Add(ControlSystem.GameName[ControlSystem.Langs[0]]);
            textBoxLangGameName.AllNames = ControlSystem.GameName;
            listBoxColors.InitializeListParameters(ListBoxes, ControlSystem.Model.Colors.Cast<SuperListItem>().ToList(), typeof(DialogSystemColors), typeof(SystemColor), 1, WANOK.MAX_COLORS);

            // Tilesets

            // list event handlers
            textBoxLangGameName.GetTextBox().Click += listBox_Click;
            listBoxColors.GetListBox().Click += listBox_Click;

            InitializeDataBindings();
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            numericWidth.DataBindings.Add("Value", ViewModelBindingSource, "ScreenWidth", true);
            numericHeight.DataBindings.Add("Value", ViewModelBindingSource, "ScreenHeight", true);
            numericSquareSize.DataBindings.Add("Value", ViewModelBindingSource, "SquareSize", true);
        }

        // -------------------------------------------------------------------
        // UnselectAllLists
        // -------------------------------------------------------------------

        public void UnselectAllLists()
        {
            for (int i = 0; i < ListBoxes.Length; i++)
            {
                ListBoxes[i].ClearSelected();
            }
        }

        // -------------------------------------------------------------------
        // ComboBoxResolution_SelectedIndexChanged
        // -------------------------------------------------------------------

        public void ComboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlSystem.SetFullScreen(ComboBoxResolution.SelectedIndex);
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            ControlSystem.Model.Colors = listBoxColors.ModelList.Cast<SystemColor>().ToList();
            ControlSystem.Save();
            Close();
        }

        // -------------------------------------------------------------------
        // listBox_Click
        // -------------------------------------------------------------------

        private void listBox_Click(object sender, EventArgs e)
        {
            int index = ((ListBox)sender).SelectedIndex;
            UnselectAllLists();
            ((ListBox)sender).SelectedIndex = index;
        }
    }
}
