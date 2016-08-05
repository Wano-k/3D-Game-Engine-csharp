using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    class PixelSelectPictureBox : InterpolationPictureBox
    {
        public Rectangle CurrentSelection = new Rectangle(0, 0, 20, 20);
        public Pen Pen = new Pen(Color.Red);

        // -------------------------------------------------------------------
        // OnPaint
        // -------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            base.OnPaint(e);

            g.DrawRectangle(Pen, CurrentSelection);
        }
    }
}
