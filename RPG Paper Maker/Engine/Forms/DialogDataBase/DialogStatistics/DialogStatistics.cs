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
    public partial class DialogStatistics : SuperListDialog
    {
        protected DialogStatisticsControl Control;
        protected BindingSource ViewModelBindingSource = new BindingSource();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogStatistics(SystemStatistics statistics)
        {
            InitializeComponent();

            Control = new DialogStatisticsControl(statistics);
            ViewModelBindingSource.DataSource = Control;

            textBoxName.InitializeParameters(Control.Model.Names);
            textBoxGraphicIcon.InitializeParameters(statistics.Bar);
            PictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxIcon.InterpolationMode = InterpolationMode.NearestNeighbor;
            LoadBar(statistics.Bar);

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
        // LoadBar
        // -------------------------------------------------------------------

        public void LoadBar(SystemGraphic graphic)
        {
            PictureBoxIcon.Image = graphic.LoadImage();
            
            int width, height;
            if (PictureBoxIcon.Image.Size.Width > PanelBar.Size.Width) width = PanelBar.Size.Width;
            else width = PictureBoxIcon.Image.Size.Width;
            
            if (PictureBoxIcon.Image.Size.Height > PanelBar.Size.Height) height = PanelBar.Size.Height;
            else height = PictureBoxIcon.Image.Size.Height;


            /*
            if (PictureBoxIcon.Image.Size.Width > PanelBar.Size.Width || PictureBoxIcon.Image.Size.Height > PanelBar.Size.Height) PictureBoxIcon.Size = PanelBar.Size;
            else PictureBoxIcon.Size = PictureBoxIcon.Image.Size;
            */
            PictureBoxIcon.Size = new Size(width, height);
            PictureBoxIcon.Location = new Point(0, 0);
        }

        // -------------------------------------------------------------------
        // Events
        // -------------------------------------------------------------------

        private void textBoxGraphicIcon_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadBar(Control.Model.Bar);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
