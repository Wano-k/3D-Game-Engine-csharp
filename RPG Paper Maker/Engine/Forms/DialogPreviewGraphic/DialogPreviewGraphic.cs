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
        protected SelectionPictureBox PictureBox = new SelectionPictureBox();
        protected TilesetSelectorPicture TilesetPictureBox = new TilesetSelectorPicture();
        protected SystemGraphic GraphicTileset;
        bool IsUsingCursorSelector = false;

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

        public DialogPreviewGraphic(SystemGraphic graphic, OptionsKind optionsKind, SystemGraphic graphicTileset = null)
        {
            InitializeComponent();

            GraphicTileset = graphicTileset;

            // Control
            Control = new DialogPreviewGraphicControl(graphic.CreateCopy());

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
            PictureBox.BackColor = WANOK.COLOR_BACKGROUND_PREVIEW_IMAGE;
            //panelPicture.Controls.Add(PictureBox);
            TilesetPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            TilesetPictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            TilesetPictureBox.LoadTexture(graphicTileset == null ? new SystemGraphic(GraphicKind.Tileset) : graphicTileset, WANOK.RELATION_SIZE);
            TilesetPictureBox.BackColor = WANOK.COLOR_BACKGROUND_PREVIEW_IMAGE;
            if (graphic.IsTileset()) TilesetPictureBox.SetCurrentTexture((int)graphic.Options[0] * WANOK.BASIC_SQUARE_SIZE, (int)graphic.Options[1] * WANOK.BASIC_SQUARE_SIZE, (int)graphic.Options[2], (int)graphic.Options[3]);
            else TilesetPictureBox.SetCurrentTexture(0, 0, 1, 1);

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
            TilesetPictureBox.MouseDown += new MouseEventHandler(TilesetSelectorPicture_MouseDown);
            TilesetPictureBox.MouseMove += new MouseEventHandler(TilesetSelectorPicture_MouseMove);
            TilesetPictureBox.MouseUp += new MouseEventHandler(TilesetSelectorPicture_MouseUp);
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
            listView1.Items.Cast<ListViewItem>().ToList().ForEach(item =>
            {
                item.BackColor = SystemColors.Window;
                item.ForeColor = SystemColors.WindowText;
            });
            listView1.SelectedItems.Cast<ListViewItem>().ToList().ForEach(item =>
            {
                item.BackColor = SystemColors.Highlight;
                item.ForeColor = SystemColors.HighlightText;
            });
            if (listView1.SelectedItems.Count == 1 && listView1.SelectedIndices[0] == 0) PictureBox.Hide();
            else
            {
                if (listView1.SelectedItems.Count == 0) PictureBox.Hide();
                else PictureBox.Show();
            }

            // Set the image selected
            if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Text != WANOK.NONE_IMAGE_STRING && listView1.SelectedItems[0].Text != WANOK.TILESET_IMAGE_STRING)
            {
                panelPicture.Controls.Clear();
                panelPicture.Controls.Add(PictureBox);
                PictureBox.LoadTexture(new SystemGraphic(listView1.SelectedItems[0].Text, listView1.SelectedItems[0].ImageIndex == 1, Control.Model.GraphicKind), 1.0f);
                Control.SetImageDatas(listView1.SelectedItems[0].Text, listView1.SelectedItems[0].ImageIndex == 1);
                groupBox1.Show();
                tableLayoutPanel3.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 20);
                tableLayoutPanel3.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 60);
                tableLayoutPanel3.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 20);
            }
            else
            {
                if (listView1.SelectedItems[0].Text == WANOK.NONE_IMAGE_STRING)
                {
                    panelPicture.Controls.Clear();
                    panelPicture.Controls.Add(PictureBox);
                    PictureBox.LoadTexture(new SystemGraphic(Control.Model.GraphicKind), 1.0f);
                    Control.SetImageDatas(WANOK.NONE_IMAGE_STRING, true);
                }
                else if (listView1.SelectedItems[0].Text == WANOK.TILESET_IMAGE_STRING)
                {
                    panelPicture.Controls.Clear();
                    panelPicture.Controls.Add(TilesetPictureBox);
                    Control.SetImageDatas(WANOK.TILESET_IMAGE_STRING, true);
                    TilesetPictureBox.SetCurrentTexture((int)Control.Model.Options[(int)SystemGraphic.OptionsEnum.TilesetX] * WANOK.BASIC_SQUARE_SIZE, (int)Control.Model.Options[(int)SystemGraphic.OptionsEnum.TilesetY] * WANOK.BASIC_SQUARE_SIZE, (int)Control.Model.Options[(int)SystemGraphic.OptionsEnum.TilesetWidth], (int)Control.Model.Options[(int)SystemGraphic.OptionsEnum.TilesetHeight]);
                }
                groupBox1.Hide();
                tableLayoutPanel3.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 20);
                tableLayoutPanel3.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 80);
                tableLayoutPanel3.ColumnStyles[2] = new ColumnStyle(SizeType.AutoSize);
            }

            // Set zoom value to normal value
            CurrentValue = 0;
            trackBarZoom.Value = 0;
            Zoom = 1.0f;
            PictureBox.Zoom(Zoom);
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

        private void TilesetSelectorPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsUsingCursorSelector = true;
                TilesetPictureBox.MakeFirstRectangleSelection(e.X, e.Y);
                TilesetPictureBox.Refresh();
            }
        }

        private void TilesetSelectorPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsUsingCursorSelector)
            {
                TilesetPictureBox.MakeRectangleSelection(e.X, e.Y);
                TilesetPictureBox.Refresh();
            }
        }

        private void TilesetSelectorPicture_MouseUp(object sender, MouseEventArgs e)
        {
            TilesetPictureBox.SetCursorRealPosition();
            IsUsingCursorSelector = false;
            TilesetPictureBox.Refresh();
            int[] texture = TilesetPictureBox.GetCurrentTexture();
            Control.Model.Options[(int)SystemGraphic.OptionsEnum.TilesetX] = texture[(int)SystemGraphic.OptionsEnum.TilesetX];
            Control.Model.Options[(int)SystemGraphic.OptionsEnum.TilesetY] = texture[(int)SystemGraphic.OptionsEnum.TilesetY];
            Control.Model.Options[(int)SystemGraphic.OptionsEnum.TilesetWidth] = texture[(int)SystemGraphic.OptionsEnum.TilesetWidth];
            Control.Model.Options[(int)SystemGraphic.OptionsEnum.TilesetHeight] = texture[(int)SystemGraphic.OptionsEnum.TilesetHeight];
        }
    }
}
