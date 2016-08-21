using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPG_Paper_Maker.Engine.CustomUserControls;
using System.Drawing;

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
        SystemTileset Tileset;
        TilesetsDatas Model;
        List<object[]> ReliefsTop = new List<object[]>();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogAddingReliefsList(string text, TilesetsDatas model, SystemTileset tileset) : base(text, model, tileset.Reliefs, typeof(SystemRelief))
        {
            Tileset = tileset;
            ReliefsTop = tileset.CreateReliefTopCopy();
            Model = model;
            TextBoxVariable.InitializeParameters(new object[] { 0, 0, 1, 1 }, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
            ComboBoxAutotile.Dock = DockStyle.Fill;
            ComboBoxAutotile.FillComboBox(Tileset.Autotiles, Model.GetAutotileById, Model.GetAutotileIndexById, 0);

            // Radios 
            Radios[DrawType.None] = new RadioButton();
            Radios[DrawType.None].Text = "None";
            Radios[DrawType.Floors] = new RadioButton();
            Radios[DrawType.Floors].Text = "Floor";
            Radios[DrawType.Autotiles] = new RadioButton();
            Radios[DrawType.Autotiles].Text = "Autotile";

            // Fill boxes
            listBoxComplete.GetListBox().SelectedIndexChanged += listBoxComplete_SelectedIndexChanged;
            listBoxComplete.InitializeListParameters(true, WANOK.GetSuperListItem(model.Reliefs.Cast<SuperListItem>().ToList()), null, Type, 1, SystemRelief.MAX_RELIEFS);
            List<SuperListItem> modelTileset = new List<SuperListItem>();
            for (int i = 0; i < tileset.Reliefs.Count; i++)
            {
                modelTileset.Add(model.GetReliefById(tileset.Reliefs[i]));
            }
            listBoxTileset.GetListBox().SelectedIndexChanged += ListBoxTileset_SelectedIndexChanged;
            listBoxTileset.InitializeListParameters(true, modelTileset, null, Type, 0, 0, false, false);

            // PictureRelief
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox.InterpolationMode = InterpolationMode.NearestNeighbor;
            PicturePanel.AutoScroll = true;
            PicturePanel.Dock = DockStyle.Fill;
            PicturePanel.BackColor = System.Drawing.Color.FromArgb(227, 227, 227);
            PicturePanel.Controls.Add(PictureBox);

            // Floor Panel
            RadioPanel.Dock = DockStyle.Fill;
            RadioPanel.RowCount = 3;
            RadioPanel.ColumnCount = 2;
            RadioPanel.Controls.Add(Radios[DrawType.None], 0, 0);
            RadioPanel.Controls.Add(Radios[DrawType.Floors], 0, 1);
            RadioPanel.Controls.Add(Radios[DrawType.Autotiles], 0, 2);
            RadioPanel.Controls.Add(new Panel(), 1, 0);
            RadioPanel.Controls.Add(TextBoxVariable, 1, 1);
            RadioPanel.Controls.Add(ComboBoxAutotile, 1, 2);
            RadioPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            RadioPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29));
            RadioPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            RadioPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            RadioPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            FloorPanel.Dock = DockStyle.Fill;
            FloorPanel.RowCount = 2;
            FloorPanel.ColumnCount = 1;
            GrowLabel label = new GrowLabel();
            label.Dock = DockStyle.Fill;
            label.Text = "Choose what kind of floor there is on top of a mountains/slopes:";
            FloorPanel.Controls.Add(label, 0, 0);
            FloorPanel.Controls.Add(RadioPanel, 0, 1);
            FloorPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 15));
            FloorPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // Setting layout
            LayoutPanel.Dock = DockStyle.Fill;
            LayoutPanel.RowCount = 1;
            LayoutPanel.ColumnCount = 1;
            LayoutPanel.Controls.Add(PicturePanel, 0, 0);
            LayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            PanelOther.Controls.Add(LayoutPanel);
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tableLayoutPanel8.Controls.Add(FloorPanel, 0, 1);
            FloorPanel.Visible = listBoxTileset.GetListBox().Items.Count > 0;

            // Events
            textBoxGraphic.GetTextBox().SelectedValueChanged += textBoxGraphic_SelectedValueChanged;
            listBoxComplete.GetListBox().MouseDown += listBoxComplete_SelectedIndexChanged;
            listBoxComplete.GetButton().Click += listBoxComplete_Click;
            buttonDelete.Click += ButtonDelete_Click;
            Radios[DrawType.None].CheckedChanged += RadioCheckedNone;
            Radios[DrawType.Floors].CheckedChanged += RadioCheckedFloor;
            Radios[DrawType.Autotiles].CheckedChanged += RadioCheckedAutotile;
            ComboBoxAutotile.SelectedIndexChanged += ComboBoxAutotileSelectedIndexChanged;
            buttonAdd.Click += ButtonAdd_Click;
            listBoxComplete.GetListBox().DoubleClick += ListBoxComplete_DoubleClick;
            TextBoxVariable.GetTextBox().SelectedValueChanged += TextVariable_SelectedValueChanged;
            listBoxTileset.GetListBox().DragDrop += ListBoxTileset_DragDrop;
            listBoxTileset.GetListBox().MouseDown += ListBoxTileset_SelectedIndexChanged;
        }


        // -------------------------------------------------------------------
        // GetListReliefs
        // -------------------------------------------------------------------

        public List<SystemRelief> GetListReliefs()
        {
            return listBoxComplete.GetListBox().Items.Cast<SystemRelief>().ToList();
        }

        // -------------------------------------------------------------------
        // GetListReliefsTop
        // -------------------------------------------------------------------

        public List<object[]> GetListReliefsTop()
        {
            return ReliefsTop;
        }

        // -------------------------------------------------------------------
        // RadioSelect
        // -------------------------------------------------------------------

        public void RadioSelect(DrawType drawType)
        {
            TextBoxVariable.Enabled = false;
            ComboBoxAutotile.Enabled = false;

            if (drawType == DrawType.Floors) TextBoxVariable.Enabled = true;
            if (drawType == DrawType.Autotiles) ComboBoxAutotile.Enabled = true;

            if (drawType == DrawType.None) ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1] = null;
            if (drawType == DrawType.Floors) ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1] = new int[] { (int)TextBoxVariable.Value[0], (int)TextBoxVariable.Value[1], (int)TextBoxVariable.Value[2], (int)TextBoxVariable.Value[3] };
            if (drawType == DrawType.Autotiles)
            {
                if (ComboBoxAutotile.SelectedIndex == -1) ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1] = new int[] { 0 };
                else ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1] = new int[] { Tileset.Autotiles[ComboBoxAutotile.SelectedIndex] };
            }
            ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][0] = drawType;
        }

        // -------------------------------------------------------------------
        // AddReliefTop
        // -------------------------------------------------------------------

        public void AddReliefTop()
        {
            ReliefsTop.Add(new object[] { DrawType.None, null });
            if (listBoxTileset.GetListBox().Items.Count == 1) FloorPanel.Visible = true;
            UpdateRelief();
        }

        // -------------------------------------------------------------------
        // DeleteReliefTop
        // -------------------------------------------------------------------

        public void DeleteReliefTop()
        {
            if (SelectedItemTileset != -1)
            {
                ReliefsTop.RemoveAt(SelectedItemTileset);
            }
            if (listBoxTileset.GetListBox().Items.Count == 0) FloorPanel.Visible = false;
            UpdateRelief();
        }

        // -------------------------------------------------------------------
        // UpdateRelief
        // -------------------------------------------------------------------

        public void UpdateRelief()
        {
            if (listBoxTileset.GetListBox().SelectedIndex != -1 && listBoxTileset.GetListBox().Items.Count == ReliefsTop.Count)
            {
                if ((DrawType)ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][0] == DrawType.Floors) TextBoxVariable.InitializeParameters(
                    new object[] {
                        ((int[])ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1])[0],
                        ((int[])ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1])[1],
                        ((int[])ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1])[2],
                        ((int[])ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1])[3],
                    },
                    new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
                else TextBoxVariable.InitializeParameters(new object[] { 0, 0, 1, 1 }, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
                if ((DrawType)ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][0] == DrawType.Autotiles)
                {
                    int id = ((int[])ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1])[0];
                    if (id > 0 && id <= Tileset.Autotiles.Count) ComboBoxAutotile.SelectedIndex = Tileset.Autotiles.IndexOf(((int[])ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1])[0]);
                }
                else if (ComboBoxAutotile.Items.Count > 0) ComboBoxAutotile.SelectedIndex = 0;

                Radios[(DrawType)ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][0]].Checked = true;
            }
        }

        // -------------------------------------------------------------------
        // Events
        // -------------------------------------------------------------------

        protected void listBoxComplete_SelectedIndexChanged(object sender, EventArgs e)
        {
            SystemRelief relief = (SystemRelief)listBoxComplete.GetListBox().SelectedItem;
            if (relief != null)
            {
                textBoxName.Text = relief.Name;
                textBoxGraphic.InitializeParameters(relief.Graphic);
                PictureBox.LoadTexture(relief.Graphic);
            }
        }

        public void textBoxGraphic_SelectedValueChanged(object sender, EventArgs e)
        {
            SystemRelief relief = (SystemRelief)listBoxComplete.GetListBox().SelectedItem;
            relief.Graphic = textBoxGraphic.Graphic;
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

        private void ComboBoxAutotileSelectedIndexChanged(object sender, EventArgs e)
        {
            ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1] = new int[] { Tileset.Autotiles[ComboBoxAutotile.SelectedIndex] };
        }

        private void ListBoxTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRelief();
        }

        private void TextVariable_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxTileset.GetListBox().SelectedIndex != -1)
            {
                ReliefsTop[listBoxTileset.GetListBox().SelectedIndex][1] = new int[] {
                    (int)TextBoxVariable.Value[0],
                    (int)TextBoxVariable.Value[1],
                    (int)TextBoxVariable.Value[2],
                    (int)TextBoxVariable.Value[3]
                };
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddReliefTop();
        }

        private void ListBoxComplete_DoubleClick(object sender, EventArgs e)
        {
            AddReliefTop();
        }

        private void ListBoxTileset_DragDrop(object sender, DragEventArgs e)
        {
            object[] obj = ReliefsTop[OldIndex];
            Point point = listBoxTileset.GetListBox().PointToClient(new Point(e.X, e.Y));
            int newIndex = listBoxTileset.GetListBox().IndexFromPoint(point);
            if (newIndex < 0) newIndex = listBoxTileset.GetListBox().Items.Count - 1;
            ReliefsTop.RemoveAt(OldIndex);
            ReliefsTop.Insert(newIndex, obj);
            UpdateRelief();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            DeleteReliefTop();
        }
    }
}
