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
        List<SuperListItemNameWithoutLang> Switches;
        int Id;

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

        public DialogSwitches()
        {
            InitializeComponent();
            Switches = new List<SuperListItemNameWithoutLang>();
            for (int i = 0; i < WANOK.Game.System.Switches.Count; i++)
            {
                Switches.Add((SuperListItemNameWithoutLang)WANOK.Game.System.Switches[i].CreateCopy());
            }

            superListBox1.InitializeListParameters(true, Switches.Cast<SuperListItem>().ToList(), null, typeof(SuperListItemNameWithoutLang), 1, SystemDatas.MAX_SWITCHES);
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

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            WANOK.Game.System.Switches = Switches;
            WANOK.SaveBinaryDatas(WANOK.Game.System, WANOK.SystemPath);
            Close();
        }
    }
}
