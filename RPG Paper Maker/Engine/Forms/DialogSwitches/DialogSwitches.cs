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
    // DialogVariable
    public partial class DialogSwitches : DialogVariable
    {
        int Id;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogSwitches()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------------------
        // GetObject
        // -------------------------------------------------------------------

        public override object[] GetObject()
        {
            return new object[] { Id };
        }

        // -------------------------------------------------------------------
        // InitializeParameters
        // -------------------------------------------------------------------

        public override void InitializeParameters(object[] value, List<object> others)
        {
            Id = (int)value[0];
        }
    }
}
