using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPG_Paper_Maker.Engine.CustomUserControls;

namespace RPG_Paper_Maker
{
    class DialogAddingReliefsList : DialogAddingSpecialList
    {
        InterpolationPictureBox PictureBox = new InterpolationPictureBox();
        TableLayoutPanel LayoutPanel = new TableLayoutPanel();
        TableLayoutPanel FloorPanel = new TableLayoutPanel();
        Panel PicturePanel = new Panel();
        TableLayoutPanel RadioPanel = new TableLayoutPanel();
        Dictionary<DrawType, RadioButton> Radios = new Dictionary<DrawType, RadioButton>();
        TextBoxVariables TextBoxVariable = new TextBoxVariables();
        ImageComboBox ComboBoxAutotile = new ImageComboBox();
        Tileset Tileset;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogAddingReliefsList(string text, SystemDatas model, Tileset tileset) : base(text, model, tileset.Reliefs, typeof(SystemRelief))
        {
            Tileset = tileset;
            TextBoxVariable.InitializeParameters(new object[] { 0, 0, 1, 1 }, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
            ComboBoxAutotile.Dock = DockStyle.Fill;
            ComboBoxAutotile.FillComboBox(Tileset.Autotiles, WANOK.SystemDatas.GetAutotileById, WANOK.SystemDatas.GetAutotileIndexById, 1);
            ListBoxesCanceling.Add(TextBoxVariable.GetTextBox());
            for (int i = 0; i < ListBoxesCanceling.Count; i++)
            {
                ListBoxesCanceling[i].MouseClick += listBox_MouseClick;
            }
            for (int i = 0; i < ListBoxes.Count; i++)
            {
                ListBoxes[i].MouseClick += listBox_MouseClick;
            }

            // Fill boxes
            List<SuperListItem> list = new List<SuperListItem>();
            for (int i = 0; i < model.Reliefs.Count; i++)
            {
                list.Add(model.Reliefs[i].CreateCopy());
            }
            listBoxComplete.InitializeListParameters(new ListBox[] { }, list, null, Type, 1, SystemRelief.MAX_RELIEFS);
            for (int i = 0; i < tileset.Reliefs.Count; i++)
            {
                listBoxTileset.Items.Add(model.GetReliefById(tileset.Reliefs[i]));
            }

            // PictureRelief
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.InterpolationMode = InterpolationMode.NearestNeighbor;
            PicturePanel.AutoScroll = true;
            PicturePanel.Dock = DockStyle.Fill;
            PicturePanel.BackColor = System.Drawing.Color.FromArgb(227, 227, 227);
            PicturePanel.Controls.Add(PictureBox);

            // Floor Panel
            RadioPanel.Dock = DockStyle.Fill;
            RadioPanel.RowCount = 4;
            RadioPanel.ColumnCount = 2;
            Radios[DrawType.None] = new RadioButton();
            Radios[DrawType.None].Text = "None";
            RadioPanel.Controls.Add(Radios[DrawType.None], 0, 0);
            Radios[DrawType.Floors] = new RadioButton();
            Radios[DrawType.Floors].Text = "Floor";
            RadioPanel.Controls.Add(Radios[DrawType.Floors], 0, 1);
            Radios[DrawType.Autotiles] = new RadioButton();
            Radios[DrawType.Autotiles].Text = "Autotile";
            RadioPanel.Controls.Add(Radios[DrawType.Autotiles], 0, 2);
            Radios[DrawType.Water] = new RadioButton();
            Radios[DrawType.Water].Text = "Water";
            RadioPanel.Controls.Add(Radios[DrawType.Water], 0, 3);
            RadioPanel.Controls.Add(new Panel(), 1, 0);
            RadioPanel.Controls.Add(TextBoxVariable, 1, 1);
            RadioPanel.Controls.Add(ComboBoxAutotile, 1, 2);
            RadioPanel.Controls.Add(new Panel(), 1, 3);
            RadioPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            RadioPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            RadioPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            RadioPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            RadioPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            RadioPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            FloorPanel.Dock = DockStyle.Fill;
            FloorPanel.RowCount = 2;
            FloorPanel.ColumnCount = 1;
            Label label = new Label();
            label.Dock = DockStyle.Fill;
            label.Text = "Choose what kind of floor there is on top of a mountains/slopes:";
            FloorPanel.Controls.Add(label, 0, 0);
            FloorPanel.Controls.Add(RadioPanel, 0, 1);
            FloorPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 15));
            FloorPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // Setting layout
            LayoutPanel.Dock = DockStyle.Fill;
            LayoutPanel.RowCount = 2;
            LayoutPanel.ColumnCount = 1;
            LayoutPanel.Controls.Add(PicturePanel, 0, 0);
            LayoutPanel.Controls.Add(FloorPanel, 0, 1);
            LayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            LayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            PanelOther.Controls.Add(LayoutPanel);

            // Events
            textBoxGraphic.GetTextBox().SelectedValueChanged += textBoxGraphic_SelectedValueChanged;
            listBoxComplete.GetListBox().SelectedIndexChanged += listBoxComplete_SelectedIndexChanged;
            listBoxComplete.GetButton().Click += listBoxComplete_Click;
            Radios[DrawType.None].CheckedChanged += RadioCheckedNone;
            Radios[DrawType.Floors].CheckedChanged += RadioCheckedFloor;
            Radios[DrawType.Autotiles].CheckedChanged += RadioCheckedAutotile;
            Radios[DrawType.Water].CheckedChanged += RadioCheckedWater;
            ComboBoxAutotile.SelectedIndexChanged += ComboBoxAutotileSelectedIndexChanged;

