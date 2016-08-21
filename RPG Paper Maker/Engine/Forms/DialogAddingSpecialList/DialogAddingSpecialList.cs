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
    public partial class DialogAddingSpecialList : Form
    {
        public Type Type;
        protected int SelectedItemTileset = -1;
        protected int OldIndex = -1;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogAddingSpecialList(string text, TilesetsDatas model, List<int> superListTileset, Type type)
        {
            InitializeComponent();

            Text = text;
            Type = type;

            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;
            groupBox2.Paint += MainForm.PaintBorderGroupBox;

            listBoxTileset.GetButton().Hide();

            MouseWheel += new MouseEventHandler(form_MouseWheel);
            listBoxComplete.GetListBox().DoubleClick += listBoxComplete_DoubleClick;
            listBoxTileset.GetListBox().MouseDown += listBoxTileset_MouseDown;
        }


        // -------------------------------------------------------------------
        // GetListTilesetAutotiles
        // -------------------------------------------------------------------

        public List<int> GetListTileset()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < listBoxTileset.GetListBox().Items.Count; i++) list.Add(((SuperListItem)listBoxTileset.GetListBox().Items[i]).Id);

            return list;
        }

        // -------------------------------------------------------------------
        // AddItem
        // -------------------------------------------------------------------

        public void AddItem()
        {
            SuperListItem item = (SuperListItem)listBoxComplete.GetListBox().SelectedItem;
            bool test = true;

            for (int i = 0; i < listBoxTileset.GetListBox().Items.Count; i++)
            {
                if (item.Id == ((SuperListItem)listBoxTileset.GetListBox().Items[i]).Id)
                {
                    test = false;
                    break;
                }
            }

            if (test)
            {
                listBoxTileset.GetListBox().Items.Add(item);
                if (listBoxTileset.GetListBox().Items.Count == 1) listBoxTileset.GetListBox().SelectedIndex = 0;
            }
        }

        // -------------------------------------------------------------------
        // DeleteItem
        // -------------------------------------------------------------------

        public void DeleteItem()
        {
            SelectedItemTileset = listBoxTileset.GetListBox().SelectedIndex;
            if (SelectedItemTileset != -1)
            {
                listBoxTileset.GetListBox().Items.RemoveAt(SelectedItemTileset);
                if (SelectedItemTileset > 0) listBoxTileset.GetListBox().SelectedIndex = SelectedItemTileset - 1;
                else if (listBoxTileset.GetListBox().Items.Count > 0) listBoxTileset.GetListBox().SelectedIndex = 0;
            }
        }

        // -------------------------------------------------------------------
        // form_MouseWheel
        // -------------------------------------------------------------------

        private void form_MouseWheel(object sender, MouseEventArgs e)
        {
            listBoxComplete.GetListBox().Focus();
        }

        // -------------------------------------------------------------------
        // textBoxName_TextChanged
        // -------------------------------------------------------------------

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            listBoxComplete.SetName(textBoxName.Text);
            SuperListItem item = (SuperListItem)listBoxComplete.GetListBox().SelectedItem;

            for (int i = 0; i < listBoxTileset.GetListBox().Items.Count; i++)
            {
                if (item.Id == ((SuperListItem)listBoxTileset.GetListBox().Items[i]).Id) listBoxTileset.GetListBox().Items[i] = item;
            }
        }

        // -------------------------------------------------------------------
        // buttonAdd_Click
        // -------------------------------------------------------------------

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        // -------------------------------------------------------------------
        // listBoxComplete_DoubleClick
        // -------------------------------------------------------------------

        private void listBoxComplete_DoubleClick(object sender, EventArgs e)
        {
            AddItem();
        }

        // -------------------------------------------------------------------
        // buttonDelete_Click
        // -------------------------------------------------------------------

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void listBoxTileset_MouseDown(object sender, MouseEventArgs e)
        {
            // If left clic, can drag and drop
            if (e.Button == MouseButtons.Left)
            {
                if (listBoxTileset.GetListBox().SelectedItem == null) return;
                OldIndex = listBoxTileset.GetListBox().SelectedIndex;
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
