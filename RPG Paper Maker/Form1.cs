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
        public string version = "1.0.6";

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();

            // Creating RPG Paper Maker Games folder
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RPG Paper Maker Games";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Getting engine settings
            WANOK.Settings = WANOK.LoadDatas<EngineSettings>(WANOK.PATHSETTINGS);

            // Updating special infos
            this.TitleName = "RPG Paper Maker " + version;
            this.Text = this.TitleName;

            // Contain shown
            EnableNoGame();
            ShowProjectContain(false);
        }

        // -------------------------------------------------------------------
        // Form1_Shown
        // -------------------------------------------------------------------

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (WANOK.Settings.showDemoTip)
            {
                DemoTip();
            }
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
                MessageBox.Show("This is the end of the Demo for now.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                StartDemo();
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
            DemoTip();
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
            this.mapEditor1.Visible = b;
            this.TreeMap.Visible = b;
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

        // -------------------------------------------------------------------
        // DemoTip
        // -------------------------------------------------------------------

        public void DemoTip()
        {
            DialogDemoTip dialog = new DialogDemoTip();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                EnableAll(false);
                this.ItemNewProject.Enabled = true;
                this.toolBarButtonNew.Enabled = true;
                this.fileToolStripMenuItem.Enabled = true;
                StartDemo();
            }
        }

        // -------------------------------------------------------------------
        // EnableAll
        // -------------------------------------------------------------------

        public void EnableAll(bool b)
        {
            this.fileToolStripMenuItem.Enabled = b;
            this.ItemNewProject.Enabled = b;
            this.toolBarButtonNew.Enabled = b;
            this.ItemOpenBrowse.Enabled = b;
            this.toolBarButtonOpen.Enabled = b;
            this.helpToolStripMenuItem.Enabled = b;
            this.ItemTutorials.Enabled = b;
            this.ItemDemo.Enabled = b;
            this.ItemAbout.Enabled = b;
        }

        // -------------------------------------------------------------------
        // EnableNoGame
        // -------------------------------------------------------------------

        public void EnableNoGame()
        {
            EnableAll(false);
            this.fileToolStripMenuItem.Enabled = true;
            this.ItemNewProject.Enabled = true;
            this.toolBarButtonNew.Enabled = true;
            this.ItemOpenBrowse.Enabled = true;
            this.toolBarButtonOpen.Enabled = true;
            this.helpToolStripMenuItem.Enabled = true;
            this.ItemTutorials.Enabled = true;
            this.ItemDemo.Enabled = true;
            this.ItemAbout.Enabled = true;
        }

        // -------------------------------------------------------------------
        // EnableGame
        // -------------------------------------------------------------------

        public void EnableGame()
        {
            EnableNoGame();
        }

        // -------------------------------------------------------------------
        // DEMO STEPS --------------------------------------------------------
        // -------------------------------------------------------------------

        // -------------------------------------------------------------------
        // StartDemo
        // -------------------------------------------------------------------

        public void StartDemo()
        {
            Thread.Sleep(100);
            WANOK.CurrentDemoDialog = new DialogDemoTipNewProject();
            WANOK.CurrentDemoDialog.Location = new Point(this.Location.X + 100, this.Location.Y + 100);
            WANOK.CurrentDemoDialog.Size = new Size(364, 235);
            WANOK.CurrentDemoDialog.Show();
            WANOK.DemoStep = DemoSteps.New;
            this.Select();
        }

        // -------------------------------------------------------------------
        // CancelDemo
        // -------------------------------------------------------------------

        public void CancelDemo()
        {
            if (WANOK.ProjectName == null)
            {
                EnableNoGame();
            }
            else
            {
                EnableGame();
            }
            WANOK.DemoStep = DemoSteps.None;
        }
    }
}
