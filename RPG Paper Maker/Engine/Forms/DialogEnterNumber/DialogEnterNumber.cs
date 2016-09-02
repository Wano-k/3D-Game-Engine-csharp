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
    public partial class DialogEnterNumber : Form
    {
        public int Value;
        public List<SuperListItem> ModelList = null;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogEnterNumber(int value, int min, int max, List<SuperListItem> list = null)
        {
            InitializeComponent();

            ModelList = list == null ? new List<SuperListItem>() : list;
            label1.Text = "Enter a value between " + min + " and " + max + ":";
            Value = value;
            numeric.Minimum = min;
            numeric.Maximum = max;
            numeric.Value = value;

            numeric.Select();
        }

        // -------------------------------------------------------------------
        // IsOkToSupress
        // -------------------------------------------------------------------

        public bool IsOkToSupress()
        {
            for (int i = Value; i < ModelList.Count; i++)
            {
                if (ModelList[i].Id != i + 1) return false;
            }
            return true;
        }

        // -------------------------------------------------------------------
        // numeric_ValueChanged
        // -------------------------------------------------------------------

        private void numeric_ValueChanged(object sender, EventArgs e)
        {
            Value = (int)numeric.Value;
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            bool test = true;
            if (ModelList.Count > Value) test = IsOkToSupress();

            if (test)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Impossible. Your last elements are not ordered well, you can't delete them. Put them in the right order.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
