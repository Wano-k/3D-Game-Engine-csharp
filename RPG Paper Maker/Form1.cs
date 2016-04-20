using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class Form1 : Form
    {
        public string version = "1.0.3";

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();

            // Creating RPG Paper Maker Games folder
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RPG Paper Maker Games";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            // Updating special infos
            Text = "RPG Paper Maker " + version;
            ShowProjectContain(false);
        }

        // -------------------------------------------------------------------
        // ItemNewProject_Click
        // -------------------------------------------------------------------

        private void ItemNewProject_Click(object sender, EventArgs e)
        {
            DialogNewProject dialog = new DialogNewProject();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ShowProjectContain(true);
            }
            
        }

        // -------------------------------------------------------------------
        // toolBar1_ButtonClick
        // -------------------------------------------------------------------

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            ItemNewProject_Click(sender, e);
        }

        // -------------------------------------------------------------------
        // ShowProjectContain
        // -------------------------------------------------------------------

        private void ShowProjectContain(bool b)
        {
            this.SplitContainerMain.Visible = b;
            this.SplitContainerTree.Visible = b;
            this.TreeMap.Visible = b;
            this.mapEditor1.Visible = b;
        }
    }
}
