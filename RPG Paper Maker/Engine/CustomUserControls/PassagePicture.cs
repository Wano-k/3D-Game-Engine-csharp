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
    class PassagePicture : PictureBox
    {
        public InterpolationMode InterpolationMode { get; set; }
        public TilesetPassage[,] PassageInfos = null;
        public Bitmap CursorYes, CursorNo;
        public Pen GridPen = new Pen(Color.DimGray);


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public PassagePicture()
        {

        }

        // -------------------------------------------------------------------
        // InitializeParameters
        // -------------------------------------------------------------------

        public void InitializeParameters(TilesetPassage[,] passageInfos)
        {
            PassageInfos = passageInfos;
        }

        // -------------------------------------------------------------------
        // LoadTextures
        // -------------------------------------------------------------------

        public void LoadTextures()
        {
            CursorYes = Properties.Resources.circle;
            CursorNo = Properties.Resources.cross_orange;
        }

        // -------------------------------------------------------------------
        // OnPaint
        // -------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            base.OnPaint(e);

            if (Width != 0 && Height != 0)
            {
                try
                {
                    for (int i = 0; i < PassageInfos.GetLength(0); i++)
                    {
                        for (int j = 0; j < PassageInfos.GetLength(1); j++)
                        {
                            if (i == 0) g.DrawLine(GridPen, 0, j * WANOK.BASIC_SQUARE_SIZE, PassageInfos.GetLength(0) * WANOK.BASIC_SQUARE_SIZE, j * WANOK.BASIC_SQUARE_SIZE);
                            g.DrawImage(CursorYes, new Rectangle(i * WANOK.BASIC_SQUARE_SIZE, j * WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE));
                        }
                        g.DrawLine(GridPen, i * WANOK.BASIC_SQUARE_SIZE, 0, i * WANOK.BASIC_SQUARE_SIZE, PassageInfos.GetLength(1) * WANOK.BASIC_SQUARE_SIZE);
                    }
                    g.DrawLine(GridPen, 0, PassageInfos.GetLength(1) * WANOK.BASIC_SQUARE_SIZE, PassageInfos.GetLength(0) * WANOK.BASIC_SQUARE_SIZE, PassageInfos.GetLength(1) * WANOK.BASIC_SQUARE_SIZE);
                    g.DrawLine(GridPen, PassageInfos.GetLength(0) * WANOK.BASIC_SQUARE_SIZE, 0, PassageInfos.GetLength(0) * WANOK.BASIC_SQUARE_SIZE, PassageInfos.GetLength(1) * WANOK.BASIC_SQUARE_SIZE);
                }
                catch { }
            }
        }
    }
}