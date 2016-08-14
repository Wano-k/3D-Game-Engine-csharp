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
            numericUpDownSpeed.DecimalPlaces = 1;
            numericUpDownSpeed.Minimum = (decimal)0.1;
            numericUpDownSpeed.Maximum = (decimal)999.0;
            numericUpDownSpeed.Increment = (decimal)0.1;
            numericUpDownSpeed.Value = (decimal)1.0;
            numericUpDownFrequency.DecimalPlaces = 1;
            numericUpDownFrequency.Minimum = (decimal)0.1;
            numericUpDownFrequency.Maximum = (decimal)999.0;
            numericUpDownFrequency.Increment = (decimal)0.1;
            numericUpDownFrequency.Value = (decimal)1.0;

            textBoxEventName.Select();
            textBoxEventName.Focus();

            // Events
            graphicControl1.ClosingDialogEvent += graphicControl1_ClosingDialogEvent;

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
        // UpdatePage
        // -------------------------------------------------------------------

        public void UpdatePage(int i)
        {
            graphicControl1.InitializeListParameters(Control.Model.Pages[i].Graphic);
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
            Control.Model.Pages[tabControl1.SelectedIndex].Graphic = graphic;
        }
    }
}
