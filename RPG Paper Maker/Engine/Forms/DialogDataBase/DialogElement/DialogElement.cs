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

            textBoxName.InitializeParameters(Control.Model.Names);
            textBoxGraphicIcon.InitializeParameters(element.Icon);
            PictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxIcon.InterpolationMode = InterpolationMode.NearestNeighbor;
            LoadIcon(element.Icon);

            // Events
            textBoxGraphicIcon.GetTextBox().SelectedValueChanged += textBoxGraphicIcon_SelectedValueChanged;

            InitializeDataBindings();
        }

        // -------------------------------------------------------------------
        // InitializeDataBindings
        // -------------------------------------------------------------------

        private void InitializeDataBindings()
        {
            textBoxName.GetTextBox().DataBindings.Add("Text", ViewModelBindingSource, "Name", true);
        }

        // -------------------------------------------------------------------
        // GetObject
        // -------------------------------------------------------------------

        public override SuperListItem GetObject()
        {
            return Control.Model;
        }

        // -------------------------------------------------------------------
        // LoadIcon
        // -------------------------------------------------------------------

        public void LoadIcon(SystemGraphic graphic)
        {
            PictureBoxIcon.Image = graphic.LoadImage();
            PictureBoxIcon.Size = new Size((WANOK.BASIC_SQUARE_SIZE / 2) + 2, (WANOK.BASIC_SQUARE_SIZE / 2) + 2);
            PictureBoxIcon.Location = new Point(0, 0);
        }

        // -------------------------------------------------------------------
        // Events
        // -------------------------------------------------------------------

        private void textBoxGraphicIcon_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadIcon(Control.Model.Icon);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
