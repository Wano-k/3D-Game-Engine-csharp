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
        protected DialogDataBaseControl Control;
        protected BindingSource ViewModelBindingSource = new BindingSource();
        public ListBox[] ListBoxesCanceling;
        public ListBox[] ListBoxes;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogDataBase()
        {
            InitializeComponent();
            Control = new DialogDataBaseControl(WANOK.LoadBinaryDatas<SystemDatas>(WANOK.SystemPath));
            ViewModelBindingSource.DataSource = Control;
            ListBoxesCanceling = new ListBox[] { textBoxLangGameName.GetTextBox(), listBoxColors.GetListBox(), textBoxGraphic.GetTextBox() };
            ListBoxes = new ListBox[] { listBoxTilesets.GetListBox() };

            // Tilesets
            listBoxTilesets.InitializeListParameters(ListBoxesCanceling, Control.ModelSystem.Tilesets.Cast<SuperListItem>().ToList(), null, typeof(Tileset), 1, Tileset.MAX_TILESETS);
            listBoxTilesets.GetListBox().SelectedIndexChanged += listBoxTilesets_SelectedIndexChanged;
            textBoxGraphic.GetTextBox().SelectedValueChanged += textBoxGraphic_SelectedValueChanged;
            collisionSettings.LoadTextures();

            // System
            ComboBoxResolution.SelectedIndex = Control.GetFullScreenIndex();
            toolTipSquareSize.SetToolTip(buttonSquareSize, "This option set the maps displaying, it is recommended to put multiple 8 numbers.\nNote that the pixel height addings are not modified.");
            textBoxLangGameName.GetTextBox().Items.Add(Control.GameName[Control.Langs[0]]);
            textBoxLangGameName.AllNames = Control.GameName;
            listBoxColors.InitializeListParameters(ListBoxesCanceling, Control.ModelSystem.Colors.Cast<SuperListItem>().ToList(), typeof(DialogSystemColors), typeof(SystemColor), 1, SystemColor.MAX_COLORS);

            // list event handlers
            for (int i = 0; i < ListBoxesCanceling.Length; i++)
            {
                ListBoxesCanceling[i].MouseClick += listBox_MouseClick;
            }
            for (int i = 0; i < ListBoxes.Length; i++)
            {
                ListBoxes[i].MouseClick += listBox_MouseClick;
            }

            MouseWheel += new MouseEventHandler(form_MouseWheel);
            UnselectAllLists();
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
        // UnselectAllCancelingLists
        // -------------------------------------------------------------------

        public void UnselectAllCancelingLists()
        {
            for (int i = 0; i < ListBoxesCanceling.Length; i++)
            {
                ListBoxesCanceling[i].ClearSelected();
            }
        }

        // -------------------------------------------------------------------
        // UnselectAllLists
        // -------------------------------------------------------------------

        public void UnselectAllLists()
        {
            UnselectAllCancelingLists();
            for (int i = 0; i < ListBoxes.Length; i++)
            {
                ListBoxes[i].SelectedIndex = 0;
            }
        }

        // -------------------------------------------------------------------
        // TILESETS
        // -------------------------------------------------------------------

        #region Tilesets

        // -------------------------------------------------------------------
        // listBoxTilesets_SelectedIndexChanged
        // -------------------------------------------------------------------

        public void listBoxTilesets_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tileset tileset = (Tileset)listBoxTilesets.GetListBox().SelectedItem;
            if (tileset != null)
            {
                textBoxTilesetName.Text = tileset.Name;
                textBoxGraphic.InitializeParameters(tileset.Graphic);
                collisionSettings.InitializeParameters(tileset);
            }
        }

        // -------------------------------------------------------------------
        // textBoxGraphic_SelectedValueChanged
        // -------------------------------------------------------------------

        public void textBoxGraphic_SelectedValueChanged(object sender, EventArgs e)
        {
            collisionSettings.InitializeParameters((Tileset)listBoxTilesets.GetListBox().SelectedItem);
        }

        // -------------------------------------------------------------------
        // textBoxTilesetName_TextChanged
        // -------------------------------------------------------------------

        private void textBoxTilesetName_TextChanged(object sender, EventArgs e)
        {
            listBoxTilesets.SetName(textBoxTilesetName.Text);
        }

        #endregion

        // -------------------------------------------------------------------
        // SYSTEM
        // -------------------------------------------------------------------

        #region System

        // -------------------------------------------------------------------
        // ComboBoxResolution_SelectedIndexChanged
        // -------------------------------------------------------------------

        public void ComboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control.SetFullScreen(ComboBoxResolution.SelectedIndex);
        }

        #endregion

        // -------------------------------------------------------------------
        // listBox_MouseClick
        // -------------------------------------------------------------------

        public void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = ((ListBox)sender).IndexFromPoint(e.X, e.Y); ;
            UnselectAllCancelingLists();
            ((ListBox)sender).SelectedIndex = index;
        }

        // -------------------------------------------------------------------
        // tabControl1_SelectedIndexChanged
        // -------------------------------------------------------------------

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnselectAllLists();
        }

        // -------------------------------------------------------------------
        // form_MouseWheel
        // -------------------------------------------------------------------

        private void form_MouseWheel(object sender, MouseEventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageTilesets)
            {
                listBoxTilesets.GetListBox().Focus();
            }
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Control.ModelSystem.Colors = listBoxColors.ModelList.Cast<SystemColor>().ToList();
            Control.ModelSystem.Tilesets = listBoxTilesets.ModelList.Cast<Tileset>().ToList();
            Control.Save();
            Close();
        }
    }
}
