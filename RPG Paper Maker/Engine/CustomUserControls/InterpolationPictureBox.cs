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

        public void LoadTexture(SystemGraphic graphic)
        {
            Image = graphic.LoadImage();
            Size = new Size((int)(Image.Width * WANOK.RELATION_SIZE), (int)(Image.Height * WANOK.RELATION_SIZE));
            Location = new Point(0, 0);
        }

        // -------------------------------------------------------------------
        // OnPaint
        // -------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode;

            base.OnPaint(e);
        }
    }
}
