using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class DialogPreviewGraphicSelectFrame : DialogPreviewGraphic
    {
        public OptionsKind OptionsKind;
        public NumericUpDown NumericFrames = new NumericUpDown();
        public ComboBox ComboBoxDialog = new ComboBox();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogPreviewGraphicSelectFrame(SystemGraphic graphic, OptionsKind optionsKind) : base(graphic, optionsKind) 
        {
            OptionsKind = optionsKind;

            // Picture
            PictureBox = new TilesetSelectorPicture();
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            PictureBox.BackColor = Color.FromArgb(210, 210, 210);
            panelPicture.Controls.Clear();
            panelPicture.Controls.Add(PictureBox);

            // Options
            GrowLabel label = new GrowLabel();
            label.Dock = DockStyle.Fill;
            switch (optionsKind)
            {
                case OptionsKind.CharacterSelection:
                    label.Text = "Select the number of frames and if there are diagonals:";
                    break;
            }
            label.Margin = new Padding(0, 0, 0, 20);
            tableLayoutPanel4.Controls.Add(label, 0, 2);

            TableLayoutPanel panelRectangle = new TableLayoutPanel();
            panelRectangle.Dock = DockStyle.Fill;
            panelRectangle.RowCount = 3;
            panelRectangle.ColumnCount = 2;
            tableLayoutPanel4.Controls.Add(panelRectangle, 0, 3);
            Label rectangleLabel = new Label();
            rectangleLabel.Text = "Frames:";
            rectangleLabel.TextAlign = ContentAlignment.MiddleLeft;
            panelRectangle.Controls.Add(rectangleLabel, 0, 0);
            rectangleLabel = new Label();
            rectangleLabel.Text = "Diagonals:";
            rectangleLabel.TextAlign = ContentAlignment.MiddleLeft;
            panelRectangle.Controls.Add(rectangleLabel, 0, 1);

            NumericFrames.Minimum = 1;
            NumericFrames.Maximum = 999;
            NumericFrames.Value = (int)graphic.Options[0];
            panelRectangle.Controls.Add(NumericFrames, 1, 0);
            ComboBoxDialog.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxDialog.Items.Add("No");
            ComboBoxDialog.Items.Add("Yes");
            ComboBoxDialog.SelectedIndex = (int)graphic.Options[1];
            panelRectangle.Controls.Add(ComboBoxDialog, 1, 1);

            panelRectangle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 63));
            panelRectangle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panelRectangle.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panelRectangle.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panelRectangle.RowStyles.Add(new RowStyle(SizeType.Percent, 100));


            // Actualize list (delete _act)
            List<ListViewItem> list = new List<ListViewItem>();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                list.Add(listView1.Items[i]);
            }
                
            for (int i = 1; i < list.Count; i++)
            {
                string path = Path.GetFileNameWithoutExtension(list[i].Text);
                if (path.Length - 4 >= 0)
                {
                    if (path.Substring(path.Length - 4, 4) == "_act") listView1.Items.Remove(list[i]);
                }
            }

            // Events
            PictureBox.MouseEnter += PictureBox_MouseEnter;
            PictureBox.MouseDown += PictureBox_MouseDown;
            PictureBox.MouseUp += PictureBox_MouseUp;
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;
            NumericFrames.ValueChanged += NumericFrames_ValueChanged;
            ComboBoxDialog.SelectedIndexChanged += ComboBoxDialog_SelectedIndexChanged;
        }

        
        // -------------------------------------------------------------------
        // UpdateSquareSize
        // -------------------------------------------------------------------

        public void UpdateSquareSize()
        {
            ((TilesetSelectorPicture)PictureBox).SelectionRectangle.SquareWidth = PictureBox.Image.Size.Width / (int)NumericFrames.Value;
            int rows = ComboBoxDialog.SelectedIndex == 0 ? 4 : 8;
            ((TilesetSelectorPicture)PictureBox).SelectionRectangle.SquareHeight = PictureBox.Image.Size.Height / rows;
            ((TilesetSelectorPicture)PictureBox).SelectionRectangle.SetRectangle(0, 0, 1, 1);
            PictureBox.Refresh();
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ((TilesetSelectorPicture)PictureBox).MakeFirstRectangleSelection(e.X, e.Y, ((TilesetSelectorPicture)PictureBox).ZoomPixel);
                PictureBox.Refresh();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            ((TilesetSelectorPicture)PictureBox).SetCursorRealPosition();
            PictureBox.Refresh();
            int[] texture = ((TilesetSelectorPicture)PictureBox).GetCurrentTexture();
            //Texture = new object[] { texture[0], texture[1], texture[2], texture[3] };
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox.Focus();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSquareSize();
        }

        private void ComboBoxDialog_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSquareSize();
        }

        private void NumericFrames_ValueChanged(object sender, EventArgs e)
        {
            UpdateSquareSize();
        }
    }
}