            UnselectAllLists();
        }

        // -------------------------------------------------------------------
        // GetListReliefs
        // -------------------------------------------------------------------

        public List<SystemRelief> GetListReliefs()
        {
            return listBoxComplete.GetListBox().Items.Cast<SystemRelief>().ToList();
        }

        // -------------------------------------------------------------------
        // RadioSelect
        // -------------------------------------------------------------------

        public void RadioSelect(DrawType drawType)
        {
            Radios[DrawType.Water].Enabled = false;
            TextBoxVariable.Enabled = false;
            ComboBoxAutotile.Enabled = false;

            if (drawType == DrawType.Floors) TextBoxVariable.Enabled = true;
            if (drawType == DrawType.Autotiles) ComboBoxAutotile.Enabled = true;
            SystemRelief relief = (SystemRelief)listBoxComplete.GetListBox().SelectedItem;
            if (drawType == DrawType.Floors) relief.TopTexture = TextBoxVariable.Value;
            if (drawType == DrawType.Autotiles) relief.TopTexture = new object[] { Tileset.Autotiles[ComboBoxAutotile.SelectedIndex] };
            relief.TopDrawType = drawType;

            /*
            if (relief.TopDrawType == DrawType.Floors) TextBoxVariables[DrawType.Floors].InitializeParameters(relief.TopTexture, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
            else TextBoxVariables[DrawType.Floors].InitializeParameters(new object[] { 0, 0, 1, 1 }, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
            if (relief.TopDrawType == DrawType.Autotiles) TextBoxVariables[DrawType.Autotiles].InitializeParameters(relief.TopTexture, null, typeof(DialogTileset), WANOK.GetStringVariables);
            else TextBoxVariables[DrawType.Autotiles].InitializeParameters(new object[] { 1 }, null, typeof(DialogTileset), WANOK.GetStringVariables);
            if (relief.TopDrawType == DrawType.Water) TextBoxVariables[DrawType.Water].InitializeParameters(relief.TopTexture, null, typeof(DialogTileset), WANOK.GetStringVariables);
            else TextBoxVariables[DrawType.Water].InitializeParameters(new object[] { 1 }, null, typeof(DialogTileset), WANOK.GetStringVariables);
            */
        }

        // -------------------------------------------------------------------
        // Events
        // -------------------------------------------------------------------

        public void listBoxComplete_SelectedIndexChanged(object sender, EventArgs e)
        {
            SystemRelief relief = (SystemRelief)listBoxComplete.GetListBox().SelectedItem;
            if (relief != null)
            {
                textBoxName.Text = relief.Name;
                textBoxGraphic.InitializeParameters(relief.Graphic);
                PictureBox.LoadTexture(relief.Graphic);

                if (relief.TopDrawType == DrawType.Floors) TextBoxVariable.InitializeParameters(relief.TopTexture, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
                else TextBoxVariable.InitializeParameters(new object[] { 0, 0, 1, 1 }, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
                if (relief.TopDrawType == DrawType.Autotiles)
                {
                    ComboBoxAutotile.SelectedIndex = WANOK.SystemDatas.GetAutotileIndexById((int)relief.TopTexture[0]);
                }
                else ComboBoxAutotile.SelectedIndex = 0;
                /*
                if (relief.TopDrawType == DrawType.Floors) TextBoxVariables[DrawType.Floors].InitializeParameters(relief.TopTexture, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
                else TextBoxVariables[DrawType.Floors].InitializeParameters(new object[] { 0, 0, 1, 1 }, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
                if (relief.TopDrawType == DrawType.Autotiles) TextBoxVariables[DrawType.Autotiles].InitializeParameters(relief.TopTexture, null, typeof(DialogTileset), WANOK.GetStringVariables);
                else TextBoxVariables[DrawType.Autotiles].InitializeParameters(new object[] { 1 }, null, typeof(DialogTileset), WANOK.GetStringVariables);
                if (relief.TopDrawType == DrawType.Water) TextBoxVariables[DrawType.Water].InitializeParameters(relief.TopTexture, null, typeof(DialogTileset), WANOK.GetStringVariables);
                else TextBoxVariables[DrawType.Water].InitializeParameters(new object[] { 1 }, null, typeof(DialogTileset), WANOK.GetStringVariables);
                */

                Radios[relief.TopDrawType].Checked = true;
            }
        }
        
        public void textBoxGraphic_SelectedValueChanged(object sender, EventArgs e)
        {
            SystemRelief relief = (SystemRelief)listBoxComplete.GetListBox().SelectedItem;
            PictureBox.LoadTexture(relief.Graphic);
        }

        private void listBoxComplete_Click(object sender, EventArgs e)
        {
            
        }

        private void RadioCheckedNone(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true) RadioSelect(DrawType.None);
        }

        private void RadioCheckedFloor(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                RadioSelect(DrawType.Floors);
            }
        }

        private void RadioCheckedAutotile(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true) RadioSelect(DrawType.Autotiles);
        }

        private void RadioCheckedWater(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true) RadioSelect(DrawType.Water);
        }

        private void ComboBoxAutotileSelectedIndexChanged(object sender, EventArgs e)
        {
            SystemRelief relief = (SystemRelief)listBoxComplete.GetListBox().SelectedItem;
            relief.TopTexture = new object[] { Tileset.Autotiles[ComboBoxAutotile.SelectedIndex] };
        }
    }
}
