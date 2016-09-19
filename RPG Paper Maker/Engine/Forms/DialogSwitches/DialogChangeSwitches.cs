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
    public partial class DialogChangeSwitches : ModelForm
    {
        EventCommandOther Model;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogChangeSwitches(EventCommandOther model)
        {
            InitializeComponent();
            Model = (EventCommandOther)model.CreateCopy();
            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;
            groupBox2.Paint += MainForm.PaintBorderGroupBox;

            // Updating infos
            textBoxVariables1.InitializeSwitch((int)Model.Command[1]);
        }

        // -------------------------------------------------------------------
        // GetModel
        // -------------------------------------------------------------------

        public override EventCommand GetModel()
        {
            return Model;
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
