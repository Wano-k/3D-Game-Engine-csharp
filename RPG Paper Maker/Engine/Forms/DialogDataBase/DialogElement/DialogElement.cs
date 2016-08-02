using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    // SuperListDialog
    public partial class DialogElement : SuperListDialog
    {
        protected DialogElementControl Control;
        protected BindingSource ViewModelBindingSource = new BindingSource();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogElement(SystemElement element)
        {
            InitializeComponent();

            Control = new DialogElementControl(element);
            ViewModelBindingSource.DataSource = Control;
            /*
            panelColor.BackColor = color.GetWinformsColor();
            textBoxName.Select();*/
            textBoxName.GetTextBox().Items.Add(Control.Model.Names[WANOK.Game.System.Langs[0]]);
            textBoxName.AllNames = Control.Model.Names;
            textBoxGraphicIcon.InitializeParameters(element.Icon);
            PictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxIcon.InterpolationMode = InterpolationMode.NearestNeighbor;
            PictureBoxIcon.Image = element.Icon.LoadImage();
            PictureBoxIcon.Size = new Size((WANOK.BASIC_SQUARE_SIZE / 2) + 2, (WANOK.BASIC_SQUARE_SIZE / 2) + 2);
            PictureBoxIcon.Location = new Point(0, 0);

            InitializeDataBindings();
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            textBoxName.DataBindings.Add("Text", ViewModelBindingSource, "Name", true);
        }

        // -------------------------------------------------------------------
        // GetObject
        // -------------------------------------------------------------------

        public override SuperListItem GetObject()
        {
            return Control.Model;
        }
    }
}
