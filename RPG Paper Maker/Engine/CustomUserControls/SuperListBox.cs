using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;

namespace RPG_Paper_Maker
{
    public partial class SuperListBox : UserControl
    {
        public List<SuperListItem> ModelList { get { return listBox.Items.Cast<SuperListItem>().ToList(); } }
        public Type DialogKind;
        public Type TypeItem;
        public SuperListItem CopiedItem = null;
        public int Min, Max;
        public System.Timers.Timer DragTimer = new System.Timers.Timer(20);
        public bool CanDrag = false;
        public Engine.TextBoxLang TextBoxLang;
        public TextBox TextBox;
        public int IndexDrag = -1;
        public bool IsSelectedItemWhenLosingFocus = false;
        public bool DisplayMenuContext = true;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SuperListBox()
        {
            InitializeComponent();

            listBox.FormattingEnabled = false;
            DragTimer.Elapsed += new System.Timers.ElapsedEventHandler(DoDrag);
            listBox.LostFocus += ListBox_LostFocus;
        }

        // -------------------------------------------------------------------
        // InitializeListParameters
        // -------------------------------------------------------------------

        public void InitializeListParameters(bool select, List<SuperListItem> modelList, Type type, Type typeItem, int min, int max, bool displayMenuContext = true)
        {
            IsSelectedItemWhenLosingFocus = select;
            DialogKind = type;
            DisplayMenuContext = displayMenuContext;
            TypeItem = typeItem;
            Min = min;
            Max = max;

            for (int i = 0; i < modelList.Count; i++)
            {
                listBox.Items.Add(modelList[i]);
            }

            if (typeItem == typeof(SuperListItemName))
            {
                TextBoxLang = new Engine.TextBoxLang();
                tableLayoutPanel1.Controls.Add(TextBoxLang, 0, 1);
                TextBoxLang.GetTextBox().TextChanged += SuperListBox_TextChanged;
            }
            else if (typeItem == typeof(SuperListItemNameWithoutLang))
            {
                TextBox = new TextBox();
                tableLayoutPanel1.Controls.Add(TextBox, 0, 1);
                TextBox.TextChanged += SuperListBoxWithoutLang_TextChanged;
            }

            if (IsSelectedItemWhenLosingFocus && listBox.Items.Count > 0) listBox.SelectedIndex = 0;
        }


        // -------------------------------------------------------------------
        // GetListBox
        // -------------------------------------------------------------------

        public ListBox GetListBox()
        {
            return listBox;
        }

        // -------------------------------------------------------------------
        // GetButton
        // -------------------------------------------------------------------

        public Button GetButton()
        {
            return button;
        }

        // -------------------------------------------------------------------
        // SetName
        // -------------------------------------------------------------------

        public void SetName(string name)
        {
            ((SuperListItem)listBox.Items[listBox.SelectedIndex]).Name = name;
            listBox.Items[listBox.SelectedIndex] = listBox.SelectedItem;
        }

        // -------------------------------------------------------------------
        // EditItem
        // -------------------------------------------------------------------

        public void EditItem()
        {
            // If double clic is opening a window...
            if (DialogKind != null)
            {
                SuperListDialog dialog = (SuperListDialog)Activator.CreateInstance(DialogKind, ((SuperListItem)listBox.Items[listBox.SelectedIndex]).CreateCopy());
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    int index = listBox.SelectedIndex;
                    listBox.Items.RemoveAt(index);
                    listBox.Items.Insert(index, dialog.GetObject());
                    listBox.SelectedIndex = index;
                }
            }
        }

        // -------------------------------------------------------------------
        // CopyItem
        // -------------------------------------------------------------------

        public void CopyItem()
        {
            CopiedItem = ((SuperListItem)listBox.Items[listBox.SelectedIndex]).CreateCopy();
        }

        // -------------------------------------------------------------------
        // PasteItem
        // -------------------------------------------------------------------

        public void PasteItem()
        {
            if (CopiedItem != null)
            {
                CopiedItem.Id = ((SuperListItem)listBox.Items[listBox.SelectedIndex]).Id;
                listBox.Items[listBox.SelectedIndex] = CopiedItem;
                CopiedItem = CopiedItem.CreateCopy();
            }
        }

        // -------------------------------------------------------------------
        // DeleteItem
        // -------------------------------------------------------------------

        public void DeleteItem()
        {
            SuperListItem defaultValue = (SuperListItem)Activator.CreateInstance(TypeItem, ((SuperListItem)listBox.Items[listBox.SelectedIndex]).Id);
            listBox.Items[listBox.SelectedIndex] = defaultValue;
        }

        // -------------------------------------------------------------------
        // listBox_MouseDown
        // -------------------------------------------------------------------

        private void listBox_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            listBox.Focus();
            IndexDrag = listBox.IndexFromPoint(e.X, e.Y);
            listBox.SelectedIndex = IndexDrag;
            if (listBox.SelectedIndex == -1) listBox.SelectedIndex = listBox.Items.Count - 1;

            // If left clic, can drag and drop
            if (e.Button == MouseButtons.Left)
            {
                if (!DragTimer.Enabled) DragTimer.Start();
            }

            // If right clic, open ContextMenu
            if (e.Button == MouseButtons.Right)
            {
                if (listBox.SelectedIndex != -1)
                {
                    ItemEdit.Enabled = DialogKind != null;
                    ItemPaste.Enabled = CopiedItem != null;
                    if (DisplayMenuContext) contextMenuStrip.Show(listBox, e.Location);
                }
            }

