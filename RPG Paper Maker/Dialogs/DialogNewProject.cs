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
    public partial class DialogNewProject : Form
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogNewProject()
        {
            InitializeComponent();
            this.TextCtrlLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\RPG Paper Maker Games";
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(this.TextCtrlLocation.Text))
            {
                string dirPath = this.TextCtrlLocation.Text + "\\" + this.TextCtrlProjectName.Text;
                if (!System.IO.Directory.Exists(dirPath))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(dirPath);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Invalid project name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("This project already exist in this folder. Please change your project name or your folder.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("The directory path is incorrect.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------------------
        // ButtonSearch_Click
        // -------------------------------------------------------------------

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (System.IO.Directory.Exists(this.TextCtrlLocation.Text))
            {
                folderDialog.SelectedPath = this.TextCtrlLocation.Text;
            }
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                this.TextCtrlLocation.Text = folderDialog.SelectedPath;
            }
        }
    }
}
