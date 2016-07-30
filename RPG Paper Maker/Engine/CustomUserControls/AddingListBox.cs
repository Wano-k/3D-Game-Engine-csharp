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
        public List<int> ModelListTileset;
        public List<SuperListItem> ModelListComplete;
        public Type DialogKind;
        public Type TypeItem;
        public ListBox[] ListBoxes;
        public SystemDatas Model;
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

        public void InitializeListParameters(SystemDatas model, ListBox[] list, List<SuperListItem> modelListComplete, List<int> modelListTileset, Type type, Type typeItem, int min, int max, MethodGetSuperItemById getById)
        {
            ListBoxes = list;
            ModelListComplete = modelListComplete;
            ModelListTileset = modelListTileset;
            DialogKind = type;
            TypeItem = typeItem;
            Model = model;
            Min = min;
            Max = max;

            listBox.Items.Clear();
            for (int i = 0; i < modelListTileset.Count; i++)
            {
                listBox.Items.Add(getById(modelListTileset[i]));
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
    }
}
