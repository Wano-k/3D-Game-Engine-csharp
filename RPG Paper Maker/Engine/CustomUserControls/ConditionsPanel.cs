using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public partial class ConditionsPanel : UserControl
    {
        public class FlatButtonCondition : FlatButton
        {
            public NTree<List<object>> Node;
            public NTree<List<object>> ParentNode;

            public FlatButtonCondition(NTree<List<object>> node, NTree<List<object>> parentNode)
            {
                Node = node;
                ParentNode = parentNode;
            }
        }

        public class ComboBoxCondition : ComboBox
        {
            public NTree<List<object>> Node;
            public NTree<List<object>> ParentNode;

            public ComboBoxCondition(NTree<List<object>> node, NTree<List<object>> parentNode)
            {
                Node = node;
                ParentNode = parentNode;
            }
        }

        public NTree<List<object>> Tree;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public ConditionsPanel()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------------------
        // InitializeListParameters
        // -------------------------------------------------------------------

        public void InitializeListParameters()
        {
            Tree = new NTree<List<object>>(null);
            GeneratePanel();
        }

        // -------------------------------------------------------------------
        // GeneratePanel
        // -------------------------------------------------------------------

        private void GeneratePanel()
        {
            mainTableLayout.Controls.Clear();
            mainTableLayout.ColumnStyles.Clear();
            AddLabel("If");
            AddLabel("(");
            BrowseNode(Tree, new NTree<List<object>>(null));
            AddLabel(")");
            mainTableLayout.ColumnCount = mainTableLayout.ColumnStyles.Count;
        }

        private void BrowseNode(NTree<List<object>> node, NTree<List<object>> parent)
        {
            if (node.GetChildren().Count == 0)
            {
                AddButton(node, parent);

                if (parent.Data != null && (!parent.IsLastChild(node) || (int)parent.Data[0] == 0))
                {
                    AddCombobox(node, parent);
                }
            }
            else
            {
                AddLabel("(");
                foreach (NTree<List<object>> child in node.GetChildren())
                {
                    BrowseNode(child, node);
                }
                AddLabel(")");
            }
        }

        // -------------------------------------------------------------------
        // AddLabel
        // -------------------------------------------------------------------

        public void AddLabel(string s)
        {
            Label label = new Label();
            label.Anchor = AnchorStyles.Left;
            label.Text = s;
            label.AutoSize = true;
            label.Margin = new Padding(0);
            label.Padding = new Padding(0);
            mainTableLayout.Controls.Add(label, mainTableLayout.ColumnStyles.Count, 0);
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

        // -------------------------------------------------------------------
        // AddButton
        // -------------------------------------------------------------------

        public void AddButton(NTree<List<object>> node, NTree<List<object>> parentNode)
        {
            FlatButtonCondition button = new FlatButtonCondition(node, parentNode);
            button.AutoSize = true;
            button.Text = node.Data == null ? "New condition" : Condition.ToString(node.Data);
            button.MouseDown += Button_MouseDown;
            mainTableLayout.Controls.Add(button, mainTableLayout.ColumnStyles.Count, 0);
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

        // -------------------------------------------------------------------
        // AddCombobox
        // -------------------------------------------------------------------

        public void AddCombobox(NTree<List<object>> node, NTree<List<object>> parentNode)
        {
            ComboBoxCondition comboBox = new ComboBoxCondition(node, parentNode);
            comboBox.Items.Add("");
            comboBox.Items.Add("And");
            comboBox.Items.Add("Or");
            comboBox.AutoSize = true;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Size = new Size(45, 20);
            comboBox.Anchor = AnchorStyles.Left;
            comboBox.SelectedIndex = (int)parentNode.Data[0];
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            mainTableLayout.Controls.Add(comboBox, mainTableLayout.ColumnStyles.Count, 0);
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

        // -------------------------------------------------------------------
        // TreeContainsBool
        // -------------------------------------------------------------------

        public NTree<List<object>> TreeContainsBool(NTree<List<object>> node, int boo)
        {
            NTree<List<object>> res = null;

            foreach (NTree<List<object>> child in node.GetChildren())
            {
                if (node.Data != null && node.Data.Count == 1 && (int)node.Data[0] == boo) return node;
                res = TreeContainsBool(child, boo);
            }

            return res;
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            List<object> condition = ((FlatButtonCondition)sender).Node.Data;
            DialogCondition dialog = new DialogCondition(condition == null ? Condition.DefaultObjects() : condition);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // if adding a new condition
                if (condition == null)
                {
                    ((FlatButtonCondition)sender).Node.Data = new List<object>(new object[] { 0 });
                    ((FlatButtonCondition)sender).Node.AddChild(dialog.Result);
                }
                // if only editing
                else
                {
                    ((FlatButtonCondition)sender).Node.Data = dialog.Result;
                }

                GeneratePanel();
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int previousSelecteIndex = (int)((ComboBoxCondition)sender).ParentNode.Data[0];
            int newSelecteIndex = ((ComboBox)sender).SelectedIndex;
            if (previousSelecteIndex != newSelecteIndex)
            {
                //((ComboBoxCondition)sender).ParentNode.Data = new List<object>(new object[] { newSelecteIndex });
                // Adding new button
                if (previousSelecteIndex == 0)
                {
                    NTree<List<object>> node = TreeContainsBool(Tree, newSelecteIndex);
                    if (node != null)
                    {
                        ((ComboBoxCondition)sender).ParentNode.Data = null;
                        ((ComboBoxCondition)sender).ParentNode = node;
                        node.AddChild(null);
                    }
                    else
                    {
                        ((ComboBoxCondition)sender).ParentNode.Data = new List<object>(new object[] { newSelecteIndex });
                        ((ComboBoxCondition)sender).ParentNode.AddChild(null);

                    }
                }

                GeneratePanel();
            }
        }
    }
}
