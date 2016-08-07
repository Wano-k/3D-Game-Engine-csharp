using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    
    class TilesetSelectorPicture : SelectionPictureBox
    {


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public TilesetSelectorPicture()
        {
            
            int BORDER_SIZE = 4;
            SelectionRectangle = new SelectionRectangle(0, 0, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE, BORDER_SIZE);
            try
            {
                using (FileStream stream = new FileStream(Path.Combine("Config", "bmp", "tileset_cursor.png"), FileMode.Open, FileAccess.Read))
                {
                    SelectionRectangle.TexCursor = Image.FromStream(stream);
                }

            }
            catch { }
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
                if (Image.Width >= WANOK.SQUARE_SIZE && Image.Height >= WANOK.SQUARE_SIZE) SelectionRectangle.Draw(g, SelectionRectangle.DrawWithImage);
            }
            catch { }
        }
    }
}
