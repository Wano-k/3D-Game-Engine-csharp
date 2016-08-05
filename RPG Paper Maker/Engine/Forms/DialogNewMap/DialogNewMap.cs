using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPG_Paper_Maker.Controls;

namespace RPG_Paper_Maker
{
    public partial class DialogNewMap : Form
    {
        protected DialogNewMapControl Control;
        protected BindingSource ViewModelBindingSource = new BindingSource();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogNewMap(MapInfos mapInfos = null)
        {
            InitializeComponent();

            // Control
            Control = (mapInfos == null) ? new DialogNewMapControl() : new DialogNewMapControl(mapInfos);
            ViewModelBindingSource.DataSource = Control;
            InitializeDataBindings();

            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;
            groupBox2.Paint += MainForm.PaintBorderGroupBox;
            groupBox3.Paint += MainForm.PaintBorderGroupBox;

            // ComboBox
            for (int i = 0; i < WANOK.Game.Tilesets.TilesetsList.Count; i++)
            {
                ComboBoxTileset.Items.Add(WANOK.GetStringComboBox(WANOK.Game.Tilesets.TilesetsList[i].Id, WANOK.Game.Tilesets.TilesetsList[i].Name));
            }
            ComboBoxTileset.SelectedIndex = WANOK.Game.Tilesets.GetTilesetIndexById(Control.Model.Tileset);
            for (int i = 0; i < WANOK.Game.System.Colors.Count; i++)
            {
                ComboBoxColor.Items.Add(WANOK.GetStringComboBox(WANOK.Game.System.Colors[i].Id, WANOK.Game.System.Colors[i].Name));
            }
            ComboBoxColor.SelectedIndex = WANOK.Game.System.GetColorIndexById(Control.Model.SkyColor);
            if (ComboBoxSkyBox.Items.Count > 0) ComboBoxSkyBox.SelectedIndex = 0;

            // Is setting
            if (mapInfos != null)
            {
                Text = "Set map";
            }
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            TextBoxName.DataBindings.Add("Text", ViewModelBindingSource, "MapName", true);
            NumericWidth.DataBindings.Add("Value", ViewModelBindingSource, "Width", true);
            NumericHeight.DataBindings.Add("Value", ViewModelBindingSource, "Height", true);
        }

        // -------------------------------------------------------------------
        // Get
        // -------------------------------------------------------------------

        public MapInfos GetMapInfos()
        {
            return Control.Model;
        }

        public string GetRealMapName()
        {
            return Control.RealMapName;
        }

        public string GetMapName()
        {
            return Control.MapName;
        }

        public int GetWidth()
        {
            return Control.Width;
        }

        public int GetHeight()
        {
            return Control.Height;
        }

        // -------------------------------------------------------------------
        // ComboBoxTileset_SelectedIndexChanged
        // -------------------------------------------------------------------

        private void ComboBoxTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control.SetTileset(ComboBoxTileset.SelectedIndex);
        }

        // -------------------------------------------------------------------
        // ComboBoxColor_SelectedIndexChanged
        // -------------------------------------------------------------------

        private void ComboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control.SetSkyColor(ComboBoxColor.SelectedIndex);
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            Control.CreateMap();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
