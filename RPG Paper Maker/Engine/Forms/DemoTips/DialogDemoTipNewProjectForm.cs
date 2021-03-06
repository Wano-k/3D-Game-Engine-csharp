﻿using System;
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
    public partial class DialogDemoTipNewProjectForm : Form
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogDemoTipNewProjectForm()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------------------
        // ButtonCancel_Click
        // -------------------------------------------------------------------

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            ((MainForm)Application.OpenForms[0]).CancelDemo();
            this.Close();
        }
    }
}
