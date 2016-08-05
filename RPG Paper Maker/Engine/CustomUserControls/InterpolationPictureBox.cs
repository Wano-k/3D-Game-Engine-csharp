using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public class InterpolationPictureBox : PictureBox
    {

        public InterpolationMode InterpolationMode { get; set; }

        // -------------------------------------------------------------------
        // LoadTexture
        // -------------------------------------------------------------------

        public void LoadTexture(SystemGraphic graphic, float zoom = -1)
        {
            Image = graphic.LoadImage();
            if (zoom == -1) zoom = WANOK.RELATION_SIZE;
            Zoom(zoom);
            Location = new Point(0, 0);
        }

        // -------------------------------------------------------------------
        // Zoom
        // -------------------------------------------------------------------

        public void Zoom(float zoom)
        {
            Size = new Size((int)(Image.Width * zoom), (int)(Image.Height * zoom));
        }

        // -------------------------------------------------------------------
        // OnPaint
        // -------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

            base.OnPaint(e);
        }
    }
}
