using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RPG_Paper_Maker
{
    public partial class MainForm : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        public InterpolationPictureBox PictureBoxSpecialTileset = new InterpolationPictureBox();
        public ImageComboBox ComboBoxSpecialTileset1 = new ImageComboBox();

        public string TitleName = "RPG Paper Maker " + Application.ProductVersion;
        public MainFormControl Control = new MainFormControl();
        public bool IsInItemHeightSquare = false;
        public bool IsInItemHeightPixel = false;
        public bool IsUsingCursorSelector = false;
        public string CopiedMap = "";


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public MainForm()
        {
            #region Constructor

            InitializeComponent();

            Control.InitializeMain();

            // This code created the basic tree map nodes settings
            /*
            TreeNode rootNode, directoryNode, mapNode;
            rootNode = TreeMap.Nodes.Add("Maps");
            rootNode.Tag = TreeTag.CreateRoot();
            directoryNode = rootNode.Nodes.Add("Plains");
            directoryNode.Tag = TreeTag.CreateDirectory();
            mapNode = directoryNode.Nodes.Add("MAP0001");
            mapNode.Tag = TreeTag.CreateMap("MAP0001", "MAP0001");
            WANOK.SaveTree(TreeMap, "TreeMapDatas.rpmdatas");
            */

            // Recent projects
            List <string> listRecent = WANOK.Settings.ListRecentProjects;
            for (int i = listRecent.Count-1; i >= 0; i--)
            {
                AddToRecentList(listRecent[i]);
            }

            // Updating special infos
            Text = TitleName;
            KeyPreview = true;
            TilesetSelectorPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            TilesetSelectorPicture.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            PictureBoxSpecialTileset.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxSpecialTileset.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            MapEditor.MouseWheel += MapEditor_MouseWheel;
            MouseWheel += MainForm_MouseWheel;
            ItemFloor.DropDown.MouseLeave += new EventHandler(ItemFloorDrop_MouseLeave);
            ItemSprite.DropDown.MouseLeave += new EventHandler(ItemSpriteDrop_MouseLeave);
            ItemDrawMode.DropDown.MouseLeave += new EventHandler(ItemDrawModeDrop_MouseLeave);
            ItemHeight.DropDown.MouseLeave += new EventHandler(ItemHeightDrop_MouseLeave);
            ComboBoxSpecialTileset1.SelectedIndexChanged += new EventHandler(ComboBoxSpecialTileset1_SelectedIndexChanged);


            // Contain shown
            EnableNoGame();
            ShowProjectContain(false);
            HideSpecialTileset();
            menuStrip1.Renderer = new MainRender(this);
            menuStrip2.Renderer = new MainRender2(this);

            #endregion
        }

        // -------------------------------------------------------------------
        // Renders : All settings for main menu strip (color etc.)
        // -------------------------------------------------------------------

        #region Renders

        public static void PaintBorderGroupBox(object sender, PaintEventArgs e)
        {
            GroupBox box = (GroupBox)sender;
            DrawGroupBox(box, e.Graphics, Color.Black, Color.Silver);
        }

        public static void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                g.Clear(SystemColors.Control);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }

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

        private class MainRender : ToolStripProfessionalRenderer
        {
            public MainForm MainForm;

            public MainRender(MainForm mainForm) : base(new MainColorTable())
            {
                MainForm = mainForm;
            }    

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle r = new Rectangle(Point.Empty, e.Item.Size);
                if (e.Item.Name == MainForm.MapEditor.SelectedDrawType)
                {
                    if (e.Item.Selected) e.Graphics.FillRectangle(Brushes.DarkCyan, r);
                    else e.Graphics.FillRectangle(Brushes.CadetBlue, r);
                }
                else
                {
                    if (!e.Item.Selected)
                    {
                        SolidBrush brush;
                        if (e.Item.Pressed) brush = new SolidBrush(Color.FromArgb(100, 100, 100));
                        else brush = new SolidBrush(Color.FromArgb(64, 64, 64));
                        e.Graphics.FillRectangle(brush, r);
                    }
                    else {
                        e.Graphics.FillRectangle(Brushes.CadetBlue, r);
                    }
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

        private class MainRender2 : ToolStripProfessionalRenderer
        {
            public MainForm MainForm;

            public MainRender2(MainForm mainForm) : base(new MainColorTable())
            {
                MainForm = mainForm;
            }

            protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
            {
                OnRenderMenuItemBackground(e);
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle r = new Rectangle(Point.Empty, e.Item.Size);
                if (e.Item.Name == MainForm.MapEditor.SelectedDrawType)
                {
                    e.Graphics.FillRectangle(Brushes.CadetBlue, r);
                }
                else
                {
                    SolidBrush brush;
                    if (e.Item.Selected || e.Item.Pressed) brush = new SolidBrush(Color.FromArgb(100, 100, 100));
                    else brush = new SolidBrush(Color.FromArgb(64, 64, 64));
                    e.Graphics.FillRectangle(brush, r);
                }
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {

            }
        }

        #endregion

        // -------------------------------------------------------------------
        // MainForm events
        // -------------------------------------------------------------------

        #region MainForm events

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (WANOK.Settings.ShowDemoTip)
            {
                DemoTip();
            }
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (IsInItemHeightSquare || IsInItemHeightPixel)
            {
                Control.SetHeight(IsInItemHeightSquare, e.Delta > 0);
                MapEditor.SetGridHeight(Control.GetHeight());
                ItemHeight1.Text = "Square number: " + Control.HeightSquare;
                ItemHeight2.Text = "Adding pixels: " + Control.HeightPixel;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WhenClosingAnyProject();
        }

        #endregion

        // -------------------------------------------------------------------
        // Keyboard management
        // -------------------------------------------------------------------

        #region Keyboard management

        // -------------------------------------------------------------------
        // ProcessCmdKey
        // -------------------------------------------------------------------

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down)
            {
                if (MapEditor.Focused) WANOK.KeyboardManager.SetKeyDownStatus((Microsoft.Xna.Framework.Input.Keys)keyData);
                if (!MapEditor.Focused)
                {
                    if (keyData == Keys.Up) UpdateTreeMapKeyUp();
                    if (keyData == Keys.Down) UpdateTreeMapKeyDown();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // -------------------------------------------------------------------
        // Form1_KeyUp
        // -------------------------------------------------------------------

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (MapEditor.Focused) WANOK.KeyboardManager.SetKeyUpStatus(SpecialKeys(e.KeyCode));
        }

        // -------------------------------------------------------------------
        // Form1_KeyDown
        // -------------------------------------------------------------------

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (MapEditor.Focused) WANOK.KeyboardManager.SetKeyDownStatus(SpecialKeys(e.KeyCode));
            if (!MapEditor.Focused)
            {
                if (e.Control && e.KeyCode == Keys.C) CopyMap();
                if (e.Control && e.KeyCode == Keys.V) PasteMap();
            }

            e.SuppressKeyPress = true;
        }

        // -------------------------------------------------------------------
        // SpecialKeys
        // -------------------------------------------------------------------

        public static Microsoft.Xna.Framework.Input.Keys SpecialKeys(Keys k)
        {
            switch (k)
            {
                case Keys.ControlKey:
                    return Convert.ToBoolean(GetAsyncKeyState(Keys.LControlKey)) ? Microsoft.Xna.Framework.Input.Keys.LeftControl : Microsoft.Xna.Framework.Input.Keys.RightControl;
                case Keys.Menu:
                    return Convert.ToBoolean(GetAsyncKeyState(Keys.LMenu)) ? Microsoft.Xna.Framework.Input.Keys.LeftAlt : Microsoft.Xna.Framework.Input.Keys.RightAlt;
                case Keys.ShiftKey:
                    return Convert.ToBoolean(GetAsyncKeyState(Keys.LShiftKey)) ? Microsoft.Xna.Framework.Input.Keys.LeftShift : Microsoft.Xna.Framework.Input.Keys.RightShift;
            }

            return (Microsoft.Xna.Framework.Input.Keys)k;
        }

        #endregion

        // -------------------------------------------------------------------
        // Main menu bar
        // -------------------------------------------------------------------

        #region Main menu bar

        // FILE

        private void ItemNewProject_Click(object sender, EventArgs e)
        {
            Control.OpenNewDialog();
            if (WANOK.DemoStep == DemoSteps.New)
            {
                WANOK.CurrentDemoDialog.Close();
            }

            DialogNewProject dialog = new DialogNewProject();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OpenProject(dialog.GetProjectName(), dialog.GetDirPath());
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

        private void ItemSave_Click(object sender, EventArgs e)
        {
            Control.SaveMap();
            MapEditor.SaveMap();
            WANOK.SelectedNode.Text = ((TreeTag)WANOK.SelectedNode.Tag).MapName;
        }

        private void ItemSaveAll_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void ItemCloseProject_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        private void ItemExit_Click(object sender, EventArgs e)
        {
            WhenClosingAnyProject();
            Close();
        }

        // MANAGEMENT

        private void ItemInputs_Click(object sender, EventArgs e)
        {
            Control.OpenNewDialog();
            DialogInputsManager dialog = new DialogInputsManager();
            dialog.ShowDialog();
        }

        private void ItemDataBase_Click(object sender, EventArgs e)
        {
            Control.OpenNewDialog();
            DialogDataBase dialog = new DialogDataBase();
            if (dialog.ShowDialog() == DialogResult.OK){
                SetTitle();
                TreeTag tag = (TreeTag)WANOK.SelectedNode.Tag;
                if (tag.IsMap) ReLoadMap(tag.RealMapName);
            }
        }

        // TEST

        private void ItemPlay_Click(object sender, EventArgs e)
        {
            WhenLaunchingGame();
            /*
            ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Users\Marie_MSI\Documents\Visual Studio 2015\Projects\Test\MonoGame-3D-Game-Test\Test\bin\DesktopGL\x86\Release\RPG Paper Maker.exe");
            startInfo.WorkingDirectory = @"C:\Users\Marie_MSI\Documents\Visual Studio 2015\Projects\Test\MonoGame-3D-Game-Test\Test\bin\DesktopGL\x86\Release";
            Process p = Process.Start(startInfo);
            p.WaitForExit();
            */
            ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine(WANOK.CurrentDir, "Game.exe"));
            startInfo.WorkingDirectory = WANOK.CurrentDir;
            Process p = Process.Start(startInfo);
            p.WaitForExit(); 
        }

        // OPTIONS

        private void ItemRTP_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = WANOK.SystemDatas.PathRTP;
            dialog.Description = "Select your new RTP directory here.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                WANOK.SystemDatas.PathRTP = dialog.SelectedPath;
            }
        }

        // HELP

        private void ItemTutorials_Click(object sender, EventArgs e)
        {
            Process.Start("http://rpgpapermaker.com/index.php/tutorials");
        }

        private void ItemDemo_Click(object sender, EventArgs e)
        {
            DemoTip();
        }

        private void ItemAbout_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.Text = "About RPG Paper Maker";
            box.ShowDialog();
        }

        #endregion

        // -------------------------------------------------------------------
        // Toolbar
        // -------------------------------------------------------------------

        #region Toolbar

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
            else if (e.Button.Name.Equals("toolBarButtonSave"))
            {
                ItemSave_Click(sender, e);
            }
            else if (e.Button.Name.Equals("toolBarButtonSaveAll"))
            {
                ItemSaveAll_Click(sender, e);
            }
            else if (e.Button.Name.Equals("toolBarButtonInput"))
            {
                ItemInputs_Click(sender, e);
            }
            else if (e.Button.Name.Equals("toolBarButtonPlay"))
            {
                ItemPlay_Click(sender, e);
            }
            else if (e.Button.Name.Equals("toolBarButtonDataBase"))
            {
                ItemDataBase_Click(sender, e);
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // Map editor menu bar
        // -------------------------------------------------------------------

        #region Map editor menu bar

        private void menuStrip2_MouseHover(object sender, EventArgs e)
        {
            Control.OpenNewDialog();
            menuStrip2.Focus();
        }

        // DropDown auto

        private void ItemFloorDrop_MouseLeave(object sender, EventArgs e)
        {
            ItemFloor.HideDropDown();
        }

        private void ItemFloor_MouseEnter(object sender, EventArgs e)
        {
            ItemFloor.ShowDropDown();
        }

        private void ItemFloor_MouseLeave(object sender, EventArgs e)
        {
            HideDropDownIfNotInControl(ItemFloor);
        }

        private void ItemSpriteDrop_MouseLeave(object sender, EventArgs e)
        {
            ItemSprite.HideDropDown();
        }

        private void ItemSprite_MouseEnter(object sender, EventArgs e)
        {
            ItemSprite.ShowDropDown();
        }

        private void ItemSprite_MouseLeave(object sender, EventArgs e)
        {
            HideDropDownIfNotInControl(ItemSprite);
        }

        private void ItemDrawModeDrop_MouseLeave(object sender, EventArgs e)
        {
            ItemDrawMode.HideDropDown();
        }

        private void ItemDrawMode_MouseEnter(object sender, EventArgs e)
        {
            ItemDrawMode.ShowDropDown();
        }

        private void ItemDrawMode_MouseLeave(object sender, EventArgs e)
        {
            HideDropDownIfNotInControl(ItemDrawMode);
        }

        private void ItemHeightDrop_MouseLeave(object sender, EventArgs e)
        {
            ItemHeight.HideDropDown();
        }

        private void ItemHeight_MouseEnter(object sender, EventArgs e)
        {
            ItemHeight.ShowDropDown();
        }

        private void ItemHeight_MouseLeave(object sender, EventArgs e)
        {
            HideDropDownIfNotInControl(ItemHeight);
        }

        // Height

        private void ItemHeight1_MouseEnter(object sender, EventArgs e)
        {
            IsInItemHeightSquare = true;
        }

        private void ItemHeight1_MouseLeave(object sender, EventArgs e)
        {
            IsInItemHeightSquare = false;
        }

        private void ItemHeight2_MouseEnter(object sender, EventArgs e)
        {
            IsInItemHeightPixel = true;
        }

        private void ItemHeight2_MouseLeave(object sender, EventArgs e)
        {
            IsInItemHeightPixel = false;
        }

        // Floors

        private void ItemFloor_Click(object sender, EventArgs e)
        {
            SelectFloors();
        }

        private void ItemFloor1_Click(object sender, EventArgs e)
        {
            SelectFloor();
        }

        private void ItemFloor2_Click(object sender, EventArgs e)
        {
            SelectAutotiles();
        }

        public void SelectFloors()
        {
            switch (ItemFloor.Text)
            {
                case "Floors":
                    SelectFloor();
                    break;
                case "Autotiles":
                    SelectAutotiles();
                    break;
            }
        }

        public void SelectFloor()
        {
            SetSelectedDrawType("ItemFloor");
            SetSelectedDrawTypeParticular(DrawType.Floors);
            ItemFloor.Text = ItemFloor1.Text;
            ItemFloor.Image = ItemFloor1.Image;
            HideSpecialTileset();
        }

        public void SelectAutotiles()
        {
            SetSelectedDrawType("ItemFloor");
            SetSelectedDrawTypeParticular(DrawType.Autotiles);
            ItemFloor.Text = ItemFloor2.Text;
            ItemFloor.Image = ItemFloor2.Image;
            ShowSpecialTileset();
            MapEditor.SetCurrentAutotileId(0);
            Tileset tileset = MapEditor.GetMapTileset();
            for (int i = 0; i < tileset.Autotiles.Count; i++)
            {
                SystemAutotile autotile = WANOK.SystemDatas.GetAutotileById(tileset.Autotiles[i]);
                ComboBoxSpecialTileset1.Items.Add(new DropDownItem(WANOK.GetStringComboBox(autotile.Id,autotile.Name), autotile.Graphic.LoadImage()));
            }
            if (ComboBoxSpecialTileset1.Items.Count > 0) ComboBoxSpecialTileset1.SelectedIndex = 0;
            PanelSpecialMenu.Controls.Add(ComboBoxSpecialTileset1);
        }

        // Sprite

        private void ItemSprite_Click(object sender, EventArgs e)
        {
            SelectSprites();
        }

        private void ItemSprite1_Click(object sender, EventArgs e)
        {
            SelectFaceSprite();
        }

        private void ItemSprite2_Click(object sender, EventArgs e)
        {
            SelectFixSprite();
        }

        private void ItemSprite3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Action unavailable now.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ItemSprite4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Action unavailable now.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ItemSprite5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Action unavailable now.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ItemSprite6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Action unavailable now.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void SelectSprites()
        {
            switch (ItemSprite.Text)
            {
                case "Face sprite":
                    SelectFaceSprite();
                    break;
                case "Fix sprite":
                    SelectFixSprite();
                    break;
            }
        }

        public void SelectFaceSprite()
        {
            SetSelectedDrawType("ItemSprite");
            SetSelectedDrawTypeParticular(DrawType.FaceSprite);
            ItemSprite.Text = ItemSprite1.Text;
            ItemSprite.Image = ItemSprite1.Image;
            HideSpecialTileset();
        }

        public void SelectFixSprite()
        {
            SetSelectedDrawType("ItemSprite");
            SetSelectedDrawTypeParticular(DrawType.FixSprite);
            ItemSprite.Text = ItemSprite2.Text;
            ItemSprite.Image = ItemSprite2.Image;
            HideSpecialTileset();
        }

        // Start

        private void ItemStart_Click(object sender, EventArgs e)
        {
            SetSelectedDrawType("ItemStart");
        }

        // DrawMode

        private void ItemDrawMode1_Click(object sender, EventArgs e)
        {
            MapEditor.DrawMode = DrawMode.Pencil;
            ItemDrawMode.Image = Properties.Resources.pencil;
        }

        private void ItemDrawMode2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Action unavailable now.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ItemDrawMode3_Click(object sender, EventArgs e)
        {
            MapEditor.DrawMode = DrawMode.Tin;
            ItemDrawMode.Image = Properties.Resources.tin;
        }

        #endregion

        // -------------------------------------------------------------------
        // Tree Map
        // -------------------------------------------------------------------

        #region Tree Map

        public void SaveTreeMap()
        {
            Control.SaveTreeMap(TreeMap);
        }

        // Drag & drop

        private void TreeMap_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void TreeMap_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void TreeMap_DragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = TreeMap.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = TreeMap.GetNodeAt(targetPoint);
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (!draggedNode.Equals(targetNode) && targetNode != null && !((TreeTag)targetNode.Tag).IsMap && !Control.IsATreeChild(targetNode, draggedNode))
            {
                draggedNode.Remove();
                targetNode.Nodes.Add(draggedNode);
                targetNode.Expand();
                SaveTreeMap();
            }
        }

        public void UpdateTreeMapKeyUp()
        {
            TreeNode movingNode = TreeMap.SelectedNode;
            TreeNode parentNode = movingNode.Parent;
            if (parentNode != null)
            {
                int index = movingNode.Index;

                if (index > 0)
                {
                    movingNode.Remove();
                    parentNode.Nodes.Insert(index - 1, movingNode);
                    TreeMap.SelectedNode = movingNode;
                }
            }
        }

        public void UpdateTreeMapKeyDown()
        {
            TreeNode movingNode = TreeMap.SelectedNode;
            TreeNode parentNode = movingNode.Parent;

            if (parentNode != null)
            {
                int index = movingNode.Index;

                if (index < movingNode.Parent.Nodes.Count - 1)
                {
                    movingNode.Remove();
                    parentNode.Nodes.Insert(index + 1, movingNode);
                    TreeMap.SelectedNode = movingNode;
                }
            }
        }

        // On right click

        private void TreeMap_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeMap.SelectedNode = e.Node;

                // Enable/disable
                EnableTreePopUpDir(TreeMap.SelectedNode != Control.FindRootNode(TreeMap.SelectedNode));

                // Show context menu
                TreeTag tag = (TreeTag)e.Node.Tag;
                if (tag.IsMap)
                {
                    ContextMenuMap.Show(TreeMap, e.Location);
                }
                else
                {
                    ContextMenuDir.Show(TreeMap, e.Location);
                }
            }
        }

        // Showing map or not

        private void TreeMap_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeTag previousTag = (TreeTag)WANOK.SelectedNode.Tag;
            TreeTag tag = (TreeTag)e.Node.Tag;

            // If the previous node selected was a map and have been saved, we can delete temp files
            if (previousTag.IsMap && !WANOK.ListMapToSave.Contains(previousTag.RealMapName))
            {
                Control.DeleteTemp(previousTag.RealMapName);
            }
            if (tag.IsMap && !WANOK.ListMapToSave.Contains(tag.RealMapName))
            {
                Control.DeleteTemp(tag.RealMapName);
            }
            WANOK.SelectedNode = e.Node;

            // Reload map if selecting a new map
            if (tag.IsMap)
            {
                ShowMapEditor(true);
                ReLoadMap(tag.RealMapName);
            }
            else
            {
                ShowMapEditor(false);
            }
        }

        // Popup menus

        private void MenuItemNewDir_Click(object sender, EventArgs e)
        {
            Control.OpenNewDialog();
            DialogNewDir dialog = new DialogNewDir();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TreeNode node = TreeMap.SelectedNode.Nodes.Insert(0, dialog.DirectoryName);
                TreeMap.ExpandAll();
                node.Tag = TreeTag.CreateDirectory();
                TreeMap.SelectedNode = node;
                SaveTreeMap();
            }
        }

        private void MenuItemNewMap_Click(object sender, EventArgs e)
        {
            Control.OpenNewDialog();
            if (Directory.GetDirectories(WANOK.MapsDirectoryPath).Length < MainFormControl.MAX_MAP)
            {
                DialogNewMap dialog = new DialogNewMap();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    TreeNode node = TreeMap.SelectedNode.Nodes.Insert(0, dialog.GetMapName());
                    TreeMap.ExpandAll();
                    node.Tag = TreeTag.CreateMap(dialog.GetMapName(), dialog.GetRealMapName());
                    TreeMap.SelectedNode = node;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                    SaveTreeMap();
                }
            }
            else
            {
                MessageBox.Show("Action unavailable, the max map number is " + MainFormControl.MAX_MAP, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MenuItemPasteMap_Click(object sender, EventArgs e)
        {
            PasteMap();
        }

        private void MenuItemSetDirName_Click(object sender, EventArgs e)
        {
            Control.OpenNewDialog();
            DialogNewDir dialog = new DialogNewDir(TreeMap.SelectedNode.Text);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TreeMap.SelectedNode.Text = dialog.DirectoryName;
                SaveTreeMap();
            }
        }

        private void MenuItemDeleteDir_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this directory? All the content inside will be deleted too.", "Delete the directory", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    DeleteMapsDirectory(TreeMap.SelectedNode);
                } catch
                {
                    MessageBox.Show("Error while trying to delete your files. Maybe you have your explorer opened in one of the folders.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SaveTreeMap();
            }
        }

        private void MenuItemSetMap_Click(object sender, EventArgs e)
        {
            Control.OpenNewDialog();
            string mapName = ((TreeTag)TreeMap.SelectedNode.Tag).RealMapName;
            DialogNewMap dialog = new DialogNewMap(Control.LoadMapInfos(mapName));
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ReLoadMap(mapName);
                TreeMap.SelectedNode.Text = dialog.GetMapName() + " *";
                MapEditor.SaveMap(false);
                WANOK.ListMapToSave.Add(dialog.GetRealMapName());
                SaveTreeMap();
            }
        }

        private void MenuItemMoveMap_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Action unavailable now.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MenuItemCopyMap_Click(object sender, EventArgs e)
        {
            CopyMap();
        }

        private void MenuItemDeleteMap_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this map?", "Delete the map", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    Control.DeleteMapsDirectory(((TreeTag)TreeMap.SelectedNode.Tag).RealMapName);
                    TreeMap.SelectedNode.Remove();
                }
                catch
                {
                    MessageBox.Show("Error while trying to delete your files. Maybe you have your explorer opened in one of the folders.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SaveTreeMap();
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // TilesetSelector
        // -------------------------------------------------------------------

        #region TilesetSelector

        private void TilesetSelectorPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsUsingCursorSelector = true;
                TilesetSelectorPicture.MakeFirstRectangleSelection(e.X, e.Y);
                TilesetSelectorPicture.Refresh();
            }
        }

        private void TilesetSelectorPicture_MouseUp(object sender, MouseEventArgs e)
        {
            TilesetSelectorPicture.SetCursorRealPosition();
            IsUsingCursorSelector = false;
            TilesetSelectorPicture.Refresh();
            MapEditor.SetCurrentTexture(TilesetSelectorPicture.GetCurrentTexture());
        }

        private void TilesetSelectorPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsUsingCursorSelector)
            {
                TilesetSelectorPicture.MakeRectangleSelection(e.X, e.Y);
                TilesetSelectorPicture.Refresh();
            }
        }

        private void TilesetSelectorPicture_MouseEnter(object sender, EventArgs e)
        {
            TilesetSelectorPicture.Focus();
        }

        private void ComboBoxSpecialTileset1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxSpecialTileset1.SelectedIndex != -1)
            {
                Tileset tileset = MapEditor.GetMapTileset();
                PictureBoxSpecialTileset.Image = WANOK.SystemDatas.GetAutotileById(tileset.Autotiles[ComboBoxSpecialTileset1.SelectedIndex]).Graphic.LoadImage();
                PictureBoxSpecialTileset.Size = new Size((int)(PictureBoxSpecialTileset.Image.Width * WANOK.RELATION_SIZE), (int)(PictureBoxSpecialTileset.Image.Height * WANOK.RELATION_SIZE));
                PictureBoxSpecialTileset.Location = new Point(0, 0);
                MapEditor.SetCurrentAutotileId(tileset.Autotiles[ComboBoxSpecialTileset1.SelectedIndex]);
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // MapEditor
        // -------------------------------------------------------------------

        #region MapEditor

        private void MapEditor_MouseEnter(object sender, EventArgs e)
        {
            MapEditor.Focus();
        }

        private void MapEditor_MouseWheel(object sender, MouseEventArgs e)
        {
            WANOK.MapMouseManager.SetWheelStatus(e.Delta);
        }

        private void MapEditor_MouseDown(object sender, MouseEventArgs e)
        {
            WANOK.MapMouseManager.SetMouseDownStatus(e);
        }

        private void MapEditor_MouseUp(object sender, MouseEventArgs e)
        {
            WANOK.MapMouseManager.SetMouseUpStatus(e);
        }

        private void MapEditor_MouseMove(object sender, MouseEventArgs e)
        {
            WANOK.MapMouseManager.SetPosition(e.X, e.Y);
        }

        #endregion

        // -------------------------------------------------------------------
        // SET OF FUNCTIONS --------------------------------------------------
        // -------------------------------------------------------------------

        #region functions

        // -------------------------------------------------------------------
        // ShowSpecialTileset
        // -------------------------------------------------------------------

        public void ShowSpecialTileset()
        {
            scrollPanelTileset.Controls.Clear();
            scrollPanelTileset.Controls.Add(PictureBoxSpecialTileset);
            PanelSpecialMenu.Controls.Clear();
            ComboBoxSpecialTileset1.Items.Clear();
            tableLayoutPanelTileset.RowStyles[0] = new RowStyle(SizeType.Absolute, 27);
        }

        // -------------------------------------------------------------------
        // HideSpecialTileset
        // -------------------------------------------------------------------

        public void HideSpecialTileset()
        {
            tableLayoutPanelTileset.RowStyles[0] = new RowStyle(SizeType.Absolute, 0);
            scrollPanelTileset.Controls.Clear();
            scrollPanelTileset.Controls.Add(TilesetSelectorPicture);
        }

        // -------------------------------------------------------------------
        // ShowProjectContain
        // -------------------------------------------------------------------

        public void ShowProjectContain(bool b)
        {
            SplitContainerMain.Visible = b;
            SplitContainerTree.Visible = b;
            MapEditor.Visible = b;
            PanelSpecialMenu.Visible = b;
            TilesetSelectorPicture.Visible = b;
            PictureBoxSpecialTileset.Visible = b;
            TreeMap.Visible = b;
        }

        // -------------------------------------------------------------------
        // ShowMapEditor
        // -------------------------------------------------------------------

        public void ShowMapEditor(bool b)
        {
            MapEditor.Visible = b;
            menuStrip2.Visible = b;
            PanelSpecialMenu.Visible = b;
            TilesetSelectorPicture.Visible = b;
            PictureBoxSpecialTileset.Visible = b;
        }

        // -------------------------------------------------------------------
        // SetTitle
        // -------------------------------------------------------------------

        public void SetTitle(string dir)
        {
            Text = TitleName + " - " + Control.SetTitle(dir);
        }

        public void SetTitle()
        {
            Text = TitleName + " - " + Control.SetTitle();
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
                ItemNewProject.Enabled = true;
                toolBarButtonNew.Enabled = true;
                fileToolStripMenuItem.Enabled = true;
                StartDemo();
            }
        }

        // -------------------------------------------------------------------
        // EnableAll
        // -------------------------------------------------------------------

        public void EnableAll(bool b)
        {
            ItemNewProject.Enabled = b;
            toolBarButtonNew.Enabled = b;
            ItemOpenBrowse.Enabled = b;
            toolBarButtonOpen.Enabled = b;
            ItemInputs.Enabled = b;
            toolBarButtonInput.Enabled = b;
            ItemDataBase.Enabled = b;
            toolBarButtonDataBase.Enabled = b;
            ItemSave.Enabled = b;
            toolBarButtonSave.Enabled = b;
            ItemSaveAll.Enabled = b;
            toolBarButtonSaveAll.Enabled = b;
            ItemCloseProject.Enabled = b;
            ItemExit.Enabled = b;
            ItemPlay.Enabled = b;
            toolBarButtonPlay.Enabled = b;
            ItemRTP.Enabled = b;
            ItemTutorials.Enabled = b;
            ItemDemo.Enabled = b;
            ItemAbout.Enabled = b;
        }

        // -------------------------------------------------------------------
        // EnableNoGame
        // -------------------------------------------------------------------

        public void EnableNoGame()
        {
            EnableAll(false);
            ItemNewProject.Enabled = true;
            toolBarButtonNew.Enabled = true;
            ItemOpenBrowse.Enabled = true;
            toolBarButtonOpen.Enabled = true;
            ItemExit.Enabled = true;
            ItemTutorials.Enabled = true;
            ItemDemo.Enabled = true;
            ItemAbout.Enabled = true;
        }

        // -------------------------------------------------------------------
        // EnableGame
        // -------------------------------------------------------------------

        public void EnableGame()
        {
            EnableNoGame();
            ItemSave.Enabled = true;
            toolBarButtonSave.Enabled = true;
            ItemSaveAll.Enabled = true;
            toolBarButtonSaveAll.Enabled = true;
            ItemCloseProject.Enabled = true;
            ItemInputs.Enabled = true;
            toolBarButtonInput.Enabled = true;
            ItemDataBase.Enabled = true;
            toolBarButtonDataBase.Enabled = true;
            ItemPlay.Enabled = true;
            toolBarButtonPlay.Enabled = true;
            ItemRTP.Enabled = true;
        }

        // -------------------------------------------------------------------
        // EnableTreePopUpDir
        // -------------------------------------------------------------------

        public void EnableTreePopUpDir(bool b)
        {
            MenuItemSetDirName.Enabled = b;
            MenuItemDeleteDir.Enabled = b;
        }

        // -------------------------------------------------------------------
        // OpenProject
        // -------------------------------------------------------------------

        public void OpenProject(string name, string dir)
        {
            WhenClosingAnyProject();
            if (Directory.Exists(dir))
            {
                Control.CloseProject();
                SetTitle(dir);
                TreeMap.Nodes.Clear();
                WANOK.LoadTree(TreeMap, Path.Combine(new string[] { WANOK.CurrentDir, "Content", "Datas", "Maps", "TreeMapDatas.rpmdatas" }));
                TreeMap.ExpandAll();
                WANOK.SelectedNode = TreeMap.Nodes[0];
                AddToRecentList(dir, WANOK.Settings.AddProjectPath(dir));
                WANOK.SaveDatas(WANOK.Settings, WANOK.PATHSETTINGS);
                ShowProjectContain(true);
                EnableGame();
                ShowMapEditor(false);
            }
            else
            {
                MessageBox.Show("Error : can't open the project!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // -------------------------------------------------------------------
        // AddToRecentList
        // -------------------------------------------------------------------

        public void AddToRecentList(string path, int index = -1)
        {
            if (index != -1) ItemOpenProject.DropDownItems.RemoveAt(index + 1);
            ToolStripItem item = ItemOpenProject.DropDownItems.Add(path);
            ItemOpenProject.DropDownItems.Insert(1, item);
            if (ItemOpenProject.DropDownItems.Count-1 > EngineSettings.MAX_RECENT_SIZE) ItemOpenProject.DropDownItems.RemoveAt(ItemOpenProject.DropDownItems.Count - 1);
            item.ForeColor = SystemColors.ControlLightLight;
            item.Click += delegate (object sender, EventArgs e){ OpenProject(Path.GetFileName(path), path); };
        }

        // -------------------------------------------------------------------
        // CloseProject
        // -------------------------------------------------------------------

        public void CloseProject()
        {
            WhenClosingAnyProject();
            Control.CloseProject();
            Text = TitleName;
            EnableNoGame();
            ShowProjectContain(false);
        }

        // -------------------------------------------------------------------
        // DeleteMapsDirectory
        // -------------------------------------------------------------------

        public void DeleteMapsDirectory(TreeNode dir)
        {
            List<TreeNode> list = new List<TreeNode>(dir.Nodes.Cast<TreeNode>().ToList());
            foreach (TreeNode node in list)
            {
                if (((TreeTag)node.Tag).IsMap)
                {
                    Control.DeleteMapsDirectory(((TreeTag)node.Tag).RealMapName);
                    TreeMap.Nodes.Remove(node);
                }
                else
                {
                    DeleteMapsDirectory(node);
                }
            }

            TreeMap.Nodes.Remove(dir);
        }

        // -------------------------------------------------------------------
        // HideDropDownIfNotInControl
        // -------------------------------------------------------------------

        public void HideDropDownIfNotInControl(ToolStripMenuItem c)
        {
            if (!c.DropDown.ClientRectangle.Contains(c.DropDown.PointToClient(Cursor.Position)))
            {
                c.HideDropDown();
                menuStrip2.Focus();
            }
        }

        // -------------------------------------------------------------------
        // SetSelectedDrawType
        // -------------------------------------------------------------------

        public void SetSelectedDrawType(string item)
        {
            MapEditor.SelectedDrawType = item;
            menuStrip2.Refresh();
        }

        // -------------------------------------------------------------------
        // SetSelectedDrawTypeParticular
        // -------------------------------------------------------------------

        public void SetSelectedDrawTypeParticular(DrawType item)
        {
            MapEditor.SelectedDrawTypeParticular = item;
            menuStrip2.Refresh();
        }

        // -------------------------------------------------------------------
        // ReloadMenuMapEditor
        // -------------------------------------------------------------------

        public void ReloadMenuMapEditor()
        {
            Control.ClearHeight();
            ItemHeight1.Text = "Square number: 0";
            ItemHeight2.Text = "Adding pixels: 0";
        }

        // -------------------------------------------------------------------
        // WhenClosingAnyProject
        // -------------------------------------------------------------------

        public void WhenClosingAnyProject()
        {
            if (WANOK.ListMapToSave.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("You have some maps that have not been saved. Do you want to save it before closing the project?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    Control.SaveAllMaps(true);
                }
                else if (dialog == DialogResult.No)
                {
                    Control.DeleteAllTemp();
                }
            }
            else if (WANOK.SelectedNode != null && ((TreeTag)WANOK.SelectedNode.Tag).IsMap)
            {
                Control.DeleteTemp(((TreeTag)WANOK.SelectedNode.Tag).RealMapName);
            }
        }

        // -------------------------------------------------------------------
        // WhenLaunchingGame
        // -------------------------------------------------------------------

        public void WhenLaunchingGame()
        {
            if (WANOK.ListMapToSave.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("You have some maps that have not been saved. Do you want to save it before playing the project?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    SaveAll();
                }
            }
        }

        // -------------------------------------------------------------------
        // SaveAll
        // -------------------------------------------------------------------

        public void SaveAll()
        {
            Control.SaveAllMaps(false);
            MapEditor.SaveMap();
            TreeNode[] nodeList = new TreeNode[] { TreeMap.Nodes[0] };
            WANOK.SetIconsTreeNodes(nodeList);
        }

        // -------------------------------------------------------------------
        // SetTilesetImage
        // -------------------------------------------------------------------

        public void ReLoadMap(string mapName)
        {
            MapEditor.ReLoadMap(mapName);
            TilesetSelectorPicture.LoadTexture(MapEditor.GetMapTileset().Graphic);
            MapEditor.SetCurrentTexture(new int[] { 0, 0, 1, 1 });
            TilesetSelectorPicture.SetCurrentTexture(0, 0, 1, 1);
            TilesetSelectorPicture.Refresh();
            SelectFloors();
        }

        // -------------------------------------------------------------------
        // CopyMap
        // -------------------------------------------------------------------

        public void CopyMap()
        {
            TreeTag tag = (TreeTag)TreeMap.SelectedNode.Tag;
            if (tag.IsMap) CopiedMap = tag.RealMapName;
        }

        // -------------------------------------------------------------------
        // PasteMap
        // -------------------------------------------------------------------

        public void PasteMap()
        {
            TreeTag tag = (TreeTag)TreeMap.SelectedNode.Tag;

            if (!tag.IsMap && CopiedMap != "")
            {
                Control.PasteMap(CopiedMap);
                string mapName = WANOK.LoadBinaryDatas<MapInfos>(Path.Combine(WANOK.MapsDirectoryPath, CopiedMap, "infos.map")).MapName;
                TreeNode node = TreeMap.SelectedNode.Nodes.Insert(0, mapName);
                TreeMap.ExpandAll();
                node.Tag = TreeTag.CreateMap(mapName, CopiedMap);
                TreeMap.SelectedNode = node;
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                SaveTreeMap();
            }
        }

        #endregion

        // -------------------------------------------------------------------
        // DEMO STEPS --------------------------------------------------------
        // -------------------------------------------------------------------

        #region Demo

        // -------------------------------------------------------------------
        // StartDemo
        // -------------------------------------------------------------------

        public void StartDemo()
        {
            Thread.Sleep(100);
            WANOK.CurrentDemoDialog = new DialogDemoTipNewProject();
            WANOK.CurrentDemoDialog.Location = new Point(Location.X + 50, Location.Y + 100);
            WANOK.CurrentDemoDialog.Size = new Size(364, 209);
            WANOK.CurrentDemoDialog.Show();
            WANOK.DemoStep = DemoSteps.New;
            Select();
        }

        // -------------------------------------------------------------------
        // CancelDemo
        // -------------------------------------------------------------------

        public void CancelDemo()
        {
            if (WANOK.CurrentDir == ".")
            {
                EnableNoGame();
            }
            else
            {
                EnableGame();
            }
            WANOK.DemoStep = DemoSteps.None;
        }

        #endregion
    }
}
