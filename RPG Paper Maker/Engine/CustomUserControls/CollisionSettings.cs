using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace RPG_Paper_Maker
{
    public partial class CollisionSettings : UserControl
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public CollisionSettings()
        {
            InitializeComponent();

            passagePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            passagePicture.InterpolationMode = InterpolationMode.NearestNeighbor;
        }

        // -------------------------------------------------------------------
        // InitializeParameters
        // -------------------------------------------------------------------

        public void InitializeParameters(Tileset tileset)
        {
            if (!tileset.Graphic.IsNone())
            {
                LoadPicture(passagePicture, tileset.Graphic);
                tileset.PassableCollision = GetPassageColision(tileset.PassableCollision);
                passagePicture.InitializeParameters(tileset.PassableCollision);
            }
            else
            {
                LoadNonePicture(passagePicture);
            }
        }

        // -------------------------------------------------------------------
        // LoadTextures
        // -------------------------------------------------------------------

        public void LoadTextures()
        {
            passagePicture.LoadTextures();
        }

        // -------------------------------------------------------------------
        // GetPassageColision
        // -------------------------------------------------------------------

        public TilesetPassage[,] GetPassageColision(TilesetPassage[,] passage)
        {
            TilesetPassage[,] newPassage = new TilesetPassage[passagePicture.Width / WANOK.BASIC_SQUARE_SIZE, passagePicture.Height / WANOK.BASIC_SQUARE_SIZE];

            for (int i = 0; i < newPassage.GetLength(0); i++)
            {
                for (int j = 0; j < newPassage.GetLength(1); j++)
                {
                    if (i < passage.GetLength(0) && j < passage.GetLength(1)) newPassage[i, j] = passage[i, j].CreateCopy();
                    else newPassage[i, j] = new TilesetPassage();
                }
            }

            return newPassage;
        }

        // -------------------------------------------------------------------
        // LoadPicture
        // -------------------------------------------------------------------

        public void LoadPicture(PictureBox pb, SystemGraphic graphic)
        {
            pb.Image = graphic.LoadImage();
            pb.Size = new Size((int)(pb.Image.Width * WANOK.RELATION_SIZE), (int)(pb.Image.Height * WANOK.RELATION_SIZE));
        }

        // -------------------------------------------------------------------
        // LoadNonePicture
        // -------------------------------------------------------------------

        public void LoadNonePicture(PictureBox pb)
        {
            pb.Image = null;
            pb.Size = new Size(0, 0);
        }
    }
}
