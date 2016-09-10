using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class DialogEvent : Form
    {
        protected DialogEventControl Control;
        protected BindingSource ViewModelBindingSource = new BindingSource();
        private System.Timers.Timer GraphicFrameTimer = new System.Timers.Timer();
        protected Dictionary<EventTrigger, RadioButtonWithGroup> RadiosTrigger = new Dictionary<EventTrigger, RadioButtonWithGroup>();
        protected SystemEvent.SystemEventPage CopiedPage = null;
        protected List<EventCommand> CommandsSelected = null;
        private Timer CommandUnderscoreTimer = new Timer();
        private bool IsUnderScoreDisplayed = false;
        private string CurrentMethodString = "";
        protected TableLayoutPanel TableLayoutListCommandsPanel = new TableLayoutPanel();
        private Label ListCommandsSelectedLabel = null;


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogEvent(SystemEvent ev)
        {
            InitializeComponent();

            Control = new DialogEventControl(ev.CreateCopy());
            ViewModelBindingSource.DataSource = Control;

            // Timers
            GraphicFrameTimer.Interval = 150;
            GraphicFrameTimer.Start();
            CommandUnderscoreTimer.Interval = 250;

            // Radios trigger
            RadiosTrigger[EventTrigger.ActionButton] = radioButtonActionButton;
            RadiosTrigger[EventTrigger.PlayerTouch] = radioButtonPlayerTouch;
            RadiosTrigger[EventTrigger.EventTouch] = radioButtonEventTouch;
            RadiosTrigger[EventTrigger.Detection] = radioButtonDetection;
            RadiosTrigger[EventTrigger.Autorun] = radioButtonAutorun;
            RadiosTrigger[EventTrigger.ParallelProcess] = radioButtonParallelProcess;

            // Update pages
            UpdatePage(0);
            for (int i = 0; i < Control.Model.Pages.Count - 1; i++)
            {
                TabPage tabPage = new TabPage((i + 2).ToString());
                tabPage.Margin = new Padding(3);
                tabPage.Padding = new Padding(3);
                tabPage.BackColor = Color.White;
                tabControl1.TabPages.Add(tabPage);
            }
            buttonDeletePage.Enabled = tabControl1.TabPages.Count > 1;


            // Speed & frequency
            numericUpDownSpeed.DecimalPlaces = 2;
            numericUpDownSpeed.Minimum = (decimal)0.1;
            numericUpDownSpeed.Maximum = (decimal)999.0;
            numericUpDownSpeed.Increment = (decimal)0.5;
            numericUpDownSpeed.Value = (decimal)1.0;
            numericUpDownFrequency.DecimalPlaces = 2;
            numericUpDownFrequency.Minimum = (decimal)0.1;
            numericUpDownFrequency.Maximum = (decimal)999.0;
            numericUpDownFrequency.Increment = (decimal)0.5;
            numericUpDownFrequency.Value = (decimal)1.0;

            // CommandsView
            CommandsView.ExpandAll();
            TableLayoutListCommandsPanel.AutoSize = true;
            TableLayoutListCommandsPanel.Hide();
            CommandsView.Controls.Add(TableLayoutListCommandsPanel);
            
            // Event name
            textBoxEventName.Select();
            textBoxEventName.Focus();

            // Events
            graphicControl1.ClosingDialogEvent += graphicControl1_ClosingDialogEvent;
            GraphicFrameTimer.Elapsed += graphicFrameTimer_Elapsed;
            graphicControl1.GetComboBox().SelectedIndexChanged += DialogEvent_SelectedIndexChanged;
            CommandsView.LostFocus += CommandsView_LostFocus;
            CommandUnderscoreTimer.Tick += CommandUnderscoreTimer_Tick;
            TableLayoutListCommandsPanel.CellPaint += TableLayoutListCommandsPanel_CellPaint;

            // Trigger
            foreach (EventTrigger trigger in Enum.GetValues(typeof(EventTrigger)))
            {
                RadiosTrigger[trigger].GroupName = "trigger";
                RadiosTrigger[trigger].AutoCheck = false;
                RadiosTrigger[trigger].Click += radioTrigger_Clicked;
            }

            InitializeDataBindings();
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            textBoxEventName.DataBindings.Add("Text", ViewModelBindingSource, "Name", true);
            tabControl1.DataBindings.Add("SelectedIndex", ViewModelBindingSource, "CurrentPage", true);
            checkBoxMoveAnimation.DataBindings.Add("Checked", ViewModelBindingSource, "MoveAnimation", true);
            checkBoxStopAnimation.DataBindings.Add("Checked", ViewModelBindingSource, "StopAnimation", true);
            checkBoxDirectionFix.DataBindings.Add("Checked", ViewModelBindingSource, "DirectionFix", true);
            checkBoxThrough.DataBindings.Add("Checked", ViewModelBindingSource, "Through", true);
            checkBoxSetWithCamera.DataBindings.Add("Checked", ViewModelBindingSource, "SetWithCamera", true);
        }

        // -------------------------------------------------------------------
        // GetEvent
        // -------------------------------------------------------------------

        public SystemEvent GetEvent()
        {
            return Control.Model;
        }

        // -------------------------------------------------------------------
        // UpdatePage
        // -------------------------------------------------------------------

        public void UpdatePage(int i)
        {
            conditionsPanel1.InitializeListParameters(Control.Model.Pages[i].ConditionsTree.Tree);
            graphicControl1.InitializeListParameters(Control.Model.Pages[i].Graphic, MapEditor.GetMapTileset().Graphic);
            graphicControl1.GetComboBox().SelectedIndex = (int)Control.Model.Pages[i].GraphicDrawType;
            foreach (EventTrigger trigger in Enum.GetValues(typeof(EventTrigger)))
            {
                RadiosTrigger[trigger].Checked = false;
            }
            RadiosTrigger[Control.Model.Pages[i].Trigger].Checked = true;
            CommandsView.Nodes.Clear();
            AddCommandNodes(Control.Model.Pages[i].CommandsTree, CommandsView.Nodes);

            // Move page
            tabControl1.TabPages[i].Controls.Clear();
            tabControl1.TabPages[i].Controls.Add(panel1);
            panel1.Controls.Clear();
            panel1.Controls.Add(tableLayoutMainPage);
        }

        // -------------------------------------------------------------------
        // UpdateTrigger
        // -------------------------------------------------------------------

        public void UpdateTrigger(EventTrigger trigger)
        {
            Control.Model.Pages[Control.Model.CurrentPage].Trigger = trigger;
        }

        // -------------------------------------------------------------------
        // AddPage
        // -------------------------------------------------------------------

        public void AddPage()
        {
            tabControl1.TabPages.Insert(Control.Model.CurrentPage, (Control.Model.CurrentPage + 1).ToString());
            for (int i = Control.Model.CurrentPage + 1; i < tabControl1.TabPages.Count; i++)
            {
                tabControl1.TabPages[i].Text = (i + 1).ToString();
            }
            tabControl1.TabPages[Control.Model.CurrentPage].Padding = new Padding(3);
            tabControl1.TabPages[Control.Model.CurrentPage].BackColor = Color.White;
        }

        // -------------------------------------------------------------------
        // CreatePage
        // -------------------------------------------------------------------

        public void CreatePage(bool newPage)
        {
            Control.Model.CreateNewPage(newPage ? null : CopiedPage.CreateCopy());
            AddPage();
            tabControl1.SelectedIndex = Control.Model.CurrentPage;
            tabControl1.Refresh();
            buttonDeletePage.Enabled = true;
        }

        // -------------------------------------------------------------------
        // HightlightAllChildren
        // -------------------------------------------------------------------

        public void HightlightAllChildren(TreeNode node, Color backColor, Color foreColor)
        {
            node.BackColor = backColor;
            node.ForeColor = foreColor;

            foreach (TreeNode child in node.Nodes)
            {
                HightlightAllChildren(child, backColor, foreColor);
            }
        }

        // -------------------------------------------------------------------
        // UnLightAllChildren
        // -------------------------------------------------------------------

        public void UnLightAll()
        {
            foreach (TreeNode node in CommandsView.Nodes)
            {
                HightlightAllChildren(node, SystemColors.Window, SystemColors.WindowText);
            }
        }

        // -------------------------------------------------------------------
        // AddCommandNodes
        // -------------------------------------------------------------------

        public void AddCommandNodes(NTree<EventCommand> node, TreeNodeCollection commandNode)
        {
            foreach (NTree<EventCommand> child in node.Children)
            {
                TreeNode commandChild = commandNode.Add(WANOK.ListBeginning + child.Data.ToString());
                commandChild.Tag = child;
                AddCommandNodes(child, commandChild.Nodes);
            }
        }

        // -------------------------------------------------------------------
        // StopUnderscoreTimer
        // -------------------------------------------------------------------

        public void StopUnderscoreTimer()
        {
            CommandUnderscoreTimer.Stop();
            if (IsUnderScoreDisplayed) CommandsView.SelectedNode.Text = WANOK.ListBeginning + CurrentMethodString;
            IsUnderScoreDisplayed = false;
        }

        // -------------------------------------------------------------------
        // GenerateListCommandsPanel
        // -------------------------------------------------------------------

        public void GenerateListCommandsPanel()
        {
            // IA sorting list
            List<EventCommandKind> list = EventCommandKind.GetValues();
            list.Sort((a, b) => string.Compare(a.ToString(), b.ToString(), true));
            List<EventCommandKind> sortedList = new List<EventCommandKind>();
            foreach (EventCommandKind ev in list)
            {
                string command = ev.ToString().ToLower();
                string currentCommand = CurrentMethodString.ToLower();
                string[] split1 = command.Split(' ');
                string[] split2 = currentCommand.Split(' ');
                if (currentCommand == "") split2 = new string[] { "" };
                List<string> grp1 = split1.ToList();
                List<string> grp2 = split2.ToList();

                foreach (string w1 in split1)
                {
                    foreach (string w2 in split2)
                    {
                        if (w1.Contains(w2)) grp2.Remove(w2);
                    }
                }

                if (grp2.Count == 0) sortedList.Add(ev);
            }

            // Generating panel
            TableLayoutListCommandsPanel.Controls.Clear();
            TableLayoutListCommandsPanel.ColumnStyles.Clear();
            TableLayoutListCommandsPanel.RowStyles.Clear();
            TableLayoutListCommandsPanel.ColumnCount = 1;
            TableLayoutListCommandsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            foreach (EventCommandKind value in sortedList)
            {
                if (value != EventCommandKind.None)
                {
                    Label label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.Text = value.ToString();
                    label.AutoSize = true;
                    label.Margin = new Padding(0);
                    label.Padding = new Padding(0);
                    label.BackColor = Color.Transparent;
                    label.Tag = value;
                    label.TextAlign = ContentAlignment.MiddleLeft;
                    label.MouseEnter += TableLayoutListCommandsPanel_MouseEnter;
                    label.MouseClick += TableLayoutListCommandsPanel_MouseDown;
                    TableLayoutListCommandsPanel.Controls.Add(label, TableLayoutListCommandsPanel.RowStyles.Count, 0);
                    TableLayoutListCommandsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
                }
            }
            if (TableLayoutListCommandsPanel.Controls.Count > 0)
            {
                ListCommandsSelectedLabel = (Label)TableLayoutListCommandsPanel.Controls[0];
                ListCommandsSelectedLabel.Font = new Font(ListCommandsSelectedLabel.Font, FontStyle.Bold);
            }
            else
            {
                ListCommandsSelectedLabel = null;
            }
            TableLayoutListCommandsPanel.RowCount = TableLayoutListCommandsPanel.RowStyles.Count;
            TableLayoutListCommandsPanel.Size = new Size(0, 0);
        }

        // -------------------------------------------------------------------
        // OpenLabelDialogCommand
        // -------------------------------------------------------------------

        public void OpenLabelDialogCommand()
        {
            if (ListCommandsSelectedLabel != null)
            {
                Form dialog = ((EventCommandKind)ListCommandsSelectedLabel.Tag).GetDialog();
                if (dialog == null) WANOK.ShowActionMessage();
                else
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
            }
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control.Model.CurrentPage = tabControl1.SelectedIndex;
            UpdatePage(tabControl1.SelectedIndex);
        }

        private void graphicControl1_ClosingDialogEvent(SystemGraphic graphic)
        {
            graphicControl1.panel.Frame = 0;
            Control.Model.Pages[tabControl1.SelectedIndex].Graphic = graphic;
        }

        private void graphicFrameTimer_Elapsed(object sender, EventArgs e)
        {
            if (checkBoxMoveAnimation.Checked)
            {
                int maxFrames = (int)graphicControl1.panel.Graphic.Options[(int)SystemGraphic.OptionsEnum.Frames];
                graphicControl1.panel.Frame++;
                graphicControl1.panel.Frame = WANOK.Mod(graphicControl1.panel.Frame, maxFrames);
                graphicControl1.panel.Invalidate();
            }
        }

        private void checkBoxMoveAnimation_CheckedChanged(object sender, EventArgs e)
        {
            graphicControl1.panel.Frame = 0;
            graphicControl1.panel.Animated = checkBoxMoveAnimation.Checked;
            graphicControl1.panel.Invalidate();
        }

        private void numericUpDownSpeed_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownFrequency_ValueChanged(object sender, EventArgs e)
        {
            int time = (int)(150 / (float)numericUpDownFrequency.Value);
            if (time == 0) time = 1;
            GraphicFrameTimer.Interval = time;
        }

        private void DialogEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WANOK.GetGeneralDrawType(Control.Model.Pages[tabControl1.SelectedIndex].GraphicDrawType) != WANOK.GetGeneralDrawType((DrawType)graphicControl1.GetComboBox().SelectedIndex))
            {
                graphicControl1.ResetGraphics();
            }
            Control.Model.Pages[tabControl1.SelectedIndex].GraphicDrawType = (DrawType)graphicControl1.GetComboBox().SelectedIndex;
        }

        private void radioTrigger_Clicked(object sender, EventArgs e)
        {
            RadioButtonWithGroup rb = (sender as RadioButtonWithGroup);

            if (!rb.Checked)
            {
                foreach (EventTrigger trigger in Enum.GetValues(typeof(EventTrigger)))
                {
                    if (RadiosTrigger[trigger].GroupName == rb.GroupName)
                    {
                        RadiosTrigger[trigger].Checked = false;
                    }
                }
                rb.Checked = true;
            }

            UpdateTrigger(RadiosTrigger.FirstOrDefault(x => x.Value == (RadioButton)sender).Key);
        }

        private void buttonNewPage_Click(object sender, EventArgs e)
        {
            CreatePage(true);
        }

        private void buttonCopyPage_Click(object sender, EventArgs e)
        {
            CopiedPage = Control.Model.Pages[Control.Model.CurrentPage].CreateCopy();
            buttonPastPage.Enabled = true;
        }

        private void buttonPastPage_Click(object sender, EventArgs e)
        {
            CreatePage(false);
        }

        private void buttonDeletePage_Click(object sender, EventArgs e)
        {
            for (int i = Control.Model.CurrentPage + 1; i < tabControl1.TabPages.Count; i++)
            {
                tabControl1.TabPages[i].Text = i.ToString();
            }
            Control.Model.Pages.RemoveAt(Control.Model.CurrentPage);
            tabControl1.TabPages.RemoveAt(Control.Model.CurrentPage);
            Control.Model.CurrentPage = 0;

            tabControl1.Refresh();
            buttonDeletePage.Enabled = tabControl1.TabPages.Count > 1;
        }

        private void CommandsView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UnLightAll();
            HightlightAllChildren(CommandsView.SelectedNode, SystemColors.Highlight, SystemColors.HighlightText);

            CurrentMethodString = ((NTree<EventCommand>)CommandsView.SelectedNode.Tag).Data.ToString();
            if (((NTree<EventCommand>)CommandsView.SelectedNode.Tag).Data.EventCommandKind == EventCommandKind.None)
            {
                CommandUnderscoreTimer.Start();
                GenerateListCommandsPanel();
                TableLayoutListCommandsPanel.Location = new Point(CommandsView.SelectedNode.Bounds.X + 30, CommandsView.SelectedNode.Bounds.Y + CommandsView.SelectedNode.Bounds.Height);
            }
            else
            {
                StopUnderscoreTimer();
                TableLayoutListCommandsPanel.Hide();
            }
        }

        private void CommandsView_LostFocus(object sender, EventArgs e)
        {
            StopUnderscoreTimer();
            TableLayoutListCommandsPanel.Hide();
            UnLightAll();
            CommandsView.SelectedNode = null;
        }

        private void CommandsView_KeyDown(object sender, KeyEventArgs e)
        {
            if (((NTree<EventCommand>)CommandsView.SelectedNode.Tag).Data.EventCommandKind == EventCommandKind.None)
            {
                // Setting the text written
                int k = (int)e.KeyCode;
                if (k == 13) OpenLabelDialogCommand();
                else
                {
                    if (k >= 65 && k <= 90) CurrentMethodString += e.KeyCode.ToString();
                    else if (k == 32) CurrentMethodString += " ";
                    else if (k == 8 && CurrentMethodString.Length > 0) CurrentMethodString = CurrentMethodString.Substring(0, CurrentMethodString.Length - 1);
                    if (CurrentMethodString.Length > 0)
                    {
                        CurrentMethodString = CurrentMethodString.ToLower();
                        CurrentMethodString = CurrentMethodString.First().ToString().ToUpper() + CurrentMethodString.Substring(1);
                    }
                    CommandsView.SelectedNode.Text = WANOK.ListBeginning + CurrentMethodString + (IsUnderScoreDisplayed ? "_" : "");

                    // Displaying IA research 
                    GenerateListCommandsPanel();
                    TableLayoutListCommandsPanel.Show();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void CommandUnderscoreTimer_Tick(object sender, EventArgs e)
        {
            if (!IsUnderScoreDisplayed) CommandsView.SelectedNode.Text = WANOK.ListBeginning + CurrentMethodString + "_";
            else CommandsView.SelectedNode.Text = CommandsView.SelectedNode.Text = WANOK.ListBeginning + CurrentMethodString;
            IsUnderScoreDisplayed = !IsUnderScoreDisplayed;
        }

        private void TableLayoutListCommandsPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if ((e.Column + e.Row) % 2 == 0) e.Graphics.FillRectangle(Brushes.LightGray, e.CellBounds);
            else e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.CellBounds);
        }

        private void TableLayoutListCommandsPanel_MouseDown(object sender, MouseEventArgs e)
        {
            OpenLabelDialogCommand();
        }

        private void TableLayoutListCommandsPanel_MouseEnter(object sender, EventArgs e)
        {
            ListCommandsSelectedLabel.Font = new Font(ListCommandsSelectedLabel.Font, FontStyle.Regular);
            ListCommandsSelectedLabel = (Label)sender;
            ListCommandsSelectedLabel.Font = new Font(ListCommandsSelectedLabel.Font, FontStyle.Bold);
        }

        private void CommandsView_DoubleClick(object sender, EventArgs e)
        {
            DialogCommands dialog = new DialogCommands();
            if (dialog.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
