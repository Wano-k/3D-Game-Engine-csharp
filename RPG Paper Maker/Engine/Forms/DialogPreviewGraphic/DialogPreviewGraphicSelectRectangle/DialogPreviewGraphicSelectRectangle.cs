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
        public bool IsUsingCursorSelector = false;


        public DialogPreviewGraphicSelectRectangle(SystemGraphic graphic, object[] options) : base(graphic, options) 
        {
            // Picture
            PictureBox = new PixelSelectPictureBox();
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            panelPicture.Controls.Clear();
            panelPicture.Controls.Add(PictureBox);

            // Events
            PictureBox.MouseEnter += PictureBox_MouseEnter;
            PictureBox.MouseDown += PictureBox_MouseDown;
            PictureBox.MouseUp += PictureBox_MouseUp;
            PictureBox.MouseMove += PictureBox_MouseMove;
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsUsingCursorSelector = true;
                ((PixelSelectPictureBox)PictureBox).MakeFirstRectangleSelection(e.X, e.Y, (int)(((PixelSelectPictureBox)PictureBox).ZoomPixel));
                PictureBox.Refresh();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            ((PixelSelectPictureBox)PictureBox).SetCursorRealPosition();
            IsUsingCursorSelector = false;
            PictureBox.Refresh();
            //MapEditor.SetCurrentTexture(((PixelSelectPictureBox)PictureBox).GetCurrentTexture());
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsUsingCursorSelector)
            {
                ((PixelSelectPictureBox)PictureBox).MakeRectangleSelection(e.X, e.Y, (int)(((PixelSelectPictureBox)PictureBox).ZoomPixel));
                PictureBox.Refresh();
            }
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox.Focus();
        }
    }
}