            SelectedIndexChanged();
        }

        // -------------------------------------------------------------------
        // listBox_DoubleClick
        // -------------------------------------------------------------------

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                EditItem();
            }
            else
            {
                listBox.SelectedIndex = listBox.Items.Count - 1;
            }
        }

        // -------------------------------------------------------------------
        // ItemEdit_Click
        // -------------------------------------------------------------------

        private void ItemEdit_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        // -------------------------------------------------------------------
        // ItemCopy_Click
        // -------------------------------------------------------------------

        private void ItemCopy_Click(object sender, EventArgs e)
        {
            CopyItem();
        }

        // -------------------------------------------------------------------
        // ItemPaste_Click
        // -------------------------------------------------------------------

        private void ItemPaste_Click(object sender, EventArgs e)
        {
            PasteItem();
        }

        // -------------------------------------------------------------------
        // ItemDelete_Click
        // -------------------------------------------------------------------

        private void ItemDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        // -------------------------------------------------------------------
        // SelectedIndexChanged
        // -------------------------------------------------------------------

        private void SelectedIndexChanged()
        {
            if (TypeItem == typeof(SuperListItemName) && listBox.SelectedItem != null) TextBoxLang.InitializeParameters(((SuperListItemName)listBox.SelectedItem).Names);
            else if (TypeItem == typeof(SuperListItemNameWithoutLang) && listBox.SelectedItem != null) TextBox.Name = ((SuperListItemName)listBox.SelectedItem).Name;
        }

        // -------------------------------------------------------------------
        // listBox_KeyDown
        // -------------------------------------------------------------------

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox.SelectedIndex != -1 && DisplayMenuContext)
            {
                if (e.KeyCode == Keys.Delete) DeleteItem();
                if (e.Control && e.KeyCode == Keys.C) CopyItem();
                if (e.Control && e.KeyCode == Keys.V) PasteItem();
            }
        }

        // -------------------------------------------------------------------
        // DoDrag
        // -------------------------------------------------------------------

        private void DoDrag(object sender, System.Timers.ElapsedEventArgs e)
        {
            CanDrag = true;
            DragTimer.Stop();
        }

        // -------------------------------------------------------------------
        // listBox_DragDrop
        // -------------------------------------------------------------------

        private void listBox_DragDrop(object sender, DragEventArgs e)
        {
            Point point = listBox.PointToClient(new Point(e.X, e.Y));
            int newIndex = listBox.IndexFromPoint(point);
            if (newIndex < 0) newIndex = listBox.Items.Count - 1;
            object data = e.Data.GetData(TypeItem);

            listBox.Items.Remove(data);
            listBox.Items.Insert(newIndex, data);
            listBox.SelectedIndex = newIndex;
        }

        // -------------------------------------------------------------------
        // listBox_DragOver
        // -------------------------------------------------------------------

        private void listBox_DragOver(object sender, DragEventArgs e)
        {
            if (CanDrag && listBox.SelectedItem != null) e.Effect = DragDropEffects.Move;
            else e.Effect = DragDropEffects.None;
        }

        // -------------------------------------------------------------------
        // listBox_MouseUp
        // -------------------------------------------------------------------

        private void listBox_MouseUp(object sender, MouseEventArgs e)
        {
            CanDrag = false;
        }

        // -------------------------------------------------------------------
        // listBox_MouseMove
        // -------------------------------------------------------------------

        private void listBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (CanDrag && listBox.SelectedItem != null)
            {
                listBox.DoDragDrop(listBox.SelectedItem, DragDropEffects.Move);
                CanDrag = false;
            }
        }

        // -------------------------------------------------------------------
        // listBox_SelectedIndexChanged
        // -------------------------------------------------------------------

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged();
        }

        // -------------------------------------------------------------------
        // button_Click
        // -------------------------------------------------------------------

        private void button_Click(object sender, EventArgs e)
        {
            DialogEnterNumber dialog = new DialogEnterNumber(listBox.Items.Count, Min, Max, ModelList);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Suppress
                if (listBox.Items.Count > dialog.Value)
                {
                    int nb = listBox.Items.Count - dialog.Value;
                    for (int i = 0; i < nb; i++) listBox.Items.RemoveAt(dialog.Value);
                }
                // Add
                else if (listBox.Items.Count < dialog.Value)
                {
                    int nb = dialog.Value - listBox.Items.Count;
                    for (int i = 0; i < nb; i++) {
                        SuperListItem defaultValue = (SuperListItem)Activator.CreateInstance(TypeItem, listBox.Items.Count + 1);
                        listBox.Items.Add(defaultValue);
                    }
                }
            }
        }

        private void SuperListBox_TextChanged(object sender, EventArgs e)
        {
            ((SuperListItemName)listBox.SelectedItem).SetName();
            SetName(((SuperListItemName)listBox.SelectedItem).Name);
        }

        private void SuperListBoxWithoutLang_TextChanged(object sender, EventArgs e)
        {
            SetName(((SuperListItemNameWithoutLang)listBox.SelectedItem).Name);
        }

        private void ListBox_LostFocus(object sender, EventArgs e)
        {
            if (!IsSelectedItemWhenLosingFocus) listBox.SelectedIndex = -1;
        }
    }
}
