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
        public ListBox[] ListBoxesCanceling, ListBoxes;
        public System.Timers.Timer DragTimer = new System.Timers.Timer(20);
        public bool CanDrag = false;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogAddingSpecialList(string text, SystemDatas model, List<int> superListTileset, Type type)
        {
            InitializeComponent();

            Text = text;
            Type = type;
            ListBoxesCanceling = new ListBox[] { textBoxGraphic.GetTextBox(), listBoxTileset };
            ListBoxes = new ListBox[] { listBoxComplete.GetListBox() };

            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;
            groupBox2.Paint += MainForm.PaintBorderGroupBox;

            // list event handlers
            for (int i = 0; i < ListBoxesCanceling.Length; i++)
            {
                ListBoxesCanceling[i].MouseClick += listBox_MouseClick;
            }
            for (int i = 0; i < ListBoxes.Length; i++)
            {
                ListBoxes[i].MouseClick += listBox_MouseClick;
            }

            MouseWheel += new MouseEventHandler(form_MouseWheel);
            listBoxComplete.GetListBox().DoubleClick += listBoxComplete_DoubleClick;

            DragTimer.Elapsed += new System.Timers.ElapsedEventHandler(DoDrag);
        }

        // -------------------------------------------------------------------
        // GetListTilesetAutotiles
        // -------------------------------------------------------------------

        public List<int> GetListTileset()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < listBoxTileset.Items.Count; i++) list.Add(((SuperListItem)listBoxTileset.Items[i]).Id);

            return list;
        }

        // -------------------------------------------------------------------
        // UnselectAllCancelingLists
        // -------------------------------------------------------------------

        public void UnselectAllCancelingLists()
        {
            for (int i = 0; i < ListBoxesCanceling.Length; i++)
            {
                ListBoxesCanceling[i].ClearSelected();
            }
        }

        // -------------------------------------------------------------------
        // UnselectAllLists
        // -------------------------------------------------------------------

        public void UnselectAllLists()
        {
            UnselectAllCancelingLists();
            for (int i = 0; i < ListBoxes.Length; i++)
            {
                ListBoxes[i].SelectedIndex = 0;
            }
        }

        // -------------------------------------------------------------------
        // AddItem
        // -------------------------------------------------------------------

        public void AddItem()
        {
            SuperListItem item = (SuperListItem)listBoxComplete.GetListBox().SelectedItem;
            bool test = true;

            for (int i = 0; i < listBoxTileset.Items.Count; i++)
            {
                if (item.Id == ((SuperListItem)listBoxTileset.Items[i]).Id)
                {
                    test = false;
                    break;
                }
            }

            if (test) listBoxTileset.Items.Add(item);
        }

        // -------------------------------------------------------------------
        // DeleteItem
        // -------------------------------------------------------------------

        public void DeleteItem()
        {
            SuperListItem item = (SuperListItem)listBoxTileset.SelectedItem;
            if (item != null)
            {
                listBoxTileset.Items.Remove(item);
            }
        }

        // -------------------------------------------------------------------
        // listBox_MouseClick
        // -------------------------------------------------------------------

        public void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = ((ListBox)sender).IndexFromPoint(e.X, e.Y); ;
            UnselectAllCancelingLists();
            ((ListBox)sender).SelectedIndex = index;
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

            for (int i = 0; i < listBoxTileset.Items.Count; i++)
            {
                if (item.Id == ((SuperListItem)listBoxTileset.Items[i]).Id) listBoxTileset.Items[i] = item;
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

        // -------------------------------------------------------------------
        // listBoxTileset_KeyDown
        // -------------------------------------------------------------------

        private void listBoxTileset_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBoxTileset.SelectedIndex != -1)
            {
                if (e.KeyCode == Keys.Delete) DeleteItem();
            }
        }

        // -------------------------------------------------------------------
        // Drag & drop
        // -------------------------------------------------------------------

        private void listBoxTileset_MouseDown(object sender, MouseEventArgs e)
        {
            // If left clic, can drag and drop
            if (e.Button == MouseButtons.Left)
            {
                if (listBoxTileset.SelectedItem == null) return;
                if (!DragTimer.Enabled) DragTimer.Start();
            }
        }

        private void DoDrag(object sender, System.Timers.ElapsedEventArgs e)
        {
            CanDrag = true;
            DragTimer.Stop();
        }

        private void listBoxTileset_DragDrop(object sender, DragEventArgs e)
        {
            Point point = listBoxTileset.PointToClient(new Point(e.X, e.Y));
            int newIndex = listBoxTileset.IndexFromPoint(point);
            if (newIndex < 0) newIndex = listBoxTileset.Items.Count - 1;
            object data = e.Data.GetData(Type);

            listBoxTileset.Items.Remove(data);
            listBoxTileset.Items.Insert(newIndex, data);
            listBoxTileset.SelectedIndex = newIndex;
        }

        private void listBoxTileset_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBoxTileset_MouseUp(object sender, MouseEventArgs e)
        {
            CanDrag = false;
        }

        private void listBoxTileset_MouseMove(object sender, MouseEventArgs e)
        {
            if (CanDrag)
            {
                listBoxTileset.DoDragDrop(listBoxTileset.SelectedItem, DragDropEffects.Move);
                CanDrag = false;
            }
        }    

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
