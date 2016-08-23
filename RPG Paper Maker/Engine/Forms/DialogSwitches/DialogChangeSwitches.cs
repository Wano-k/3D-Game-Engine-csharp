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
    public partial class DialogChangeSwitches : Form
    {

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogChangeSwitches(EventCommandOther model)
        {
            InitializeComponent();

            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;
            groupBox2.Paint += MainForm.PaintBorderGroupBox;

            // Updating infos
            textBoxVariables1.InitializeSwitch((int)model.Command[1]);
        }
    }
}
