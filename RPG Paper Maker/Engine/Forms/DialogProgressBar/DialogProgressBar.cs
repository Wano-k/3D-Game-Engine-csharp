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
    public partial class DialogProgressBar : Form
    {
        public DialogProgressBar(string text)
        {
            InitializeComponent();

            label1.Text = text;
            progressBar.Value = 0;
        }

        public void SetValue(string text, int value)
        {
            label1.Text = text;
            label1.Update();
            label1.Refresh();
            SetValue(value);
        }

        public void SetValue(int value)
        {
            progressBar.Value = value;
            progressBar.Update();
            progressBar.Refresh();
            Refresh();
        }

        public void Stop()
        {
            label1.Text = "";
            progressBar.Value = 100;
            WANOK.KeyboardManager.InitializeKeyboard();
        }
    }
}
