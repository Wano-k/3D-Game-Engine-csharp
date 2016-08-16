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
    public partial class DialogPreviewGraphicSelectRectangle : DialogPreviewGraphic
    {
        public NumericUpDown numericButtonX = new NumericUpDown();
        public NumericUpDown numericButtonY = new NumericUpDown();
        public NumericUpDown numericButtonWidth = new NumericUpDown();
        public NumericUpDown numericButtonHeight = new NumericUpDown();
        public bool IsUsingCursorSelector = false;
        public OptionsKind OptionsKind;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogPreviewGraphicSelectRectangle(SystemGraphic graphic, OptionsKind optionsKind, SystemGraphic graphicTileset = null) : base(graphic, optionsKind, graphicTileset)
        {
            OptionsKind = optionsKind;

            // Picture
            PictureBox = new PixelSelectPictureBox();
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            PictureBox.BackColor = WANOK.COLOR_BACKGROUND_PREVIEW_IMAGE;
            panelPicture.Controls.Clear();
            panelPicture.Controls.Add(PictureBox);

            // Options
            GrowLabel label = new GrowLabel();
            label.Dock = DockStyle.Fill;
            switch (optionsKind)
            {
                case OptionsKind.BarSelection:
                    label.Text = "Select the rectangle that represents the filled bar:";
                    break;
            }
            label.Margin = new Padding(0, 0, 0, 20);
            tableLayoutPanel4.Controls.Add(label, 0, 2);

            TableLayoutPanel panelRectangle = new TableLayoutPanel();
            panelRectangle.Dock = DockStyle.Fill;
            panelRectangle.RowCount = 5;
            panelRectangle.ColumnCount = 2;

            tableLayoutPanel4.Controls.Add(panelRectangle, 0, 3);
            Label rectangleLabel = new Label();
            rectangleLabel.Text = "X:";
            rectangleLabel.TextAlign = ContentAlignment.MiddleLeft;
            panelRectangle.Controls.Add(rectangleLabel, 0, 0);
            rectangleLabel = new Label();
            rectangleLabel.Text = "Y:";
            rectangleLabel.TextAlign = ContentAlignment.MiddleLeft;
            panelRectangle.Controls.Add(rectangleLabel, 0, 1);
            rectangleLabel = new Label();
            rectangleLabel.Text = "Width:";
            rectangleLabel.TextAlign = ContentAlignment.MiddleLeft;
            panelRectangle.Controls.Add(rectangleLabel, 0, 2);
            rectangleLabel = new Label();
            rectangleLabel.Text = "Height:";
            rectangleLabel.TextAlign = ContentAlignment.MiddleLeft;
            panelRectangle.Controls.Add(rectangleLabel, 0, 3);

            numericButtonX.Minimum = 0;
            numericButtonY.Minimum = 0;
            numericButtonWidth.Minimum = 1;
            numericButtonHeight.Minimum = 1;
            numericButtonX.Maximum = 32000;
            numericButtonY.Maximum = 32000;
            numericButtonWidth.Maximum = 32000;
            numericButtonHeight.Maximum = 32000;
            numericButtonX.Value = (int)graphic.Options[0];
            numericButtonY.Value = (int)graphic.Options[1];
            numericButtonWidth.Value = (int)graphic.Options[2];
            numericButtonHeight.Value = (int)graphic.Options[3];
            panelRectangle.Controls.Add(numericButtonX, 1, 0);
            panelRectangle.Controls.Add(numericButtonY, 1, 1);
            panelRectangle.Controls.Add(numericButtonWidth, 1, 2);
            panelRectangle.Controls.Add(numericButtonHeight, 1, 3);

            panelRectangle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
            panelRectangle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panelRectangle.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panelRectangle.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panelRectangle.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panelRectangle.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panelRectangle.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // Update image rectangle
            PictureBox.SelectionRectangle.X = (int)numericButtonX.Value;
            PictureBox.SelectionRectangle.Y = (int)numericButtonY.Value;
            PictureBox.SelectionRectangle.Width = (int)numericButtonWidth.Value;
            PictureBox.SelectionRectangle.Height = (int)numericButtonHeight.Value;
            PictureBox.Refresh();

            // Events
            PictureBox.MouseEnter += PictureBox_MouseEnter;
            PictureBox.MouseDown += PictureBox_MouseDown;
            PictureBox.MouseUp += PictureBox_MouseUp;
            PictureBox.MouseMove += PictureBox_MouseMove;
            numericButtonX.ValueChanged += NumericButtonX_ValueChanged;
            numericButtonY.ValueChanged += NumericButtonY_ValueChanged;
            numericButtonWidth.ValueChanged += NumericButtonWidth_ValueChanged;
            numericButtonHeight.ValueChanged += NumericButtonHeight_ValueChanged;
        }

        // -------------------------------------------------------------------
        // UpdateOptions
        // -------------------------------------------------------------------

        public void UpdateOptions()
        {
            Control.Model.Options = new object[] { (int)numericButtonX.Value, (int)numericButtonY.Value, (int)numericButtonWidth.Value, (int)numericButtonHeight.Value };
        }

        // -------------------------------------------------------------------
        // UpdateRectangle
        // -------------------------------------------------------------------

        public void UpdateRectangle()
        {
            if (!IsUsingCursorSelector)
            {
                int x = (int)numericButtonX.Value, y = (int)numericButtonY.Value, width = (int)numericButtonWidth.Value, height = (int)numericButtonHeight.Value;
                if (x >= PictureBox.Image.Size.Width) x = PictureBox.Image.Size.Width - 1;
                if (y >= PictureBox.Image.Size.Height) y = PictureBox.Image.Size.Height - 1;
                if (x + width >= PictureBox.Image.Size.Width) width = PictureBox.Image.Size.Width - x;
                if (y + height >= PictureBox.Image.Size.Height) height = PictureBox.Image.Size.Height - y;
                numericButtonX.Value = x;
                numericButtonY.Value = y;
                numericButtonWidth.Value = width;
                numericButtonHeight.Value = height;

                PictureBox.SelectionRectangle.SetRectangle(x, y, width, height);
                PictureBox.Refresh();
                UpdateOptions();
            }
        }

        // -------------------------------------------------------------------
        // UpdateNumerics
        // -------------------------------------------------------------------

        public void UpdateNumerics()
        {
            PictureBox.Refresh();
            numericButtonX.Value = PictureBox.SelectionRectangle.RealX;
            numericButtonY.Value = PictureBox.SelectionRectangle.RealY;
            numericButtonWidth.Value = PictureBox.SelectionRectangle.Width;
            numericButtonHeight.Value = PictureBox.SelectionRectangle.Height;
        }

        // -------------------------------------------------------------------
        // Event
        // -------------------------------------------------------------------

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsUsingCursorSelector = true;
                PictureBox.MakeFirstRectangleSelection(e.X, e.Y, PictureBox.ZoomPixel);
                UpdateNumerics();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox.SetCursorRealPosition();
            IsUsingCursorSelector = false;
            PictureBox.Refresh();
            UpdateOptions();
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsUsingCursorSelector)
            {
                PictureBox.MakeRectangleSelection(e.X, e.Y, PictureBox.ZoomPixel);
                UpdateNumerics();
            }
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox.Focus();
        }

        private void NumericButtonX_ValueChanged(object sender, EventArgs e)
        {
            UpdateRectangle();
        }

        private void NumericButtonY_ValueChanged(object sender, EventArgs e)
        {
            UpdateRectangle();
        }

        private void NumericButtonWidth_ValueChanged(object sender, EventArgs e)
        {
            UpdateRectangle();
        }

        private void NumericButtonHeight_ValueChanged(object sender, EventArgs e)
        {
            UpdateRectangle();
        }
    }
}
