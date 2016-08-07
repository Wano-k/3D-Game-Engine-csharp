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

            ToolTip toolTip = new ToolTip();
            toolTip.InitialDelay = 700;
            toolTip.AutoPopDelay = 32000;
            toolTip.IsBalloon = true;
            toolTip.SetToolTip(Button, "The text box on the left only modify name for the main langage.\nThis button allows you to edit for each langage, or double click on the text box to set for all langages..");
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

        // -------------------------------------------------------------------
        // Events
        // -------------------------------------------------------------------

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AllNames[WANOK.CurrentLang] = textBox1.Text;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            WANOK.ShowActionMessage();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            DialogLangSetAll dialog = new DialogLangSetAll(textBox1.Text);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.Content;
                List<string> keys = new List<string>(AllNames.Keys);
                foreach (string lang in keys)
                {
                    AllNames[lang] = dialog.Content;
                }
            }
        }
    }
}
