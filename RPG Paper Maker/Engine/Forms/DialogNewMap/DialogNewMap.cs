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
            if (ComboBoxTileset.Items.Count > 0) ComboBoxTileset.SelectedItem = ComboBoxTileset.Items[0];
            if (ComboBoxColor.Items.Count > 0) ComboBoxColor.SelectedItem = ComboBoxColor.Items[0];
            if (ComboBoxSkyBox.Items.Count > 0) ComboBoxSkyBox.SelectedItem = ComboBoxSkyBox.Items[0];

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
