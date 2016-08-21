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
        public TilesetsDatas Model;
        public int Min, Max;
        public bool IsSelectedItemWhenLosingFocus = false;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public AddingListBox()
        {
            InitializeComponent();

            listBox.FormattingEnabled = false;
            listBox.LostFocus += ListBox_LostFocus;
        }

        // -------------------------------------------------------------------
        // InitializeListParameters
        // -------------------------------------------------------------------

        public void InitializeListParameters(bool select, TilesetsDatas model, List<SuperListItem> modelListComplete, List<int> modelListTileset, Type type, Type typeItem, int min, int max, MethodGetSuperItemById getById)
        {
            IsSelectedItemWhenLosingFocus = select;
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
        // SetName
        // -------------------------------------------------------------------

        public void SetName(string name)
        {
            ((SuperListItem)listBox.Items[listBox.SelectedIndex]).Name = name;
            listBox.Items[listBox.SelectedIndex] = listBox.SelectedItem;
        }

        private void ListBox_LostFocus(object sender, EventArgs e)
        {
            if (!IsSelectedItemWhenLosingFocus) listBox.SelectedIndex = -1;
        }
    }
}
