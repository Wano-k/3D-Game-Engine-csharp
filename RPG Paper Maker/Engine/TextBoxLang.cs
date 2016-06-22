using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker.Engine
{
    public partial class TextBoxLang : UserControl
    {
        public Dictionary<string, string> AllNames = new Dictionary<string, string>();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public TextBoxLang()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------------------
        // GetTextBox
        // -------------------------------------------------------------------

        public ListBox GetTextBox()
        {
            return listBox1;
        }

        // -------------------------------------------------------------------
        // listBox1_DoubleClick
        // -------------------------------------------------------------------

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            DialogLangSetAll dialog = new DialogLangSetAll((string)listBox1.Items[0]);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items[0] = dialog.Content;
                List<string> keys = new List<string>(AllNames.Keys);
                foreach (string lang in keys)
                {
                    AllNames[lang] = dialog.Content;
                }
            }
        }
    }
}
