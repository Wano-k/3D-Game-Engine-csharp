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
        // InitializeParameters
        // -------------------------------------------------------------------

        public void InitializeParameters(Dictionary<string, string> allNames)
        {
            AllNames = allNames;
            textBox1.Text = allNames[WANOK.CurrentLang];
        }

        // -------------------------------------------------------------------
        // GetTextBox
        // -------------------------------------------------------------------

        public TextBox GetTextBox()
        {
            return textBox1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AllNames[WANOK.CurrentLang] = textBox1.Text;
        }
    }
}
