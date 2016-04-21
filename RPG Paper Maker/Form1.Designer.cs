namespace RPG_Paper_Maker
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ImageListToolBar = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.SplitContainerMain = new System.Windows.Forms.SplitContainer();
            this.SplitContainerTree = new System.Windows.Forms.SplitContainer();
            this.TreeMap = new System.Windows.Forms.TreeView();
            this.mapEditor1 = new RPG_Paper_Maker.MapEditor();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemOpenBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemCloseProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemTutorials = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemDemo = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButtonNew = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonOpen = new System.Windows.Forms.ToolBarButton();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).BeginInit();
            this.SplitContainerMain.Panel1.SuspendLayout();
            this.SplitContainerMain.Panel2.SuspendLayout();
            this.SplitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerTree)).BeginInit();
            this.SplitContainerTree.Panel2.SuspendLayout();
            this.SplitContainerTree.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageListToolBar
            // 
            this.ImageListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListToolBar.ImageStream")));
            this.ImageListToolBar.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListToolBar.Images.SetKeyName(0, "new_file.png");
            this.ImageListToolBar.Images.SetKeyName(1, "open_file.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 508);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(890, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // SplitContainerMain
            // 
            this.SplitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerMain.Location = new System.Drawing.Point(0, 66);
            this.SplitContainerMain.Name = "SplitContainerMain";
            // 
            // SplitContainerMain.Panel1
            // 
            this.SplitContainerMain.Panel1.Controls.Add(this.SplitContainerTree);
            // 
            // SplitContainerMain.Panel2
            // 
            this.SplitContainerMain.Panel2.Controls.Add(this.mapEditor1);
            this.SplitContainerMain.Size = new System.Drawing.Size(890, 442);
            this.SplitContainerMain.SplitterDistance = 150;
            this.SplitContainerMain.TabIndex = 2;
            // 
            // SplitContainerTree
            // 
            this.SplitContainerTree.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SplitContainerTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerTree.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerTree.Name = "SplitContainerTree";
            this.SplitContainerTree.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainerTree.Panel2
            // 
            this.SplitContainerTree.Panel2.Controls.Add(this.TreeMap);
            this.SplitContainerTree.Size = new System.Drawing.Size(150, 442);
            this.SplitContainerTree.SplitterDistance = 275;
            this.SplitContainerTree.TabIndex = 0;
            // 
            // TreeMap
            // 
            this.TreeMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeMap.Location = new System.Drawing.Point(0, 0);
            this.TreeMap.Name = "TreeMap";
            this.TreeMap.Size = new System.Drawing.Size(146, 159);
            this.TreeMap.TabIndex = 0;
            this.TreeMap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeMap_KeyDown);
            this.TreeMap.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TreeMap_KeyUp);
            // 
            // mapEditor1
            // 
            this.mapEditor1.BackColor = System.Drawing.Color.Black;
            this.mapEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapEditor1.Location = new System.Drawing.Point(0, 0);
            this.mapEditor1.Name = "mapEditor1";
            this.mapEditor1.Size = new System.Drawing.Size(732, 438);
            this.mapEditor1.TabIndex = 0;
            this.mapEditor1.VSync = false;
            this.mapEditor1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mapEditor1_KeyDown);
            this.mapEditor1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mapEditor1_KeyUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(890, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemNewProject,
            this.ItemOpenProject,
            this.ItemCloseProject,
            this.toolStripSeparator1,
            this.ItemExit});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // ItemNewProject
            // 
            this.ItemNewProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemNewProject.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemNewProject.Image = global::RPG_Paper_Maker.Properties.Resources.new_file;
            this.ItemNewProject.Name = "ItemNewProject";
            this.ItemNewProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.ItemNewProject.Size = new System.Drawing.Size(190, 22);
            this.ItemNewProject.Text = "New project...";
            this.ItemNewProject.Click += new System.EventHandler(this.ItemNewProject_Click);
            // 
            // ItemOpenProject
            // 
            this.ItemOpenProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemOpenProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemOpenBrowse});
            this.ItemOpenProject.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemOpenProject.Image = global::RPG_Paper_Maker.Properties.Resources.open_file;
            this.ItemOpenProject.Name = "ItemOpenProject";
            this.ItemOpenProject.Size = new System.Drawing.Size(190, 22);
            this.ItemOpenProject.Text = "Open a project";
            // 
            // ItemOpenBrowse
            // 
            this.ItemOpenBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemOpenBrowse.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemOpenBrowse.Name = "ItemOpenBrowse";
            this.ItemOpenBrowse.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.ItemOpenBrowse.Size = new System.Drawing.Size(164, 22);
            this.ItemOpenBrowse.Text = "Browse...";
            this.ItemOpenBrowse.Click += new System.EventHandler(this.ItemOpenBrowse_Click);
            // 
            // ItemCloseProject
            // 
            this.ItemCloseProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemCloseProject.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemCloseProject.Name = "ItemCloseProject";
            this.ItemCloseProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ItemCloseProject.Size = new System.Drawing.Size(190, 22);
            this.ItemCloseProject.Text = "Close project";
            this.ItemCloseProject.Click += new System.EventHandler(this.ItemCloseProject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // ItemExit
            // 
            this.ItemExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemExit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemExit.Name = "ItemExit";
            this.ItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.ItemExit.Size = new System.Drawing.Size(190, 22);
            this.ItemExit.Text = "Quit";
            this.ItemExit.Click += new System.EventHandler(this.ItemExit_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemTutorials,
            this.ItemDemo,
            this.ItemAbout});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // ItemTutorials
            // 
            this.ItemTutorials.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemTutorials.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemTutorials.Name = "ItemTutorials";
            this.ItemTutorials.Size = new System.Drawing.Size(120, 22);
            this.ItemTutorials.Text = "Tutorials";
            this.ItemTutorials.Click += new System.EventHandler(this.ItemTutorials_Click);
            // 
            // ItemDemo
            // 
            this.ItemDemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemDemo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemDemo.Name = "ItemDemo";
            this.ItemDemo.Size = new System.Drawing.Size(120, 22);
            this.ItemDemo.Text = "Demo!";
            this.ItemDemo.Click += new System.EventHandler(this.ItemDemo_Click);
            // 
            // ItemAbout
            // 
            this.ItemAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemAbout.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemAbout.Name = "ItemAbout";
            this.ItemAbout.Size = new System.Drawing.Size(120, 22);
            this.ItemAbout.Text = "About...";
            this.ItemAbout.Click += new System.EventHandler(this.ItemAbout_Click);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonNew,
            this.toolBarButtonOpen});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.ImageListToolBar;
            this.toolBar1.Location = new System.Drawing.Point(0, 24);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(890, 42);
            this.toolBar1.TabIndex = 4;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButtonNew
            // 
            this.toolBarButtonNew.ImageKey = "new_file.png";
            this.toolBarButtonNew.Name = "toolBarButtonNew";
            this.toolBarButtonNew.Text = "New";
            this.toolBarButtonNew.ToolTipText = "Create a new project";
            // 
            // toolBarButtonOpen
            // 
            this.toolBarButtonOpen.ImageKey = "open_file.png";
            this.toolBarButtonOpen.Name = "toolBarButtonOpen";
            this.toolBarButtonOpen.Text = "Open";
            this.toolBarButtonOpen.ToolTipText = "Open a project";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(890, 530);
            this.Controls.Add(this.SplitContainerMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "RPG Paper Maker 0.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SplitContainerMain.Panel1.ResumeLayout(false);
            this.SplitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).EndInit();
            this.SplitContainerMain.ResumeLayout(false);
            this.SplitContainerTree.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerTree)).EndInit();
            this.SplitContainerTree.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList ImageListToolBar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer SplitContainerMain;
        private System.Windows.Forms.SplitContainer SplitContainerTree;
        private System.Windows.Forms.TreeView TreeMap;
        private MapEditor mapEditor1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolStripMenuItem ItemNewProject;
        private System.Windows.Forms.ToolStripMenuItem ItemOpenProject;
        private System.Windows.Forms.ToolStripMenuItem ItemOpenBrowse;
        private System.Windows.Forms.ToolBarButton toolBarButtonOpen;
        private System.Windows.Forms.ToolBarButton toolBarButtonNew;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ItemTutorials;
        private System.Windows.Forms.ToolStripMenuItem ItemAbout;
        private System.Windows.Forms.ToolStripMenuItem ItemDemo;
        private System.Windows.Forms.ToolStripMenuItem ItemCloseProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ItemExit;
    }
}

