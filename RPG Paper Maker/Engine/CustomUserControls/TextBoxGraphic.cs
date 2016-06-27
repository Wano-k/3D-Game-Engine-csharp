using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker.Engine.CustomUserControls
{
    public partial class TextBoxGraphic : UserControl
    {
        public SystemGraphic Graphic;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public TextBoxGraphic()
        {
            InitializeComponent();
            listBox1.Items.Add("");
        }

        // -------------------------------------------------------------------
        // InitializeParameters
        // -------------------------------------------------------------------

        public void InitializeParameters(SystemGraphic graphic)
        {
            Graphic = graphic;
            listBox1.Items[0] = graphic.GraphicName;
        }

        // -------------------------------------------------------------------
        // GetTextBox
        // -------------------------------------------------------------------

        public ListBox GetTextBox()
        {
            return listBox1;
        }

        // -------------------------------------------------------------------
        // OpenDialog
        // -------------------------------------------------------------------

        public void OpenDialog()
        {
            listBox1.SelectedIndex = 0;
            DialogPreviewGraphic dialog = new DialogPreviewGraphic(Graphic);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Graphic = dialog.GetGraphic();
                listBox1.Items[0] = Graphic.GraphicName;
            }
        }

        // -------------------------------------------------------------------
        // Button_Click
        // -------------------------------------------------------------------

        private void Button_Click(object sender, EventArgs e)
        {
            OpenDialog();
        }

        // -------------------------------------------------------------------
        // listBox1_DoubleClick
        // -------------------------------------------------------------------

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenDialog();
        }
    }
}
