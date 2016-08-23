using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker.Engine.CustomUserControls
{
    public partial class TextBoxVariables : UserControl
    {
        public object[] Value;
        public List<object> Others;
        public Type DialogKind;
        public delegate string GetString(object[] args);
        public GetString MethodString;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public TextBoxVariables()
        {
            InitializeComponent();
            listBox1.Items.Add("");
            listBox1.LostFocus += ListBox1_LostFocus;
        }

        // -------------------------------------------------------------------
        // InitializeParameters
        // -------------------------------------------------------------------

        public void InitializeParameters(object[] value, List<object> others, Type type, GetString getString)
        {
            Value = value;
            Others = others;
            DialogKind = type;
            MethodString = getString;

            listBox1.Items[0] = MethodString(Value);
        }

        public void InitializeSwitch(int id)
        {
            InitializeParameters(new object[] { id }, null, typeof(DialogSwitches), WANOK.GetStringSwitch);
        }

        // -------------------------------------------------------------------
        // GetTextBox
        // -------------------------------------------------------------------

        public ListBox GetTextBox()
        {
            return listBox1;
        }

        // -------------------------------------------------------------------
        // OpenDialog
        // -------------------------------------------------------------------

        public void OpenDialog()
        {
            listBox1.SelectedIndex = 0;
            DialogVariable dialog = (DialogVariable)Activator.CreateInstance(DialogKind);
            dialog.InitializeParameters(Value, Others);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                object[] obj = dialog.GetObject();
                for (int i = 0; i < Value.Length; i++)
                {
                    Value[i] = obj[i];
                }
                listBox1.Items[0] = MethodString(Value);
            }
            listBox1.Focus();
        }

        // -------------------------------------------------------------------
        // Button_Click
        // -------------------------------------------------------------------

        private void Button_Click(object sender, EventArgs e)
        {
            OpenDialog();
        }

        // -------------------------------------------------------------------
        // listBox1_DoubleClick
        // -------------------------------------------------------------------

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenDialog();
        }

        private void ListBox1_LostFocus(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
        }
    }
}
