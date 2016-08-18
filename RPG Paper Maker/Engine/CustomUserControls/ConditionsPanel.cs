using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class ConditionsPanel : UserControl
    {


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public ConditionsPanel()
        {
            InitializeComponent();

            flatButton1.Click += FlatButton1_Click;
        }

        // -------------------------------------------------------------------
        // InitializeListParameters
        // -------------------------------------------------------------------

        public void InitializeListParameters()
        {
            
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void FlatButton1_Click(object sender, EventArgs e)
        {
            DialogCondition dialog = new DialogCondition(Condition.DefaultObjects());
            if (dialog.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
