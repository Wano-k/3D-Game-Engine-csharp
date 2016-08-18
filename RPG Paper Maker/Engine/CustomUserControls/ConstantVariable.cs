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
    public partial class ConstantVariable : UserControl
    {
        private List<Control[]> SelectionItems = new List<Control[]>();


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public ConstantVariable()
        {
            InitializeComponent();

            SelectionItems.Clear();
            SelectionItems.Add(new Control[] { numericUpDown1 });
            SelectionItems.Add(new Control[] { textBoxVariables1 });

            radioButton1.Checked = true;
        }

        // -------------------------------------------------------------------
        // GetList
        // -------------------------------------------------------------------

        public ListBox GetListBox()
        {
            return textBoxVariables1.GetTextBox();

        }
        // -------------------------------------------------------------------
        // Checks
        // -------------------------------------------------------------------

        public void CheckSelection(int index)
        {
            WANOK.CheckControls(SelectionItems, index);
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CheckSelection(0);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CheckSelection(1);
        }
    }
}
