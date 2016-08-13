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

        public DialogEvent(SystemEvent ev = null)
        {
            InitializeComponent();

            Control = new DialogEventControl(ev == null ? new SystemEvent() : ev);
            ViewModelBindingSource.DataSource = Control;

            apparenceControl1.InitializeListParameters(Control.Model.GraphicApparence.CreateCopy());

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
        }
    }
}
