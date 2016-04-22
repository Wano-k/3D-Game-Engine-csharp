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

namespace RPG_Paper_Maker
{
    public partial class DialogNewProject : Form
    {
        public string ProjectName;
        public string DirPath;

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
            if (Directory.Exists(this.TextCtrlLocation.Text))
            {
                if (this.TextCtrlProjectName.Text.Contains("/") || this.TextCtrlProjectName.Text.Contains("\\")  || this.TextCtrlProjectName.Text.Contains(":") || this.TextCtrlProjectName.Text.Contains("*") || this.TextCtrlProjectName.Text.Contains("?") || this.TextCtrlProjectName.Text.Contains("<") || this.TextCtrlProjectName.Text.Contains(">") || this.TextCtrlProjectName.Text.Contains("\"") || this.TextCtrlProjectName.Text.Contains("|") || this.TextCtrlProjectName.Text.Trim().Equals("") || this.TextCtrlProjectName.Text.Replace('.', ' ').Trim().Equals("") || this.TextCtrlProjectName.Text.Contains("..") || this.TextCtrlProjectName.Text.Trim()[this.TextCtrlProjectName.Text.Trim().Length-1] == '.')
                {
                    MessageBox.Show("Could not create a directory with that name. Do not use / \\ : ? * | < > \". You can't name with an empty field, or \".\" or \"..\" field.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string dirPath = this.TextCtrlLocation.Text + "\\" + this.TextCtrlProjectName.Text;
                    if (!Directory.Exists(dirPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(dirPath);
                            try
                            {
                                WANOK.CopyAll(new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\Basic"), new DirectoryInfo(dirPath));
                                this.ProjectName = this.TextCtrlProjectName.Text.Trim();
                                this.DirPath = this.TextCtrlLocation.Text + "\\" + this.ProjectName;

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Could not generate the project. See if you have \"Basic\" folder in the main folder.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Could not create the directory. Report it to the dev.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This project already exist in this folder. Please change your project name or your folder.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
            if (Directory.Exists(this.TextCtrlLocation.Text))
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
