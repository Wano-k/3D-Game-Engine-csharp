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
        public Type DialogKind;
        public OptionsKind OptionsKind;
        public SystemGraphic GraphicTileset;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public TextBoxGraphic()
        {
            InitializeComponent();
            listBox1.Items.Add("");
            listBox1.LostFocus += ListBox1_LostFocus;
        }

        // -------------------------------------------------------------------
        // InitializeParameters
        // -------------------------------------------------------------------

        public void InitializeParameters(SystemGraphic graphic, Type type = null, OptionsKind optionsKind = OptionsKind.None, SystemGraphic graphicTileset = null)
        {
            Graphic = graphic;
            OptionsKind = optionsKind;
            GraphicTileset = graphicTileset;
            DialogKind = type == null ? typeof(DialogPreviewGraphic) : type;
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
            DialogPreviewGraphic dialog = (DialogPreviewGraphic)Activator.CreateInstance(DialogKind, Graphic, OptionsKind, GraphicTileset);
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

        private void ListBox1_LostFocus(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
        }
    }
}
