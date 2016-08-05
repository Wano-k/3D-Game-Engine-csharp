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
    
    class TilesetSelectorPicture : InterpolationPictureBox
    {
        protected SelectionRectangle SelectionRectangle;
        protected Image TexCursor;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public TilesetSelectorPicture()
        {
            
            int BORDER_SIZE = 4;
            try
            {
                using (FileStream stream = new FileStream(Path.Combine("Config", "bmp", "tileset_cursor.png"), FileMode.Open, FileAccess.Read))
                {
                    TexCursor = Image.FromStream(stream);
                }

            }
            catch { }
            SelectionRectangle = new SelectionRectangle(0, 0, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE, BORDER_SIZE);
        }

        // -------------------------------------------------------------------
        // SetCursorRealPosition
        // -------------------------------------------------------------------

        public void SetCursorRealPosition()
        {
            SelectionRectangle.SetRealPosition();
        }

        // -------------------------------------------------------------------
        // SetCurrentTexture
        // -------------------------------------------------------------------

        public void SetCurrentTexture(int x, int y, int width, int height)
        {
            SelectionRectangle.SetRectangle(x, y, width, height);
        }

        public void SetCurrentTextureBasic()
        {
            SetCurrentTexture(0, 0, 1, 1);
        }

        // -------------------------------------------------------------------
        // GetCurrentTexture
        // -------------------------------------------------------------------

        public int[] GetCurrentTexture()
        {
            return SelectionRectangle.GetRectangleArray();
        }

        // -------------------------------------------------------------------
        // MakeFirstRectangleSelection
        // -------------------------------------------------------------------

        public void MakeFirstRectangleSelection(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height) SelectionRectangle.SetRectangle(x, y, 1, 1);
        }

        // -------------------------------------------------------------------
        // MakeRectangleSelection
        // -------------------------------------------------------------------

        public void MakeRectangleSelection(int x, int y)
        {
            if (x < 0) x = 0;
            if (x >= Width) x = Width - 1;
            int init_pos_x = SelectionRectangle.X / WANOK.BASIC_SQUARE_SIZE;
            int pos_x = x / WANOK.BASIC_SQUARE_SIZE;
            int i_x = init_pos_x <= pos_x ? 1 : -1;
            int width = (pos_x - init_pos_x) + i_x;
            SelectionRectangle.Width = width * WANOK.BASIC_SQUARE_SIZE;

            if (y < 0) y = 0;
            if (y >= Height) y = Height - 1;
            int init_pos_y = SelectionRectangle.Y / WANOK.BASIC_SQUARE_SIZE;
            int pos_y = y / WANOK.BASIC_SQUARE_SIZE;
            int i_y = init_pos_y <= pos_y ? 1 : -1;
            int height = (pos_y - init_pos_y) + i_y;
            SelectionRectangle.Height = height * WANOK.BASIC_SQUARE_SIZE;
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
                if (Image.Width >= WANOK.SQUARE_SIZE && Image.Height >= WANOK.SQUARE_SIZE) SelectionRectangle.Draw(g, TexCursor);
            }
            catch { }
        }
    }
}
