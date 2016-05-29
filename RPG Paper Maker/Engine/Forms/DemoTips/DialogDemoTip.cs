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
    public partial class DialogDemoTip : Form
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogDemoTip()
        {
            InitializeComponent();

            this.CheckBoxShow.Checked = !WANOK.Settings.showDemoTip;
        }

        // -------------------------------------------------------------------
        // ButtonOk_Click
        // -------------------------------------------------------------------

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            SetOptions();
            this.Close();
        }

        // -------------------------------------------------------------------
        // ButtonLater_Click
        // -------------------------------------------------------------------

        private void ButtonLater_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            SetOptions();
            this.Close();
        }

        // -------------------------------------------------------------------
        // SetOptions
        // -------------------------------------------------------------------

        private void SetOptions()
        {
            WANOK.Settings.showDemoTip = !this.CheckBoxShow.Checked;
            WANOK.SaveDatas(WANOK.Settings, WANOK.PATHSETTINGS);
        }
    }
}
