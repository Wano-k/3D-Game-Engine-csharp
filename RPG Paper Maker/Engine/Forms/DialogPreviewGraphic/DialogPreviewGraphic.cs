using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPG_Paper_Maker.Controls;
using System.IO;

namespace RPG_Paper_Maker
{
    public partial class DialogPreviewGraphic : Form
    {
        protected DialogPreviewGraphicControl Control;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogPreviewGraphic(SystemGraphic graphic)
        {
            InitializeComponent();

            // Control
            Control = new DialogPreviewGraphicControl(graphic);

            Text = graphic.GraphicKind.ToString() + " graphic preview";

            // list
            listView1.Select();
            listView1.HideSelection = false;
            listView1.Items[0].Selected = true;

            List<string> LocalFiles = Control.GetLocalFiles();
            List<string> RTPFiles = Control.GetRTPFiles();

            for (int i = 0; i < LocalFiles.Count; i++)
            {
                listView1.Items.Add(Path.GetFileName(LocalFiles[i]), 0);
                if (!graphic.IsNone() && graphic.GraphicName == listView1.Items[i+1].Text && !graphic.IsRTP) listView1.Items[i+1].Selected = true;
            }
            for (int i = LocalFiles.Count; i < LocalFiles.Count + RTPFiles.Count; i++)
            {
                listView1.Items.Add(Path.GetFileName(RTPFiles[i - LocalFiles.Count]), 1);
                if (!graphic.IsNone() && graphic.GraphicName == listView1.Items[i+1].Text && graphic.IsRTP) listView1.Items[i+1].Selected = true;
            }

            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;

            // Options
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 80);
        }

        // -------------------------------------------------------------------
        // GetGraphic
        // -------------------------------------------------------------------

        public SystemGraphic GetGraphic()
        {
            return Control.Model;
        }

        // -------------------------------------------------------------------
        // GetOptionsBox
        // -------------------------------------------------------------------

        public GroupBox GetOptionsBox()
        {
            return groupBox1;
        }

        // -------------------------------------------------------------------
        // listView1_MouseEnter
        // -------------------------------------------------------------------

        private void listView1_MouseEnter(object sender, EventArgs e)
        {
            listView1.Focus();
        }

        // -------------------------------------------------------------------
        // listView1_SelectedIndexChanged
        // -------------------------------------------------------------------

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Text != WANOK.NONE_IMAGE_STRING)
            {
                string path = listView1.SelectedItems[0].ImageIndex == 0 ? Control.Model.GetLocalPath(listView1.SelectedItems[0].Text) : Control.Model.GetRTPPath(listView1.SelectedItems[0].Text);
                Image image;
                try
                {
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        image = Image.FromStream(stream);
                    }
                    pictureBox.Image = image;
                    pictureBox.Size = new Size(image.Width, image.Height);
                }
                catch
                {
                    pictureBox.Image = null;
                }
                Control.SetImageDatas(listView1.SelectedItems[0].Text, listView1.SelectedItems[0].ImageIndex == 1);
            }
            else
            {
                pictureBox.Image = null;
                Control.SetImageDatas(WANOK.NONE_IMAGE_STRING, true);
            }
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
