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
    class CollisionPicture : PictureBox
    {
        public InterpolationMode InterpolationMode { get; set; }


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public CollisionPicture()
        {

        }

        // -------------------------------------------------------------------
        // OnPaint
        // -------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode;
            Graphics g = e.Graphics;

            base.OnPaint(e);
        }
    }
}
