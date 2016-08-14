using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    class PixelSelectPictureBox : SelectionPictureBox
    {

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public PixelSelectPictureBox()
        {
            SelectionRectangle = new SelectionRectangle(0, 0, 1, 1, 1, 1);
            SelectionRectangle.Pen = new Pen(Color.Red, (int)ZoomPixel);
        }

        // -------------------------------------------------------------------
        // Zoom
        // -------------------------------------------------------------------

        public override void Zoom(float zoom)
        {
            base.Zoom(zoom);
            ZoomPixel = zoom;
            SelectionRectangle.Pen = new Pen(Color.Red, (int)ZoomPixel);
        }

        // -------------------------------------------------------------------
        // OnPaint
        // -------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            base.OnPaint(e);

            try
            {
                SelectionRectangle.Draw(g, SelectionRectangle.DrawWithPixel, ZoomPixel);
            }
            catch { }
        }
    }
}
