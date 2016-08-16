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

            InitializeDataBindings();
        }
        

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            textBoxEventName.DataBindings.Add("Text", ViewModelBindingSource, "Name", true);
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
            graphicControl1.InitializeListParameters(Control.Model.Pages[i].Graphic);
            graphicControl1.GetComboBox().SelectedIndex = (int)Control.Model.Pages[i].GraphicDrawType;

            // Move page
            tabControl1.TabPages[i].Controls.Clear();
            tabControl1.TabPages[i].Controls.Add(tableLayoutMainPage);
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
                int maxFrames = (int)graphicControl1.panel.Graphic.Options[0];
                graphicControl1.panel.Frame++;
                graphicControl1.panel.Frame = WANOK.Mod(graphicControl1.panel.Frame, maxFrames);
                graphicControl1.panel.Invalidate();
            }
        }

        private void checkBoxMoveAnimation_CheckedChanged(object sender, EventArgs e)
        {
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
            Control.Model.Pages[tabControl1.SelectedIndex].GraphicDrawType = (DrawType)graphicControl1.GetComboBox().SelectedIndex;
            graphicControl1.ResetGraphics();
        }
    }
}
