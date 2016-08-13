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
        public List<Control[]> ListControlsGameOver = new List<Control[]>();
        public List<Control[]> ListControlsGameOverHeroes = new List<Control[]>();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogStatistics(SystemStatistics statistics)
        {
            InitializeComponent();

            Control = new DialogStatisticsControl(statistics);
            ViewModelBindingSource.DataSource = Control;

            // Initialise general datas
            textBoxName.InitializeParameters(Control.Model.Names);
            textBoxGraphicIcon.InitializeParameters(statistics.Bar.CreateCopy(), typeof(DialogPreviewGraphicSelectRectangle), OptionsKind.BarSelection);
            PictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxIcon.InterpolationMode = InterpolationMode.NearestNeighbor;
            LoadBar(statistics.Bar);

            // Game over
            listView1.Columns[0].Width = listView1.Size.Width - 4;
            for (int i = 0; i < WANOK.Game.Heroes.HeroesList.Count; i++)
            {
                listView1.Items.Add(WANOK.GetStringList(WANOK.Game.Heroes.HeroesList[i].Id, WANOK.Game.Heroes.HeroesList[i].Name));
                if (statistics.AllGameOverOptions.HeroesSelected != null && statistics.AllGameOverOptions.HeroesSelected.Contains(WANOK.Game.Heroes.HeroesList[i].Id)) listView1.Items[i].Checked = true;

            }
            comboBoxComparaison1.InitValues();
            comboBoxMeasure1.InitValues();
            ListControlsGameOver.Add(new Control[] { });
            ListControlsGameOver.Add(new Control[] { radioButton3, radioButton4, listView1, label3, comboBoxComparaison1, label4, numericUpDown1, comboBoxMeasure1 });
            ListControlsGameOverHeroes.Add(new Control[] { });
            ListControlsGameOverHeroes.Add(new Control[] { listView1 });
            if (statistics.AllGameOverOptions.NoImplication) radioButton1.Checked = true;
            else radioButton2.Checked = true;
            if (statistics.AllGameOverOptions.AllHeroes) radioButton3.Checked = true;
            else radioButton4.Checked = true;
            comboBoxComparaison1.SelectedIndex = (int)statistics.AllGameOverOptions.Comparaison;
            comboBoxMeasure1.SelectedIndex = (int)statistics.AllGameOverOptions.Measure;

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
            numericUpDown1.DataBindings.Add("Value", ViewModelBindingSource, "Value", true);
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

            PictureBoxIcon.Size = new Size(width, height);
            PictureBoxIcon.Location = new Point(0, 0);
        }

        // -------------------------------------------------------------------
        // CheckGameOver
        // -------------------------------------------------------------------

        public void CheckGameOver(int index)
        {
            WANOK.CheckControls(ListControlsGameOver, index);
        }

        public void CheckGameOverHeroes(int index)
        {
            WANOK.CheckControls(ListControlsGameOverHeroes, index);
        }

        // -------------------------------------------------------------------
        // Events
        // -------------------------------------------------------------------

        private void textBoxGraphicIcon_SelectedValueChanged(object sender, EventArgs e)
        {
            Control.Model.Bar = textBoxGraphicIcon.Graphic;
            LoadBar(Control.Model.Bar);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CheckGameOver(0);
            Control.Model.AllGameOverOptions.NoImplication = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CheckGameOver(1);
            if (radioButton3.Checked) listView1.Enabled = false;
            Control.Model.AllGameOverOptions.NoImplication = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            CheckGameOverHeroes(0);
            Control.Model.AllGameOverOptions.AllHeroes = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            CheckGameOverHeroes(1);
            Control.Model.AllGameOverOptions.AllHeroes = false;
        }

        private void comboBoxComparaison1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control.Model.AllGameOverOptions.Comparaison = comboBoxComparaison1.GetResult();
        }

        private void comboBoxMeasure1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control.Model.AllGameOverOptions.Measure = comboBoxMeasure1.GetResult();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Checked) list.Add(WANOK.Game.Heroes.HeroesList[i].Id);
            }

            Control.Model.AllGameOverOptions.HeroesSelected = list.Count == 0 ? null : list;
            var lol = Control.Model.AllGameOverOptions;
        }
    }
}
