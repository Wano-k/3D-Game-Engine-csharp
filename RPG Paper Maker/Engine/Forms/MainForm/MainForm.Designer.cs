namespace RPG_Paper_Maker
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ImageListToolBar = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.SplitContainerMain = new System.Windows.Forms.SplitContainer();
            this.SplitContainerTree = new System.Windows.Forms.SplitContainer();
            this.TreeMap = new System.Windows.Forms.TreeView();
            this.ImageListTreeMap = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.ItemFloor = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemFloor1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemFloor2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemStart = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemDrawMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemDrawMode1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemDrawMode2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemDrawMode3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemHeight1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemHeight2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemOpenBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemCloseProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.managementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemInputs = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemTutorials = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemDemo = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButtonNew = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonOpen = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSave = new System.Windows.Forms.ToolBarButton();
            this.separator1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonInput = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonDataBase = new System.Windows.Forms.ToolBarButton();
            this.separator2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPlay = new System.Windows.Forms.ToolBarButton();
            this.ContextMenuMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemSetMap = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMoveMap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemDeleteMap = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuDir = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemNewMap = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemNewDir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemSetDirName = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDeleteDir = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.toolBarButtonSaveAll = new System.Windows.Forms.ToolBarButton();
            this.scrollPanel1 = new RPG_Paper_Maker.ScrollPanel();
            this.TilesetSelector = new RPG_Paper_Maker.TilesetSelector();
            this.MapEditor = new RPG_Paper_Maker.MapEditor();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).BeginInit();
            this.SplitContainerMain.Panel1.SuspendLayout();
            this.SplitContainerMain.Panel2.SuspendLayout();
            this.SplitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerTree)).BeginInit();
            this.SplitContainerTree.Panel1.SuspendLayout();
            this.SplitContainerTree.Panel2.SuspendLayout();
            this.SplitContainerTree.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.ContextMenuMap.SuspendLayout();
            this.ContextMenuDir.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.scrollPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageListToolBar
            // 
            this.ImageListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListToolBar.ImageStream")));
            this.ImageListToolBar.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListToolBar.Images.SetKeyName(0, "new_file.png");
            this.ImageListToolBar.Images.SetKeyName(1, "open_file.png");
            this.ImageListToolBar.Images.SetKeyName(2, "save.png");
            this.ImageListToolBar.Images.SetKeyName(3, "saveAll.png");
            this.ImageListToolBar.Images.SetKeyName(4, "inputs.png");
            this.ImageListToolBar.Images.SetKeyName(5, "datas.png");
            this.ImageListToolBar.Images.SetKeyName(6, "play.png");
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
            this.SplitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerMain.Name = "SplitContainerMain";
            // 
            // SplitContainerMain.Panel1
            // 
            this.SplitContainerMain.Panel1.Controls.Add(this.SplitContainerTree);
            // 
            // SplitContainerMain.Panel2
            // 
            this.SplitContainerMain.Panel2.BackColor = System.Drawing.Color.Black;
            this.SplitContainerMain.Panel2.Controls.Add(this.MapEditor);
            this.SplitContainerMain.Panel2.Controls.Add(this.menuStrip2);
            this.SplitContainerMain.Size = new System.Drawing.Size(890, 443);
            this.SplitContainerMain.SplitterDistance = 177;
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
            // SplitContainerTree.Panel1
            // 
            this.SplitContainerTree.Panel1.Controls.Add(this.scrollPanel1);
            // 
            // SplitContainerTree.Panel2
            // 
            this.SplitContainerTree.Panel2.Controls.Add(this.TreeMap);
            this.SplitContainerTree.Size = new System.Drawing.Size(177, 443);
            this.SplitContainerTree.SplitterDistance = 269;
            this.SplitContainerTree.TabIndex = 0;
            // 
            // TreeMap
            // 
            this.TreeMap.AllowDrop = true;
            this.TreeMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeMap.HideSelection = false;
            this.TreeMap.ImageIndex = 0;
            this.TreeMap.ImageList = this.ImageListTreeMap;
            this.TreeMap.Location = new System.Drawing.Point(0, 0);
            this.TreeMap.Name = "TreeMap";
            this.TreeMap.SelectedImageIndex = 0;
            this.TreeMap.ShowRootLines = false;
            this.TreeMap.Size = new System.Drawing.Size(173, 166);
            this.TreeMap.TabIndex = 0;
            this.TreeMap.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeMap_ItemDrag);
            this.TreeMap.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeMap_AfterSelect);
            this.TreeMap.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeMap_NodeMouseClick);
            this.TreeMap.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeMap_DragDrop);
            this.TreeMap.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeMap_DragEnter);
            // 
            // ImageListTreeMap
            // 
            this.ImageListTreeMap.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListTreeMap.ImageStream")));
            this.ImageListTreeMap.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListTreeMap.Images.SetKeyName(0, "dir.png");
            this.ImageListTreeMap.Images.SetKeyName(1, "map.png");
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemFloor,
            this.ItemStart,
            this.ItemDrawMode,
            this.ItemHeight});
            this.menuStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.menuStrip2.Size = new System.Drawing.Size(705, 20);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            this.menuStrip2.MouseHover += new System.EventHandler(this.menuStrip2_MouseHover);
            // 
            // ItemFloor
            // 
            this.ItemFloor.DoubleClickEnabled = true;
            this.ItemFloor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemFloor1,
            this.ItemFloor2});
            this.ItemFloor.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemFloor.Image = global::RPG_Paper_Maker.Properties.Resources.floor1;
            this.ItemFloor.Name = "ItemFloor";
            this.ItemFloor.Size = new System.Drawing.Size(62, 20);
            this.ItemFloor.Text = "Floor";
            this.ItemFloor.ToolTipText = "Draw square on the floor";
            this.ItemFloor.Click += new System.EventHandler(this.ItemFloor_Click);
            this.ItemFloor.MouseEnter += new System.EventHandler(this.ItemFloor_MouseEnter);
            this.ItemFloor.MouseLeave += new System.EventHandler(this.ItemFloor_MouseLeave);
            // 
            // ItemFloor1
            // 
            this.ItemFloor1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemFloor1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemFloor1.Image = global::RPG_Paper_Maker.Properties.Resources.floor1;
            this.ItemFloor1.Name = "ItemFloor1";
            this.ItemFloor1.Size = new System.Drawing.Size(121, 22);
            this.ItemFloor1.Text = "Floor";
            // 
            // ItemFloor2
            // 
            this.ItemFloor2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemFloor2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemFloor2.Image = global::RPG_Paper_Maker.Properties.Resources.floor2;
            this.ItemFloor2.Name = "ItemFloor2";
            this.ItemFloor2.Size = new System.Drawing.Size(121, 22);
            this.ItemFloor2.Text = "Autotiles";
            // 
            // ItemStart
            // 
            this.ItemStart.Image = global::RPG_Paper_Maker.Properties.Resources.start;
            this.ItemStart.Name = "ItemStart";
            this.ItemStart.Size = new System.Drawing.Size(28, 20);
            this.ItemStart.Click += new System.EventHandler(this.ItemStart_Click);
            // 
            // ItemDrawMode
            // 
            this.ItemDrawMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemDrawMode1,
            this.ItemDrawMode2,
            this.ItemDrawMode3});
            this.ItemDrawMode.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemDrawMode.Image = global::RPG_Paper_Maker.Properties.Resources.pencil;
            this.ItemDrawMode.Name = "ItemDrawMode";
            this.ItemDrawMode.Size = new System.Drawing.Size(28, 20);
            this.ItemDrawMode.ToolTipText = "Drawing tools";
            this.ItemDrawMode.MouseEnter += new System.EventHandler(this.ItemDrawMode_MouseEnter);
            this.ItemDrawMode.MouseLeave += new System.EventHandler(this.ItemDrawMode_MouseLeave);
            // 
            // ItemDrawMode1
            // 
            this.ItemDrawMode1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemDrawMode1.Image = global::RPG_Paper_Maker.Properties.Resources.pencil;
            this.ItemDrawMode1.Name = "ItemDrawMode1";
            this.ItemDrawMode1.Size = new System.Drawing.Size(135, 22);
            this.ItemDrawMode1.Text = "Pencil";
            this.ItemDrawMode1.Click += new System.EventHandler(this.ItemDrawMode1_Click);
            // 
            // ItemDrawMode2
            // 
            this.ItemDrawMode2.ForeColor = System.Drawing.Color.DarkRed;
            this.ItemDrawMode2.Image = global::RPG_Paper_Maker.Properties.Resources.rectangle;
            this.ItemDrawMode2.Name = "ItemDrawMode2";
            this.ItemDrawMode2.Size = new System.Drawing.Size(135, 22);
            this.ItemDrawMode2.Text = "Rectangle";
            this.ItemDrawMode2.Click += new System.EventHandler(this.ItemDrawMode2_Click);
            // 
            // ItemDrawMode3
            // 
            this.ItemDrawMode3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemDrawMode3.Image = global::RPG_Paper_Maker.Properties.Resources.tin;
            this.ItemDrawMode3.Name = "ItemDrawMode3";
            this.ItemDrawMode3.Size = new System.Drawing.Size(135, 22);
            this.ItemDrawMode3.Text = "Tin of paint";
            this.ItemDrawMode3.Click += new System.EventHandler(this.ItemDrawMode3_Click);
            // 
            // ItemHeight
            // 
            this.ItemHeight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemHeight1,
            this.ItemHeight2});
            this.ItemHeight.Image = global::RPG_Paper_Maker.Properties.Resources.height;
            this.ItemHeight.Name = "ItemHeight";
            this.ItemHeight.Size = new System.Drawing.Size(28, 20);
            this.ItemHeight.ToolTipText = "Choose the height of the grid";
            this.ItemHeight.MouseEnter += new System.EventHandler(this.ItemHeight_MouseEnter);
            this.ItemHeight.MouseLeave += new System.EventHandler(this.ItemHeight_MouseLeave);
            // 
            // ItemHeight1
            // 
            this.ItemHeight1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ItemHeight1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemHeight1.Name = "ItemHeight1";
            this.ItemHeight1.Size = new System.Drawing.Size(167, 22);
            this.ItemHeight1.Text = "Square number: 0";
            this.ItemHeight1.MouseEnter += new System.EventHandler(this.ItemHeight1_MouseEnter);
            this.ItemHeight1.MouseLeave += new System.EventHandler(this.ItemHeight1_MouseLeave);
            // 
            // ItemHeight2
            // 
            this.ItemHeight2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ItemHeight2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemHeight2.Name = "ItemHeight2";
            this.ItemHeight2.Size = new System.Drawing.Size(167, 22);
            this.ItemHeight2.Text = "Adding pixels: 0";
            this.ItemHeight2.MouseEnter += new System.EventHandler(this.ItemHeight2_MouseEnter);
            this.ItemHeight2.MouseLeave += new System.EventHandler(this.ItemHeight2_MouseLeave);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.managementToolStripMenuItem,
            this.testToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(890, 23);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemNewProject,
            this.ItemOpenProject,
            this.ItemSave,
            this.ItemSaveAll,
            this.ItemCloseProject,
            this.toolStripSeparator1,
            this.ItemExit});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
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
            // ItemSave
            // 
            this.ItemSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemSave.Image = global::RPG_Paper_Maker.Properties.Resources.save;
            this.ItemSave.Name = "ItemSave";
            this.ItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.ItemSave.Size = new System.Drawing.Size(190, 22);
            this.ItemSave.Text = "Save";
            this.ItemSave.Click += new System.EventHandler(this.ItemSave_Click);
            // 
            // ItemSaveAll
            // 
            this.ItemSaveAll.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemSaveAll.Image = global::RPG_Paper_Maker.Properties.Resources.saveAll;
            this.ItemSaveAll.Name = "ItemSaveAll";
            this.ItemSaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.ItemSaveAll.Size = new System.Drawing.Size(190, 22);
            this.ItemSaveAll.Text = "Save all";
            this.ItemSaveAll.Click += new System.EventHandler(this.ItemSaveAll_Click);
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
            // managementToolStripMenuItem
            // 
            this.managementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemInputs,
            this.ItemDataBase});
            this.managementToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.managementToolStripMenuItem.Name = "managementToolStripMenuItem";
            this.managementToolStripMenuItem.Size = new System.Drawing.Size(90, 19);
            this.managementToolStripMenuItem.Text = "Management";
            // 
            // ItemInputs
            // 
            this.ItemInputs.Enabled = false;
            this.ItemInputs.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemInputs.Image = global::RPG_Paper_Maker.Properties.Resources.inputs;
            this.ItemInputs.Name = "ItemInputs";
            this.ItemInputs.Size = new System.Drawing.Size(157, 22);
            this.ItemInputs.Text = "Inputs manager";
            this.ItemInputs.Click += new System.EventHandler(this.ItemInputs_Click);
            // 
            // ItemDataBase
            // 
            this.ItemDataBase.Enabled = false;
            this.ItemDataBase.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemDataBase.Image = global::RPG_Paper_Maker.Properties.Resources.datas;
            this.ItemDataBase.Name = "ItemDataBase";
            this.ItemDataBase.Size = new System.Drawing.Size(157, 22);
            this.ItemDataBase.Text = "Database";
            this.ItemDataBase.Click += new System.EventHandler(this.ItemDataBase_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemPlay});
            this.testToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 19);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // ItemPlay
            // 
            this.ItemPlay.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ItemPlay.Image = global::RPG_Paper_Maker.Properties.Resources.play;
            this.ItemPlay.Name = "ItemPlay";
            this.ItemPlay.Size = new System.Drawing.Size(96, 22);
            this.ItemPlay.Text = "Play";
            this.ItemPlay.Click += new System.EventHandler(this.ItemPlay_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemTutorials,
            this.ItemDemo,
            this.ItemAbout});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
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
            this.toolBarButtonOpen,
            this.toolBarButtonSave,
            this.toolBarButtonSaveAll,
            this.separator1,
            this.toolBarButtonInput,
            this.toolBarButtonDataBase,
            this.separator2,
            this.toolBarButtonPlay});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.ImageListToolBar;
            this.toolBar1.Location = new System.Drawing.Point(0, 23);
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
            // toolBarButtonSave
            // 
            this.toolBarButtonSave.ImageKey = "save.png";
            this.toolBarButtonSave.Name = "toolBarButtonSave";
            this.toolBarButtonSave.Text = "Save";
            this.toolBarButtonSave.ToolTipText = "Save your progress";
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            this.separator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonInput
            // 
            this.toolBarButtonInput.ImageKey = "inputs.png";
            this.toolBarButtonInput.Name = "toolBarButtonInput";
            this.toolBarButtonInput.Text = "Inputs";
            this.toolBarButtonInput.ToolTipText = "Inputs management";
            // 
            // toolBarButtonDataBase
            // 
            this.toolBarButtonDataBase.ImageKey = "datas.png";
            this.toolBarButtonDataBase.Name = "toolBarButtonDataBase";
            this.toolBarButtonDataBase.Text = "Database";
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            this.separator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonPlay
            // 
            this.toolBarButtonPlay.ImageKey = "play.png";
            this.toolBarButtonPlay.Name = "toolBarButtonPlay";
            this.toolBarButtonPlay.Text = "Play";
            this.toolBarButtonPlay.ToolTipText = "Try your game!";
            // 
            // ContextMenuMap
            // 
            this.ContextMenuMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSetMap,
            this.MenuItemMoveMap,
            this.toolStripSeparator2,
            this.MenuItemDeleteMap});
            this.ContextMenuMap.Name = "ContextMenuMap";
            this.ContextMenuMap.Size = new System.Drawing.Size(141, 76);
            this.ContextMenuMap.Text = "test";
            // 
            // MenuItemSetMap
            // 
            this.MenuItemSetMap.Name = "MenuItemSetMap";
            this.MenuItemSetMap.Size = new System.Drawing.Size(140, 22);
            this.MenuItemSetMap.Text = "Set map...";
            this.MenuItemSetMap.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.MenuItemSetMap.Click += new System.EventHandler(this.MenuItemSetMap_Click);
            // 
            // MenuItemMoveMap
            // 
            this.MenuItemMoveMap.Enabled = false;
            this.MenuItemMoveMap.Name = "MenuItemMoveMap";
            this.MenuItemMoveMap.Size = new System.Drawing.Size(140, 22);
            this.MenuItemMoveMap.Text = "Move map...";
            this.MenuItemMoveMap.Click += new System.EventHandler(this.MenuItemMoveMap_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(137, 6);
            // 
            // MenuItemDeleteMap
            // 
            this.MenuItemDeleteMap.Name = "MenuItemDeleteMap";
            this.MenuItemDeleteMap.Size = new System.Drawing.Size(140, 22);
            this.MenuItemDeleteMap.Text = "Delete map";
            this.MenuItemDeleteMap.Click += new System.EventHandler(this.MenuItemDeleteMap_Click);
            // 
            // ContextMenuDir
            // 
            this.ContextMenuDir.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemNewMap,
            this.MenuItemNewDir,
            this.toolStripSeparator3,
            this.MenuItemSetDirName,
            this.MenuItemDeleteDir});
            this.ContextMenuDir.Name = "ContextMenuDir";
            this.ContextMenuDir.Size = new System.Drawing.Size(183, 98);
            // 
            // MenuItemNewMap
            // 
            this.MenuItemNewMap.Name = "MenuItemNewMap";
            this.MenuItemNewMap.Size = new System.Drawing.Size(182, 22);
            this.MenuItemNewMap.Text = "New map...";
            this.MenuItemNewMap.Click += new System.EventHandler(this.MenuItemNewMap_Click);
            // 
            // MenuItemNewDir
            // 
            this.MenuItemNewDir.Name = "MenuItemNewDir";
            this.MenuItemNewDir.Size = new System.Drawing.Size(182, 22);
            this.MenuItemNewDir.Text = "New directory...";
            this.MenuItemNewDir.Click += new System.EventHandler(this.MenuItemNewDir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(179, 6);
            // 
            // MenuItemSetDirName
            // 
            this.MenuItemSetDirName.Name = "MenuItemSetDirName";
            this.MenuItemSetDirName.Size = new System.Drawing.Size(182, 22);
            this.MenuItemSetDirName.Text = "Set directory name...";
            this.MenuItemSetDirName.Click += new System.EventHandler(this.MenuItemSetDirName_Click);
            // 
            // MenuItemDeleteDir
            // 
            this.MenuItemDeleteDir.Name = "MenuItemDeleteDir";
            this.MenuItemDeleteDir.Size = new System.Drawing.Size(182, 22);
            this.MenuItemDeleteDir.Text = "Delete directory";
            this.MenuItemDeleteDir.Click += new System.EventHandler(this.MenuItemDeleteDir_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.SplitContainerMain);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 65);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(890, 443);
            this.MainPanel.TabIndex = 5;
            // 
            // toolBarButtonSaveAll
            // 
            this.toolBarButtonSaveAll.ImageKey = "saveAll.png";
            this.toolBarButtonSaveAll.Name = "toolBarButtonSaveAll";
            this.toolBarButtonSaveAll.Text = "Save all";
            // 
            // scrollPanel1
            // 
            this.scrollPanel1.AutoScroll = true;
            this.scrollPanel1.Controls.Add(this.TilesetSelector);
            this.scrollPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel1.Location = new System.Drawing.Point(0, 0);
            this.scrollPanel1.Name = "scrollPanel1";
            this.scrollPanel1.Size = new System.Drawing.Size(173, 265);
            this.scrollPanel1.TabIndex = 0;
            this.scrollPanel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollPanel1_Scroll);
            // 
            // TilesetSelector
            // 
            this.TilesetSelector.BackColor = System.Drawing.Color.Black;
            this.TilesetSelector.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TilesetSelector.Location = new System.Drawing.Point(0, 0);
            this.TilesetSelector.Margin = new System.Windows.Forms.Padding(0);
            this.TilesetSelector.Name = "TilesetSelector";
            this.TilesetSelector.Size = new System.Drawing.Size(256, 256);
            this.TilesetSelector.TabIndex = 0;
            this.TilesetSelector.VSync = true;
            this.TilesetSelector.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TilesetSelector_MouseDown);
            this.TilesetSelector.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TilesetSelector_MouseMove);
            this.TilesetSelector.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TilesetSelector_MouseUp);
            // 
            // MapEditor
            // 
            this.MapEditor.BackColor = System.Drawing.Color.Black;
            this.MapEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapEditor.DrawMode = RPG_Paper_Maker.DrawMode.Pencil;
            this.MapEditor.Location = new System.Drawing.Point(0, 20);
            this.MapEditor.Name = "MapEditor";
            this.MapEditor.SelectedDrawType = "ItemFloor";
            this.MapEditor.Size = new System.Drawing.Size(705, 419);
            this.MapEditor.TabIndex = 0;
            this.MapEditor.VSync = false;
            this.MapEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapEditor_MouseDown);
            this.MapEditor.MouseEnter += new System.EventHandler(this.MapEditor_MouseEnter);
            this.MapEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapEditor_MouseMove);
            this.MapEditor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapEditor_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(890, 530);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "RPG Paper Maker 0.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SplitContainerMain.Panel1.ResumeLayout(false);
            this.SplitContainerMain.Panel2.ResumeLayout(false);
            this.SplitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).EndInit();
            this.SplitContainerMain.ResumeLayout(false);
            this.SplitContainerTree.Panel1.ResumeLayout(false);
            this.SplitContainerTree.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerTree)).EndInit();
            this.SplitContainerTree.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ContextMenuMap.ResumeLayout(false);
            this.ContextMenuDir.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.scrollPanel1.ResumeLayout(false);
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
        private MapEditor MapEditor;
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
        private System.Windows.Forms.ImageList ImageListTreeMap;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem ItemFloor;
        private System.Windows.Forms.ToolStripMenuItem ItemFloor1;
        private System.Windows.Forms.ToolStripMenuItem ItemFloor2;
        private TilesetSelector TilesetSelector;
        private System.Windows.Forms.ToolStripMenuItem ItemSave;
        private ScrollPanel scrollPanel1;
        private System.Windows.Forms.ContextMenuStrip ContextMenuMap;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSetMap;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMoveMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDeleteMap;
        private System.Windows.Forms.ContextMenuStrip ContextMenuDir;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNewMap;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNewDir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSetDirName;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDeleteDir;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolBarButton separator1;
        private System.Windows.Forms.ToolStripMenuItem managementToolStripMenuItem;
        private System.Windows.Forms.ToolBarButton toolBarButtonInput;
        private System.Windows.Forms.ToolStripMenuItem ItemInputs;
        private System.Windows.Forms.ToolStripMenuItem ItemDrawMode;
        private System.Windows.Forms.ToolStripMenuItem ItemDrawMode1;
        private System.Windows.Forms.ToolStripMenuItem ItemDrawMode2;
        private System.Windows.Forms.ToolStripMenuItem ItemDrawMode3;
        private System.Windows.Forms.ToolBarButton toolBarButtonSave;
        private System.Windows.Forms.ToolBarButton separator2;
        private System.Windows.Forms.ToolBarButton toolBarButtonPlay;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ItemPlay;
        private System.Windows.Forms.ToolStripMenuItem ItemHeight;
        private System.Windows.Forms.ToolStripMenuItem ItemHeight1;
        private System.Windows.Forms.ToolStripMenuItem ItemHeight2;
        private System.Windows.Forms.ToolStripMenuItem ItemStart;
        private System.Windows.Forms.ToolStripMenuItem ItemDataBase;
        private System.Windows.Forms.ToolBarButton toolBarButtonDataBase;
        private System.Windows.Forms.ToolStripMenuItem ItemSaveAll;
        private System.Windows.Forms.ToolBarButton toolBarButtonSaveAll;
    }
}

