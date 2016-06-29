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

        public void InitializeParameters(Collision collision, SystemGraphic graphic)
        {
            if (!graphic.IsNone())
            {
                LoadPicture(passagePicture, graphic);
                collision = GetPassageColision(collision);
                passagePicture.InitializeParameters(collision.PassableCollision);
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

        public Collision GetPassageColision(Collision collision)
        {
            TilesetPassage[,] newPassage = new TilesetPassage[passagePicture.Width / WANOK.BASIC_SQUARE_SIZE, passagePicture.Height / WANOK.BASIC_SQUARE_SIZE];

            for (int i = 0; i < newPassage.GetLength(0); i++)
            {
                for (int j = 0; j < newPassage.GetLength(1); j++)
                {
                    if (i < collision.PassableCollision.GetLength(0) && j < collision.PassableCollision.GetLength(1)) newPassage[i, j] = collision.PassableCollision[i, j].CreateCopy();
                    else newPassage[i, j] = new TilesetPassage();
                }
            }

            return new Collision(newPassage);
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
