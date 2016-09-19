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
    public partial class DialogConditions : ModelForm
    {
        private EventCommandConditions Model;

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
        // Constructors
        // -------------------------------------------------------------------

        public DialogConditions(EventCommandConditions conditions)
        {
            InitializeComponent();

            Model = (EventCommandConditions)conditions.CreateCopy();

            conditionsPanel.InitializeListParameters(Model.Tree);
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
