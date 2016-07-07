using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class SelectionRectangle
    {
        int BORDER_SIZE;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        protected int RealX, RealY;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SelectionRectangle(int x, int y, int width, int height, int borderSize)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            RealX = 0;
            RealY = 0;
            BORDER_SIZE = borderSize;
        }

        // -------------------------------------------------------------------
        // GetRectangle
        // -------------------------------------------------------------------

        public int[] GetRectangleArray()
        {
            return new int[] { (X / WANOK.BASIC_SQUARE_SIZE), (Y / WANOK.BASIC_SQUARE_SIZE), (Width / WANOK.BASIC_SQUARE_SIZE), (Height / WANOK.BASIC_SQUARE_SIZE) };
        }

        // -------------------------------------------------------------------
        // SetRectangle
        // -------------------------------------------------------------------

        public void SetRectangle(int x, int y, int width, int height)
        {
            X = (x / WANOK.BASIC_SQUARE_SIZE) * WANOK.BASIC_SQUARE_SIZE;
            Y = (y / WANOK.BASIC_SQUARE_SIZE) * WANOK.BASIC_SQUARE_SIZE;
            Width = width * WANOK.BASIC_SQUARE_SIZE;
            Height = height * WANOK.BASIC_SQUARE_SIZE;
        }

        // -------------------------------------------------------------------
        // SetRealPosition
        // -------------------------------------------------------------------

        public void SetRealPosition()
        {
            X = RealX;
            Y = RealY;
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(Graphics g, Image texCursor)
        {
            // Setting coords
            int x = X;
            int x_width = 0;

            if (Width < 0)
            {
                x += Width + WANOK.BASIC_SQUARE_SIZE;
                Width = -Width;
                x_width = Width - WANOK.BASIC_SQUARE_SIZE;
            }

            int y = Y;
            int y_height = 0;

            if (Height < 0)
            {
                y += Height + WANOK.BASIC_SQUARE_SIZE;
                Height = -Height;
                y_height = Height - WANOK.BASIC_SQUARE_SIZE;
            }


            // Left-Top
            g.DrawImage(texCursor, new Rectangle(x, y, BORDER_SIZE, BORDER_SIZE), new Rectangle(0, 0, BORDER_SIZE, BORDER_SIZE), GraphicsUnit.Pixel);
            // Right-Top
            g.DrawImage(texCursor, new Rectangle(x + Width - BORDER_SIZE, y, BORDER_SIZE, BORDER_SIZE), new Rectangle(WANOK.BASIC_SQUARE_SIZE - BORDER_SIZE, 0, BORDER_SIZE, BORDER_SIZE), GraphicsUnit.Pixel);
            // Right-Bot
            g.DrawImage(texCursor, new Rectangle(x + Width - BORDER_SIZE, y + Height - BORDER_SIZE, BORDER_SIZE, BORDER_SIZE), new Rectangle(WANOK.BASIC_SQUARE_SIZE - BORDER_SIZE, WANOK.BASIC_SQUARE_SIZE - BORDER_SIZE, BORDER_SIZE, BORDER_SIZE), GraphicsUnit.Pixel);
            // Left-Bot
            g.DrawImage(texCursor, new Rectangle(x, y + Height - BORDER_SIZE, BORDER_SIZE, BORDER_SIZE), new Rectangle(0, WANOK.BASIC_SQUARE_SIZE - BORDER_SIZE, BORDER_SIZE, BORDER_SIZE), GraphicsUnit.Pixel);

            // Top
            g.DrawImage(texCursor, new Rectangle(x + BORDER_SIZE, y, Width - (BORDER_SIZE * 2), BORDER_SIZE), new Rectangle(BORDER_SIZE, 0, 1, BORDER_SIZE), GraphicsUnit.Pixel);
            // Right
            g.DrawImage(texCursor, new Rectangle(x + Width - BORDER_SIZE, y + BORDER_SIZE, BORDER_SIZE, Height - (BORDER_SIZE * 2)), new Rectangle(0, BORDER_SIZE, BORDER_SIZE, 1), GraphicsUnit.Pixel);
            // Bot
            g.DrawImage(texCursor, new Rectangle(x + BORDER_SIZE, y + Height - BORDER_SIZE, Width - (BORDER_SIZE * 2), BORDER_SIZE), new Rectangle(BORDER_SIZE, 0, 1, BORDER_SIZE), GraphicsUnit.Pixel);
            // Left
            g.DrawImage(texCursor, new Rectangle(x, y + BORDER_SIZE, BORDER_SIZE, Height - (BORDER_SIZE * 2)), new Rectangle(0, BORDER_SIZE, BORDER_SIZE, 1), GraphicsUnit.Pixel);

            RealX = X - x_width;
            RealY = Y - y_height;
        }
    }
}
