using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPG_Paper_Maker.Controls;

namespace RPG_Paper_Maker
{
    public partial class DialogNewProject : Form
    {
        protected DialogNewProjectControl Control = new DialogNewProjectControl();
        protected BindingSource ViewModelBindingSource = new BindingSource();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogNewProject()
        {
            InitializeComponent();

            ViewModelBindingSource.DataSource = Control;
            InitializeDataBindings();
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            TextCtrlProjectName.DataBindings.Add("Text", this.ViewModelBindingSource, "ProjectName", true);
            TextCtrlLocation.DataBindings.Add("Text", this.ViewModelBindingSource, "DirPath", true);
        }

        // -------------------------------------------------------------------
        // Get
        // -------------------------------------------------------------------

        public string GetProjectName()
        {
            return Control.ProjectName;
        }

        public string GetDirPath()
        {
            return Control.DirPath;
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            string message = Control.CreateProject();

            if (message != null)
            {
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        // -------------------------------------------------------------------
        // ButtonSearch_Click
        // -------------------------------------------------------------------

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (Directory.Exists(TextCtrlLocation.Text))
            {
                folderDialog.SelectedPath = TextCtrlLocation.Text;
            }
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                Control.LoadDirectoryLocation(folderDialog.SelectedPath);
            }
        }
    }
}
