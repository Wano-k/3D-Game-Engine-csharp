using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class DialogTileset : DialogVariable
    {
        object[] Texture;

        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogTileset()
        {
            InitializeComponent();

            // Events
            tilesetSelectorPicture1.MouseDown += new MouseEventHandler(TilesetSelectorPicture_MouseDown);
            tilesetSelectorPicture1.MouseUp += new MouseEventHandler(TilesetSelectorPicture_MouseUp);
            ok.Click += new EventHandler(ok_Click);
        }

        // -------------------------------------------------------------------
        // GetObject
        // -------------------------------------------------------------------

        public override object[] GetObject()
        {
            return Texture;
        }

        // -------------------------------------------------------------------
        // InitializeParameters
        // -------------------------------------------------------------------

        public override void InitializeParameters(object[] value, List<object> others)
        {
            Texture = value;
            tilesetSelectorPicture1.SizeMode = PictureBoxSizeMode.StretchImage;
            tilesetSelectorPicture1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            tilesetSelectorPicture1.LoadTexture((SystemGraphic)others[0]);
            tilesetSelectorPicture1.BackColor = Color.FromArgb(227, 227, 227);
            tilesetSelectorPicture1.SetCurrentTexture((int)Texture[0] * WANOK.BASIC_SQUARE_SIZE, (int)Texture[1] * WANOK.BASIC_SQUARE_SIZE, (int)Texture[2], (int)Texture[3]);
        }

        // -------------------------------------------------------------------
        // Events
        // -------------------------------------------------------------------

        private void TilesetSelectorPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tilesetSelectorPicture1.MakeFirstRectangleSelection(e.X, e.Y);
                tilesetSelectorPicture1.Refresh();
            }
        }

        private void TilesetSelectorPicture_MouseUp(object sender, MouseEventArgs e)
        {
            tilesetSelectorPicture1.SetCursorRealPosition();
            tilesetSelectorPicture1.Refresh();
            int[] texture = tilesetSelectorPicture1.GetCurrentTexture();
            Texture = new object[] { texture[0], texture[1], texture[2], texture[3] };
        }

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
