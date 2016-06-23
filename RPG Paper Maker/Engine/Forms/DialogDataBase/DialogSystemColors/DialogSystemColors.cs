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
    public partial class DialogSystemColors : SuperListDialog
    {
        protected DialogSystemColorControl Control;
        protected BindingSource ViewModelBindingSource = new BindingSource();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogSystemColors(SystemColor color = null)
        {
            InitializeComponent();
            if (color == null)
            {
                color = new SystemColor("New color", new int[] { 0, 0, 0 });
                Text = "New color";
            }
            Control = new DialogSystemColorControl(color);
            ViewModelBindingSource.DataSource = Control;
            panelColor.BackColor = color.GetWinformsColor();
            textBoxName.Select();

            InitializeDataBindings();
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            textBoxName.DataBindings.Add("Text", ViewModelBindingSource, "Name", true);
        }

        // -------------------------------------------------------------------
        // GetObject
        // -------------------------------------------------------------------

        public override SuperListItem GetObject()
        {
            return Control.Model;
        }

        // -------------------------------------------------------------------
        // panelColor_DoubleClick
        // -------------------------------------------------------------------

        private void panelColor_DoubleClick(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Control.SetColor(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                panelColor.BackColor = colorDialog.Color;
            }
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
