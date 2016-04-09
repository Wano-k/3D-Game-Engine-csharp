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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.ItemFile = new System.Windows.Forms.MenuItem();
            this.ItemNewProject = new System.Windows.Forms.MenuItem();
            this.ImageListToolBar = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.ToolButtonNew = new System.Windows.Forms.ToolBarButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.SplitContainerMain = new System.Windows.Forms.SplitContainer();
            this.SplitContainerTree = new System.Windows.Forms.SplitContainer();
            this.TreeMap = new System.Windows.Forms.TreeView();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).BeginInit();
            this.SplitContainerMain.Panel1.SuspendLayout();
            this.SplitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerTree)).BeginInit();
            this.SplitContainerTree.Panel2.SuspendLayout();
            this.SplitContainerTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ItemFile});
            // 
            // ItemFile
            // 
            this.ItemFile.Index = 0;
            this.ItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ItemNewProject});
            this.ItemFile.Text = "File";
            // 
            // ItemNewProject
            // 
            this.ItemNewProject.Index = 0;
            this.ItemNewProject.Text = "New project";
            this.ItemNewProject.Click += new System.EventHandler(this.ItemNewProject_Click);
            // 
            // ImageListToolBar
            // 
            this.ImageListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListToolBar.ImageStream")));
            this.ImageListToolBar.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListToolBar.Images.SetKeyName(0, "new_file.png");
            // 
            // toolBar1
            // 
            this.toolBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolButtonNew});
            this.toolBar1.Divider = false;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            this.toolBar1.ImageList = this.ImageListToolBar;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(826, 40);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.Wrappable = false;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // ToolButtonNew
            // 
            this.ToolButtonNew.ImageKey = "new_file.png";
            this.ToolButtonNew.Name = "ToolButtonNew";
            this.ToolButtonNew.Text = "New";
            this.ToolButtonNew.ToolTipText = "Create a new project";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(826, 22);
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
            this.SplitContainerMain.Location = new System.Drawing.Point(0, 40);
            this.SplitContainerMain.Name = "SplitContainerMain";
            // 
            // SplitContainerMain.Panel1
            // 
            this.SplitContainerMain.Panel1.Controls.Add(this.SplitContainerTree);
            this.SplitContainerMain.Size = new System.Drawing.Size(826, 392);
            this.SplitContainerMain.SplitterDistance = 140;
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
            this.SplitContainerTree.Size = new System.Drawing.Size(140, 392);
            this.SplitContainerTree.SplitterDistance = 318;
            this.SplitContainerTree.TabIndex = 0;
            // 
            // TreeMap
            // 
            this.TreeMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeMap.Location = new System.Drawing.Point(0, 0);
            this.TreeMap.Name = "TreeMap";
            this.TreeMap.Size = new System.Drawing.Size(136, 66);
            this.TreeMap.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(826, 454);
            this.Controls.Add(this.SplitContainerMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolBar1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "RPG Paper Maker 0.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SplitContainerMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).EndInit();
            this.SplitContainerMain.ResumeLayout(false);
            this.SplitContainerTree.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerTree)).EndInit();
            this.SplitContainerTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem ItemFile;
        private System.Windows.Forms.MenuItem ItemNewProject;
        private System.Windows.Forms.ToolBarButton ToolButtonNew;
        private System.Windows.Forms.ImageList ImageListToolBar;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer SplitContainerMain;
        private System.Windows.Forms.SplitContainer SplitContainerTree;
        private System.Windows.Forms.TreeView TreeMap;
    }
}

