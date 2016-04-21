﻿using Newtonsoft.Json;
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
        public string version = "1.0.1.0";


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
            WANOK.InitializeKeyBoard();

            // Contain shown
            EnableNoGame();
            ShowProjectContain(false);
            this.menuStrip1.Renderer = new MainRender();
        }

        // -------------------------------------------------------------------
        // MainColorTable
        // -------------------------------------------------------------------

        public class MainColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground
            {
                get { return Color.FromArgb(64, 64, 64); }
            }

            public override Color MenuItemSelected
            {
                get { return Color.CadetBlue; }
            }

            public override Color MenuItemBorder
            {
                get { return Color.Transparent; }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.FromArgb(64, 64, 64); }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(64, 64, 64); }
            }

            public override Color MenuBorder
            {
                get { return Color.LightGray; }
            }
        }

        // -------------------------------------------------------------------
        // MainRender
        // -------------------------------------------------------------------

        private class MainRender : ToolStripProfessionalRenderer
        {
            public MainRender() : base(new MainColorTable()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle r = new Rectangle(Point.Empty, e.Item.Size);
                if (!e.Item.Selected)
                {
                    SolidBrush brush;
                    if (e.Item.Pressed)
                    {
                        brush = new SolidBrush(Color.FromArgb(100, 100, 100));
                    }
                    else
                    {
                        brush = new SolidBrush(Color.FromArgb(64, 64, 64));
                    }
                    e.Graphics.FillRectangle(brush, r);
                }
                else {
                    e.Graphics.FillRectangle(Brushes.CadetBlue, r);
                }
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                Color myColor = Color.FromArgb(64, 64, 64);
                SolidBrush myBrush = new SolidBrush(myColor);
                e.Graphics.FillRectangle(myBrush, rc);
                int height = rc.Y + (rc.Height / 2);
                e.Graphics.DrawLine(new Pen(Color.Silver),rc.X, height, rc.Width, height);
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = Color.White;
                base.OnRenderArrow(e);
            }

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
        // mapEditor1_KeyDown
        // -------------------------------------------------------------------

        private void mapEditor1_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateKeyBoard(e.KeyCode, true);
        }

        // -------------------------------------------------------------------
        // ProcessCmdKey
        // -------------------------------------------------------------------

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down)
            {
                UpdateKeyBoard(keyData, true);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // -------------------------------------------------------------------
        // mapEditor1_KeyUp
        // -------------------------------------------------------------------

        private void mapEditor1_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateKeyBoard(e.KeyCode, false);
        }

        // -------------------------------------------------------------------
        // TreeMap_KeyDown
        // -------------------------------------------------------------------

        private void TreeMap_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateKeyBoard(e.KeyCode, true);
        }

        // -------------------------------------------------------------------
        // TreeMap_KeyUp
        // -------------------------------------------------------------------

        private void TreeMap_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateKeyBoard(e.KeyCode, false);
        }

        // -------------------------------------------------------------------
        // ItemNewProject_Click
        // -------------------------------------------------------------------

        private void ItemNewProject_Click(object sender, EventArgs e)
        {
            DialogNewProject dialog = new DialogNewProject();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OpenProject(dialog.ProjectName, dialog.DirPath);
                if (WANOK.DemoStep != DemoSteps.None)
                {
                    MessageBox.Show("This is the end of the Demo for now.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (WANOK.DemoStep != DemoSteps.None)
                {
                    StartDemo();
                }
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
                OpenProject(dir.Name, dir.FullName);
            }
        }

        // -------------------------------------------------------------------
        // ItemCloseProject_Click
        // -------------------------------------------------------------------

        private void ItemCloseProject_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        // -------------------------------------------------------------------
        // ItemExit_Click
        // -------------------------------------------------------------------

        private void ItemExit_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Are you sure you want to quit RPG Paper Maker?","Quit",MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
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
            this.ItemCloseProject.Enabled = b;
            this.ItemExit.Enabled = b;
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
            this.ItemExit.Enabled = true;
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
            this.ItemCloseProject.Enabled = true;
        }

        // -------------------------------------------------------------------
        // OpenProject
        // -------------------------------------------------------------------

        public void OpenProject(string name, string dir)
        {
            SetTitle(name, dir);
            ShowProjectContain(true);
            EnableGame();
            this.mapEditor1.Select();
        }

        // -------------------------------------------------------------------
        // CloseProject
        // -------------------------------------------------------------------

        public void CloseProject()
        {
            WANOK.ProjectName = null;
            WANOK.CurrentDir = ".";
            this.Text = this.TitleName;
            ShowProjectContain(false);
            EnableNoGame();
        }

        // -------------------------------------------------------------------
        // UpdateKeyBoard
        // -------------------------------------------------------------------

        public void UpdateKeyBoard(Keys k, bool b)
        {
            WANOK.KeyBoardStates[k] = b;
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
            WANOK.CurrentDemoDialog.Size = new Size(364, 209);
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
