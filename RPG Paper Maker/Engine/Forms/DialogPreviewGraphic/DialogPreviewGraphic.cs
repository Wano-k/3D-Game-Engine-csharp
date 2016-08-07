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
        public int ZoomTime = 5;
        public int CurrentValue = 0;
        public float Zoom = 1.0f;
        protected DialogPreviewGraphicControl Control;
        protected InterpolationPictureBox PictureBox = new InterpolationPictureBox();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogPreviewGraphic(SystemGraphic graphic, object[] options)
        {
            InitializeComponent();

            // Control
            Control = new DialogPreviewGraphicControl(graphic);

            Text = graphic.GraphicKind.ToString() + " graphic preview";

            // list
            listView1.Select();
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            ColumnHeader header = new ColumnHeader();
            header.Text = "";
            header.Name = "";
            listView1.Columns.Add(header);
            listView1.Columns[0].Width = listView1.Size.Width - 4;
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

            // Picture
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            panelPicture.Controls.Add(PictureBox);

            // Zoom
            trackBarZoom.Minimum = -ZoomTime;
            trackBarZoom.Maximum = ZoomTime;

            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;

            // Events
            AddEvent();
        }

        // -------------------------------------------------------------------
        // AddEvent
        // -------------------------------------------------------------------

        public void AddEvent()
        {
            PictureBox.MouseEnter += PictureBox_MouseEnter;
            PictureBox.PreviewKeyDown += PictureBox_PreviewKeyDown;
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
        // Events
        // -------------------------------------------------------------------


        private void listView1_MouseEnter(object sender, EventArgs e)
        {
            listView1.Focus();
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox.Focus();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set background color of selected item
            this.listView1.Items.Cast<ListViewItem>()
        .ToList().ForEach(item =>
        {
            item.BackColor = SystemColors.Window;
            item.ForeColor = SystemColors.WindowText;
        });
            this.listView1.SelectedItems.Cast<ListViewItem>()
                .ToList().ForEach(item =>
                {
                    item.BackColor = SystemColors.Highlight;
                    item.ForeColor = SystemColors.HighlightText;
                });

            // Set the image selected
            if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Text != WANOK.NONE_IMAGE_STRING)
            {
                PictureBox.LoadTexture(new SystemGraphic(listView1.SelectedItems[0].Text, listView1.SelectedItems[0].ImageIndex == 1, Control.Model.GraphicKind), 1.0f);
                Control.SetImageDatas(listView1.SelectedItems[0].Text, listView1.SelectedItems[0].ImageIndex == 1);
            }
            else
            {
                PictureBox.LoadTexture(new SystemGraphic(Control.Model.GraphicKind), 1.0f);
                Control.SetImageDatas(WANOK.NONE_IMAGE_STRING, true);
            }

            // Set zoom value to normal value
            CurrentValue = 0;
            trackBarZoom.Value = 0;
            Zoom = 1.0f;
        }

        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            int offset = trackBarZoom.Value - CurrentValue;
            CurrentValue = trackBarZoom.Value;

            if (offset > 0) Zoom *= (float)Math.Pow(2, offset);
            else Zoom /= (float)Math.Pow(2, -offset);

            PictureBox.Zoom(Zoom);
            PictureBox.Focus();
        }

        private void PictureBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                int index = listView1.SelectedItems[0].Index;
                if (e.KeyCode == Keys.Up && index > 0)
                {
                    listView1.Items[index - 1].Selected = true;
                }
                if (e.KeyCode == Keys.Down && index < listView1.Items.Count - 1)
                {
                    listView1.Items[index + 1].Selected = true;
                }
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
