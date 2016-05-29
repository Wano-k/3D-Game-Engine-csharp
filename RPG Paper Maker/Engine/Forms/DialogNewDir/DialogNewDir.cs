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
    public partial class DialogNewDir : Form
    {
        public string DirectoryName { get; set; }

        public DialogNewDir(string dirName = null)
        {
            InitializeComponent();

            if (dirName != null)
            {
                Text = "Set directory name";
                TextCtrlDirectory.Text = dirName;
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            DirectoryName = TextCtrlDirectory.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
