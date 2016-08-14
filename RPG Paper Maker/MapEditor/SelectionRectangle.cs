using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public class SelectionRectangle
    {
        int BORDER_SIZE;
        public Image TexCursor;
        public Pen Pen;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int SquareWidth, SquareHeight;
        public int RealX, RealY;

        public delegate void DrawMethod(Graphics g, int x, int y, float zoom);

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SelectionRectangle(int x, int y, int width, int height, int squareWidth, int squareHeight, int borderSize = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            RealX = 0;
            RealY = 0;
            SquareWidth = squareWidth;
            SquareHeight = squareHeight;
            BORDER_SIZE = borderSize;
        }

        // -------------------------------------------------------------------
        // GetRectangle
        // -------------------------------------------------------------------

        public int[] GetRectangleArray()
        {
            if (SquareWidth != 0 && SquareHeight != 0) return new int[] { (X / SquareWidth), (Y / SquareHeight), (Width / SquareWidth), (Height / SquareHeight) };
            else return new int[]{ 0, 0, 0, 0 };
        }

        // -------------------------------------------------------------------
        // SetRectangle
        // -------------------------------------------------------------------

        public void SetRectangle(int x, int y, int width, int height)
        {
            if (SquareWidth != 0 && SquareHeight != 0)
            {
                X = (x / SquareWidth) * SquareWidth;
                Y = (y / SquareHeight) * SquareHeight;
                Width = width * SquareWidth;
                Height = height * SquareHeight;
            }
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

        public void Draw(Graphics g, DrawMethod drawMethod, float zoom = 1.0f)
        {
            // Setting coords
            int x = X;
            int x_width = 0;

            if (Width < 0)
            {
                x += Width + SquareWidth;
                Width = -Width;
                x_width = Width - SquareWidth;
            }

            int y = Y;
            int y_height = 0;

            if (Height < 0)
            {
                y += Height + SquareHeight;
                Height = -Height;
                y_height = Height - SquareHeight;
            }

            if (SquareWidth != 0 && SquareHeight != 0) drawMethod(g, x, y, zoom);

            RealX = X - x_width;
            RealY = Y - y_height;
        }

        public void DrawWithImage(Graphics g, int x, int y, float zoom)
        {
            // Left-Top
            g.DrawImage(TexCursor, new Rectangle((int)(x * zoom), (int)(y * zoom), (int)(BORDER_SIZE * zoom), (int)(BORDER_SIZE * zoom)), new Rectangle(0, 0, BORDER_SIZE, BORDER_SIZE), GraphicsUnit.Pixel);
            // Right-Top
            g.DrawImage(TexCursor, new Rectangle((int)((x + Width - BORDER_SIZE) * zoom), (int)(y * zoom), (int)(BORDER_SIZE * zoom), (int)(BORDER_SIZE * zoom)), new Rectangle(TexCursor.Width - BORDER_SIZE, 0, BORDER_SIZE, BORDER_SIZE), GraphicsUnit.Pixel);
            // Right-Bot
            g.DrawImage(TexCursor, new Rectangle((int)((x + Width - BORDER_SIZE) * zoom), (int)((y + Height - BORDER_SIZE) * zoom), (int)(BORDER_SIZE * zoom), (int)(BORDER_SIZE * zoom)), new Rectangle(TexCursor.Width - BORDER_SIZE, TexCursor.Height - BORDER_SIZE, BORDER_SIZE, BORDER_SIZE), GraphicsUnit.Pixel);
            // Left-Bot
            g.DrawImage(TexCursor, new Rectangle((int)(x * zoom), (int)((y + Height - BORDER_SIZE) * zoom), (int)(BORDER_SIZE * zoom), (int)(BORDER_SIZE * zoom)), new Rectangle(0, TexCursor.Height - BORDER_SIZE, BORDER_SIZE, BORDER_SIZE), GraphicsUnit.Pixel);

            // Top
            g.DrawImage(TexCursor, new Rectangle((int)((x + BORDER_SIZE) * zoom), (int)(y * zoom), (int)((Width - (BORDER_SIZE * 2)) * zoom), (int)(BORDER_SIZE * zoom)), new Rectangle(BORDER_SIZE, 0, 1, BORDER_SIZE), GraphicsUnit.Pixel);
            // Right
            g.DrawImage(TexCursor, new Rectangle((int)((x + Width - BORDER_SIZE) * zoom), (int)((y + BORDER_SIZE) * zoom), (int)(BORDER_SIZE * zoom), (int)((Height - (BORDER_SIZE * 2)) * zoom)), new Rectangle(0, BORDER_SIZE, BORDER_SIZE, 1), GraphicsUnit.Pixel);
            // Bot
            g.DrawImage(TexCursor, new Rectangle((int)((x + BORDER_SIZE) * zoom), (int)((y + Height - BORDER_SIZE) * zoom), (int)((Width - (BORDER_SIZE * 2)) * zoom), (int)(BORDER_SIZE * zoom)), new Rectangle(BORDER_SIZE, 0, 1, BORDER_SIZE), GraphicsUnit.Pixel);
            // Left
            g.DrawImage(TexCursor, new Rectangle((int)(x * zoom), (int)((y + BORDER_SIZE) * zoom), (int)(BORDER_SIZE * zoom), (int)((Height - (BORDER_SIZE * 2)) * zoom)), new Rectangle(0, BORDER_SIZE, BORDER_SIZE, 1), GraphicsUnit.Pixel);
        }

        public void DrawWithPixel(Graphics g, int x, int y, float zoom)
        {
            int newX = (int)((x * zoom) + Math.Ceiling(zoom / 2)), newY = (int)((y * zoom) + Math.Ceiling(zoom / 2));
            int newWidth = (int)((Width - 1) * zoom), newHeight = (int)((Height - 1) * zoom);

            if (Width == 1 || Height == 1)
            {
                if (Width == 1) g.DrawLine(Pen, newX, (int)(y * zoom), newX, (int)((y + Height) * zoom));
                else if (Height == 1) g.DrawLine(Pen, (int)(x * zoom), newY, (int)((x + Width) * zoom), newY);
            }
            else g.DrawRectangle(Pen, new Rectangle(newX, newY, newWidth, newHeight));
        }
    }
}
