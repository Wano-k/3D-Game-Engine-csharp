using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class DialogEvent : Form
    {
        protected DialogEventControl Control;
        protected BindingSource ViewModelBindingSource = new BindingSource();
        System.Timers.Timer GraphicFrameTimer = new System.Timers.Timer();
        protected Dictionary<EventTrigger, RadioButtonWithGroup> RadiosTrigger = new Dictionary<EventTrigger, RadioButtonWithGroup>();

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

            GraphicFrameTimer.Interval = 150;
            GraphicFrameTimer.Start();

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
            tabControl1.TabPages[0].Controls.Clear();
            tabControl1.TabPages[0].Controls.Add(tableLayoutMainPage);

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

            textBoxEventName.Select();
            textBoxEventName.Focus();

            // Events
            graphicControl1.ClosingDialogEvent += graphicControl1_ClosingDialogEvent;
            GraphicFrameTimer.Elapsed += graphicFrameTimer_Elapsed;
            graphicControl1.GetComboBox().SelectedIndexChanged += DialogEvent_SelectedIndexChanged;


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
            conditionsPanel1.InitializeListParameters();
            graphicControl1.InitializeListParameters(Control.Model.Pages[i].Graphic, MapEditor.GetMapTileset().Graphic);
            graphicControl1.GetComboBox().SelectedIndex = (int)Control.Model.Pages[i].GraphicDrawType;
            RadiosTrigger[Control.Model.Pages[i].Trigger].Checked = true;

            // Move page
            tabControl1.TabPages[i].Controls.Clear();
            tabControl1.TabPages[i].Controls.Add(tableLayoutMainPage);
        }

        // -------------------------------------------------------------------
        // UpdateTrigger
        // -------------------------------------------------------------------

        public void UpdateTrigger(EventTrigger trigger)
        {
            Control.Model.Pages[Control.Model.CurrentPage].Trigger = trigger;
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
    }
}
