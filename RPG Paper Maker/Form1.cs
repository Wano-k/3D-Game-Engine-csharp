using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class Form1 : Form
    {
        public string TitleName = "RPG Paper Maker";
        public string version = "1.0.5";

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
            this.TitleName = "RPG Paper Maker " + version;
            this.Text = this.TitleName;
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
                SetTitle(dialog.ProjectName, dialog.DirPath);
                ShowProjectContain(true);
            }
        }

        // -------------------------------------------------------------------
        // ItemOpenBrowse_Click
        // -------------------------------------------------------------------

        private void ItemOpenBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "RPG Paper Maker Files|*.rpm;";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                DirectoryInfo dir = Directory.GetParent(file);
                SetTitle(dir.Name, dir.FullName);

                ShowProjectContain(true);
            }
        }

        // -------------------------------------------------------------------
        // toolBar1_ButtonClick
        // -------------------------------------------------------------------

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Name.Equals("toolBarButtonNew"))
            {
                ItemNewProject_Click(sender, e);
            }
            else if (e.Button.Name.Equals("toolBarButtonOpen"))
            {
                ItemOpenBrowse_Click(sender, e);
            }
        }

        // -------------------------------------------------------------------
        // ItemTutorials_Click
        // -------------------------------------------------------------------

        private void ItemTutorials_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://rpgpapermaker.com/index.php/tutorials");
            Process.Start(sInfo);
        }

        // -------------------------------------------------------------------
        // ItemDemo_Click
        // -------------------------------------------------------------------

        private void ItemDemo_Click(object sender, EventArgs e)
        {
            DialogDemoTip dialog = new DialogDemoTip();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        // -------------------------------------------------------------------
        // ItemAbout_Click
        // -------------------------------------------------------------------

        private void ItemAbout_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.Text = "About RPG Paper Maker";
            box.ShowDialog();
        }

        // -------------------------------------------------------------------
        // ShowProjectContain
        // -------------------------------------------------------------------

        public void ShowProjectContain(bool b)
        {
            this.SplitContainerMain.Visible = b;
            this.SplitContainerTree.Visible = b;
            this.TreeMap.Visible = b;
            this.mapEditor1.Visible = b;
        }

        // -------------------------------------------------------------------
        // SetTitle
        // -------------------------------------------------------------------

        public void SetTitle(string name, string dir)
        {
            WANOK.ProjectName = name;
            WANOK.CurrentDir = dir;
            this.Text = this.TitleName + " - " + name;
        }
    }
}
