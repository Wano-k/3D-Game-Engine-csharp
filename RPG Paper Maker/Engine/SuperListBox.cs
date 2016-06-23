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
        public List<SuperListItem> ModelList;
        public Type DialogKind;
        public ListBox[] ListBoxes;
        public int Max;

        public SuperListBox()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------------------
        // InitializeListParameters
        // -------------------------------------------------------------------

        public void InitializeListParameters(ListBox[] list, List<SuperListItem> model, Type type, int max)
        {
            ListBoxes = list;
            ModelList = model;
            DialogKind = type;
            Max = max;

            for (int i = 0; i < ModelList.Count; i++)
            {
                listBox.Items.Add(WANOK.GetStringList((i + 1), ModelList[i].Name));
            }
            if (ModelList.Count < WANOK.MAX_COLORS) listBox.Items.Add(WANOK.ListBeginning);
        }

        // -------------------------------------------------------------------
        // GetListBox
        // -------------------------------------------------------------------

        public ListBox GetListBox()
        {
            return listBox;
        }

        // -------------------------------------------------------------------
        // UnselectAllLists
        // -------------------------------------------------------------------

        public void UnselectAllLists()
        {
            for (int i = 0; i < ListBoxes.Length; i++)
            {
                ListBoxes[i].ClearSelected();
            }
        }

        // -------------------------------------------------------------------
        // EditItem
        // -------------------------------------------------------------------

        public void EditItem()
        {
            SuperListDialog dialog = listBox.SelectedIndex < ModelList.Count ? 
                (SuperListDialog)Activator.CreateInstance(DialogKind, ModelList[listBox.SelectedIndex]) : 
                (SuperListDialog)Activator.CreateInstance(DialogKind, 
                            BindingFlags.CreateInstance |
                            BindingFlags.Public |
                            BindingFlags.Instance |
                            BindingFlags.OptionalParamBinding, null, new object[] { Type.Missing }, CultureInfo.CurrentCulture);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int index = listBox.SelectedIndex;
                if (index >= ModelList.Count && ModelList.Count < Max) listBox.Items.Add(WANOK.ListBeginning);
                if (index >= ModelList.Count) ModelList.Add(dialog.GetObject());
                else ModelList[index] = dialog.GetObject();
                listBox.Items.RemoveAt(index);
                listBox.Items.Insert(index, WANOK.GetStringList((index + 1), dialog.GetObject().Name));
            }
        }

        // -------------------------------------------------------------------
        // AddItem
        // -------------------------------------------------------------------

        public void AddItem()
        {
            if (ModelList.Count == Max) MessageBox.Show("Maximum reached.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else {
                SuperListDialog dialog = (SuperListDialog)Activator.CreateInstance(DialogKind,
                            BindingFlags.CreateInstance |
                            BindingFlags.Public |
                            BindingFlags.Instance |
                            BindingFlags.OptionalParamBinding, null, new object[] { Type.Missing }, CultureInfo.CurrentCulture);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    listBox.Items.Insert(listBox.SelectedIndex + 1, WANOK.GetStringList((listBox.SelectedIndex + 2), dialog.GetObject().Name));
                    ModelList.Insert(listBox.SelectedIndex + 1, dialog.GetObject());
                    if (ModelList.Count == WANOK.MAX_COLORS) listBox.Items.RemoveAt(listBox.Items.Count - 1);
                    for (int i = listBox.SelectedIndex + 2; i < ModelList.Count; i++)
                    {
                        listBox.Items[i] = WANOK.GetStringList(i+1, ModelList[i].Name);
                    }
                }
            }
        }

        // -------------------------------------------------------------------
        // DeleteItem
        // -------------------------------------------------------------------

        public void DeleteItem()
        {
            if (listBox.Items.Count == 2) MessageBox.Show("You need at least one element.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else {
                int index = listBox.SelectedIndex;
                listBox.Items.RemoveAt(index);
                ModelList.RemoveAt(index);
                for (int i = index; i < ModelList.Count; i++)
                {
                    listBox.Items[i] = WANOK.GetStringList(i + 1, ModelList[i].Name);
                }
                listBox.SelectedIndex = index;
            }
        }

        // -------------------------------------------------------------------
        // listBox_MouseDown
        // -------------------------------------------------------------------

        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBox.IndexFromPoint(e.X, e.Y);
                UnselectAllLists();
                listBox.SelectedIndex = index;
                if (listBox.SelectedIndex != -1)
                {
                    bool exists = listBox.SelectedIndex < ModelList.Count;
                    ItemEdit.Text = exists ? "Edit" : "New element";
                    ItemAdd.Enabled = exists;
                    ItemDelete.Enabled = exists;
                    contextMenuStrip.Show(listBox, e.Location);
                }
            }
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
        }

        // -------------------------------------------------------------------
        // ItemEdit_Click
        // -------------------------------------------------------------------

        private void ItemEdit_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        // -------------------------------------------------------------------
        // ItemAdd_Click
        // -------------------------------------------------------------------

        private void ItemAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        // -------------------------------------------------------------------
        // ItemDelete_Click
        // -------------------------------------------------------------------

        private void ItemDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        // -------------------------------------------------------------------
        // listBox_KeyDown
        // -------------------------------------------------------------------

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox.SelectedIndex != -1 && listBox.SelectedIndex < listBox.Items.Count - 1 && e.KeyCode == Keys.Delete)
            {
                DeleteItem();
            }
        }

        // -------------------------------------------------------------------
        // listBox_MouseEnter
        // -------------------------------------------------------------------

        private void listBox_MouseEnter(object sender, EventArgs e)
        {
            listBox.Focus();
        }
    }
}
