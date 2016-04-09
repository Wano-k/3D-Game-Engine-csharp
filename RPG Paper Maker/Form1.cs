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
    public partial class Form1 : Form
    {
        public string version = "1.0.0";

        public Form1()
        {
            InitializeComponent();

            Text = "RPG Paper Maker " + version;
            ShowProjectContain(false);
        }

        private void ItemNewProject_Click(object sender, EventArgs e)
        {
            DialogNewProject dialog = new DialogNewProject();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ShowProjectContain(true);
            }
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            ItemNewProject_Click(sender, e);
        }

        private void ShowProjectContain(bool b)
        {
            this.SplitContainerMain.Visible = b;
            this.SplitContainerTree.Visible = b;
            this.TreeMap.Visible = b;
            this.mapEditor1.Visible = b;
        }
    }
}
