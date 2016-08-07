using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{

    class SelectionPictureBox : InterpolationPictureBox
    {
        public SelectionRectangle SelectionRectangle;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SelectionPictureBox()
        {

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

        public void MakeFirstRectangleSelection(int x, int y, int zoom = 1)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height) SelectionRectangle.SetRectangle(x / zoom, y / zoom, 1, 1);
        }

        // -------------------------------------------------------------------
        // MakeRectangleSelection
        // -------------------------------------------------------------------

        public void MakeRectangleSelection(int x, int y, int zoom = 1)
        {
            if (x < 0) x = 0;
            if (x >= Width) x = Width - 1;
            x /= zoom;
            int init_pos_x = SelectionRectangle.X / SelectionRectangle.SquareSize;
            int pos_x = x / SelectionRectangle.SquareSize;
            int i_x = init_pos_x <= pos_x ? 1 : -1;
            int width = (pos_x - init_pos_x) + i_x;
            SelectionRectangle.Width = width * SelectionRectangle.SquareSize;

            if (y < 0) y = 0;
            if (y >= Height) y = Height - 1;
            y /= zoom;
            int init_pos_y = SelectionRectangle.Y / SelectionRectangle.SquareSize;
            int pos_y = y / SelectionRectangle.SquareSize;
            int i_y = init_pos_y <= pos_y ? 1 : -1;
            int height = (pos_y - init_pos_y) + i_y;
            SelectionRectangle.Height = height * SelectionRectangle.SquareSize;
        }
    }
}
