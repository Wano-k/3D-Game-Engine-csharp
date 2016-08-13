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
        SystemTileset Tileset;
        TilesetsDatas Model;
        bool UpdatePanel = false;
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
            listBoxComplete.InitializeListParameters(new ListBox[] { }, WANOK.GetSuperListItem(model.Reliefs.Cast<SuperListItem>().ToList()), null, Type, 1, SystemRelief.MAX_RELIEFS);
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
            RadioPanel.RowCount = 3;
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
            RadioPanel.Controls.Add(new Panel(), 1, 0);
            RadioPanel.Controls.Add(TextBoxVariable, 1, 1);
            RadioPanel.Controls.Add(ComboBoxAutotile, 1, 2);
            RadioPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            RadioPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
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
            FloorPanel.Hide();

            // Events
            textBoxGraphic.GetTextBox().SelectedValueChanged += textBoxGraphic_SelectedValueChanged;
            listBoxComplete.GetListBox().SelectedIndexChanged += listBoxComplete_SelectedIndexChanged;
            listBoxComplete.GetListBox().MouseDown += listBoxComplete_SelectedIndexChanged;
            listBoxComplete.GetButton().Click += listBoxComplete_Click;
            listBoxTileset.SelectedIndexChanged += ListBoxTileset_SelectedIndexChanged;
            listBoxTileset.MouseUp += ListBoxMouseUp;
            listBoxComplete.GetListBox().MouseUp += ListBoxMouseUp;
            Radios[DrawType.None].CheckedChanged += RadioCheckedNone;
            Radios[DrawType.Floors].CheckedChanged += RadioCheckedFloor;
            Radios[DrawType.Autotiles].CheckedChanged += RadioCheckedAutotile;
            ComboBoxAutotile.SelectedIndexChanged += ComboBoxAutotileSelectedIndexChanged;
            buttonAdd.Click += ButtonAdd_Click;
            listBoxComplete.GetListBox().DoubleClick += ListBoxComplete_DoubleClick;
            buttonDelete.Click += ButtonDelete_Click;
            TextBoxVariable.GetTextBox().SelectedValueChanged += TextVariable_SelectedValueChanged;
            listBoxTileset.DragDrop += ListBoxTileset_DragDrop;

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

            if (drawType == DrawType.None) ReliefsTop[listBoxTileset.SelectedIndex][1] = null;
            if (drawType == DrawType.Floors) ReliefsTop[listBoxTileset.SelectedIndex][1] = new int[] { (int)TextBoxVariable.Value[0], (int)TextBoxVariable.Value[1], (int)TextBoxVariable.Value[2], (int)TextBoxVariable.Value[3] };
            if (drawType == DrawType.Autotiles)
            {
                if (ComboBoxAutotile.SelectedIndex == -1) ReliefsTop[listBoxTileset.SelectedIndex][1] = new int[] { 0 };
                else ReliefsTop[listBoxTileset.SelectedIndex][1] = new int[] { Tileset.Autotiles[ComboBoxAutotile.SelectedIndex] };
            }
            ReliefsTop[listBoxTileset.SelectedIndex][0] = drawType;
        }

        // -------------------------------------------------------------------
        // AddReliefTop
        // -------------------------------------------------------------------

        public void AddReliefTop()
        {
            ReliefsTop.Add(new object[] { DrawType.None, null });
        }

        // -------------------------------------------------------------------
        // DeleteReliefTop
        // -------------------------------------------------------------------

        public void DeleteReliefTop()
        {
            ReliefsTop.RemoveAt(SelectedItemTileset);
        }

        // -------------------------------------------------------------------
        // UpdateRelief
        // -------------------------------------------------------------------

        public void UpdateRelief()
        {
            if (listBoxTileset.SelectedIndex != -1 && listBoxTileset.Items.Count == ReliefsTop.Count)
            {
                if ((DrawType)ReliefsTop[listBoxTileset.SelectedIndex][0] == DrawType.Floors) TextBoxVariable.InitializeParameters(
                    new object[] {
                        ((int[])ReliefsTop[listBoxTileset.SelectedIndex][1])[0],
                        ((int[])ReliefsTop[listBoxTileset.SelectedIndex][1])[1],
                        ((int[])ReliefsTop[listBoxTileset.SelectedIndex][1])[2],
                        ((int[])ReliefsTop[listBoxTileset.SelectedIndex][1])[3],
                    },
                    new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
                else TextBoxVariable.InitializeParameters(new object[] { 0, 0, 1, 1 }, new object[] { Tileset.Graphic }, typeof(DialogTileset), WANOK.GetStringTileset);
                if ((DrawType)ReliefsTop[listBoxTileset.SelectedIndex][0] == DrawType.Autotiles)
                {
                    int id = ((int[])ReliefsTop[listBoxTileset.SelectedIndex][1])[0];
                    if (id > 0 && id <= Tileset.Autotiles.Count) ComboBoxAutotile.SelectedIndex = Tileset.Autotiles.IndexOf(((int[])ReliefsTop[listBoxTileset.SelectedIndex][1])[0]);
                }
                else if (ComboBoxAutotile.Items.Count > 0) ComboBoxAutotile.SelectedIndex = 0;

                Radios[(DrawType)ReliefsTop[listBoxTileset.SelectedIndex][0]].Checked = true;
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
            UpdatePanel = false;
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
            ReliefsTop[listBoxTileset.SelectedIndex][1] = new int[] { Tileset.Autotiles[ComboBoxAutotile.SelectedIndex] };
        }

        private void ListBoxTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRelief();

            if (listBoxTileset.SelectedItem != null)
            {
                UpdatePanel = true;
            }
            else
            {
                UpdatePanel = false;
            }
        }

        private void ListBoxMouseUp(object sender, MouseEventArgs e)
        {
            FloorPanel.Visible = UpdatePanel;
            UpdatePanel = false;
        }

        private void TextVariable_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxTileset.SelectedIndex != -1)
            {
                ReliefsTop[listBoxTileset.SelectedIndex][1] = new int[] {
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

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            DeleteReliefTop();
        }

        private void ListBoxTileset_DragDrop(object sender, DragEventArgs e)
        {
            object[] obj = ReliefsTop[OldIndex];
            ReliefsTop.RemoveAt(OldIndex);
            ReliefsTop.Insert(NewIndex, obj);
            UpdateRelief();
            FloorPanel.Visible = UpdatePanel;
            UpdatePanel = false;
        }
    }
}
