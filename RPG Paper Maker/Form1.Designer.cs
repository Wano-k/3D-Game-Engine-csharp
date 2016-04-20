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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.openAProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButtonNew = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonOpen = new System.Windows.Forms.ToolBarButton();
            this.mapEditor1 = new RPG_Paper_Maker.MapEditor();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
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
            this.SplitContainerMain.Size = new System.Drawing.Size(890, 174);
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
            this.SplitContainerTree.Size = new System.Drawing.Size(150, 174);
            this.SplitContainerTree.SplitterDistance = 89;
            this.SplitContainerTree.TabIndex = 0;
            // 
            // TreeMap
            // 
            this.TreeMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeMap.Location = new System.Drawing.Point(0, 0);
            this.TreeMap.Name = "TreeMap";
            this.TreeMap.Size = new System.Drawing.Size(146, 77);
            this.TreeMap.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(890, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemNewProject,
            this.openAProjectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // ItemNewProject
            // 
            this.ItemNewProject.Image = global::RPG_Paper_Maker.Properties.Resources.new_file;
            this.ItemNewProject.Name = "ItemNewProject";
            this.ItemNewProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.ItemNewProject.Size = new System.Drawing.Size(190, 22);
            this.ItemNewProject.Text = "New project...";
            this.ItemNewProject.Click += new System.EventHandler(this.ItemNewProject_Click);
            // 
            // openAProjectToolStripMenuItem
            // 
            this.openAProjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseToolStripMenuItem});
            this.openAProjectToolStripMenuItem.Image = global::RPG_Paper_Maker.Properties.Resources.open_file;
            this.openAProjectToolStripMenuItem.Name = "openAProjectToolStripMenuItem";
            this.openAProjectToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.openAProjectToolStripMenuItem.Text = "Open a project";
            // 
            // browseToolStripMenuItem
            // 
            this.browseToolStripMenuItem.Name = "browseToolStripMenuItem";
            this.browseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.browseToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.browseToolStripMenuItem.Text = "Browse...";
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
            // mapEditor1
            // 
            this.mapEditor1.BackColor = System.Drawing.Color.Black;
            this.mapEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapEditor1.Location = new System.Drawing.Point(0, 0);
            this.mapEditor1.Name = "mapEditor1";
            this.mapEditor1.Size = new System.Drawing.Size(732, 170);
            this.mapEditor1.TabIndex = 0;
            this.mapEditor1.VSync = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(890, 262);
            this.Controls.Add(this.SplitContainerMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "RPG Paper Maker 0.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private System.Windows.Forms.ToolStripMenuItem openAProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseToolStripMenuItem;
        private System.Windows.Forms.ToolBarButton toolBarButtonOpen;
        private System.Windows.Forms.ToolBarButton toolBarButtonNew;
    }
}

