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

        public DialogNewMap(string mapName)
        {
            InitializeComponent();

            // Control
            Control = new DialogNewMapControl(mapName);
            ViewModelBindingSource.DataSource = Control;
            InitializeDataBindings();

            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;
            groupBox2.Paint += MainForm.PaintBorderGroupBox;

            // ComboBox
            if (ComboBoxTileset.Items.Count > 0) ComboBoxTileset.SelectedItem = ComboBoxTileset.Items[0];
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
