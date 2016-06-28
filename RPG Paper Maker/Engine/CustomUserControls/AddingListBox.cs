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
    public partial class AddingListBox : UserControl
    {
        public List<SuperListItem> ModelList { get { return listBox.Items.Cast<SuperListItem>().ToList(); } }
        public Type TypeItem;
        public ListBox[] ListBoxes;
        public int Min, Max;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public AddingListBox()
        {
            InitializeComponent();

            listBox.FormattingEnabled = false;
        }

        // -------------------------------------------------------------------
        // InitializeListParameters
        // -------------------------------------------------------------------

        public void InitializeListParameters(ListBox[] list, List<SuperListItem> modelList, Type typeItem, int min, int max)
        {
            ListBoxes = list;
            TypeItem = typeItem;
            Min = min;
            Max = max;

            for (int i = 0; i < modelList.Count; i++)
            {
                listBox.Items.Add(modelList[i]);
            }
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
        // SetName
        // -------------------------------------------------------------------

        public void SetName(string name)
        {
            ((SuperListItem)listBox.Items[listBox.SelectedIndex]).Name = name;
            listBox.Items[listBox.SelectedIndex] = listBox.SelectedItem;
        }
        
        // -------------------------------------------------------------------
        // button_Click
        // -------------------------------------------------------------------

        private void button_Click(object sender, EventArgs e)
        {
            DialogAddingSpecialList dialog = new DialogAddingSpecialList(button.Text);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
            }
        }
    }
}
