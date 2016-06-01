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
    public partial class DialogInputsSelect : Form
    {
        public Microsoft.Xna.Framework.Input.Keys Key { get; set; }
        public DialogInputsSelect(string label)
        {
            InitializeComponent();

            Text = label;
        }

        private void DialogInputsSelect_KeyDown(object sender, KeyEventArgs e)
        {
            Key = (Microsoft.Xna.Framework.Input.Keys)e.KeyCode;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
