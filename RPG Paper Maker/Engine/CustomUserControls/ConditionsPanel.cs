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
            public static Color NoneColor = Color.FromArgb(250, 175, 160);
            public static Color FullColor = Color.FromArgb(205, 215, 245);

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
            public string Index;

            public ComboBoxCondition(NTree<List<object>> node, NTree<List<object>> parentNode, string index)
            {
                Node = node;
                ParentNode = parentNode;
                Index = index;
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
            Tree = new NTree<List<object>>(new List<object>(new object[] { "" }));
            Tree.AddChildData(null);
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
            BrowseNode(Tree, new NTree<List<object>>(null), Tree.GetLastNode());
            AddLabel(")");
            mainTableLayout.ColumnCount = mainTableLayout.ColumnStyles.Count;
        }

        private void BrowseNode(NTree<List<object>> node, NTree<List<object>> parent, NTree<List<object>> lastNode)
        {
            if (node.GetChildren().Count > 1) AddLabel("(");
            foreach (NTree<List<object>> child in node.GetChildren())
            {
                BrowseNode(child, node, lastNode);
            }
            if (node.GetChildren().Count > 1) AddLabel(")");

            if (node.GetChildren().Count == 0)
            {
                // Button
                AddButton(node, parent);

                // ComboBox
                var lastChild = parent.IsLastChild(node);
                if (node.Data != null && lastChild) AddCombobox(node, parent, "", lastNode);
                else if (!lastChild) AddCombobox(node, parent, (string)parent.Data[0], lastNode);
            }
            else
            {
                if (parent.Data != null && parent.Data.Count == 1 && (string)parent.Data[0] != "" && !parent.IsLastChild(node)) AddCombobox(node, parent, (string)parent.Data[0], lastNode);
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
            button.Text = node.Data == null ? "New" : Condition.ToString(node.Data);
            button.BackColor = node.Data == null ? FlatButtonCondition.NoneColor : FlatButtonCondition.FullColor;
            button.Size = new Size(0, 0);
            button.MouseDown += Button_MouseDown;
            mainTableLayout.Controls.Add(button, mainTableLayout.ColumnStyles.Count, 0);
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

        // -------------------------------------------------------------------
        // AddCombobox
        // -------------------------------------------------------------------

        public void AddCombobox(NTree<List<object>> node, NTree<List<object>> parentNode, string selected, NTree<List<object>> lastNode)
        {
            ComboBoxCondition comboBox = new ComboBoxCondition(node, parentNode, selected);
            if (selected != "")
            {
                comboBox.Items.Add(selected);
            }
            else
            {
                comboBox.Items.Add("");
                if (node == lastNode)
                {
                    comboBox.Items.Add("And");
                    comboBox.Items.Add("Or");
                }
                else comboBox.Items.Add((string)parentNode.Data[0]);
            }
            comboBox.AutoSize = true;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Size = new Size(45, 20);
            comboBox.Anchor = AnchorStyles.Left;
            comboBox.SelectedItem = selected;
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            mainTableLayout.Controls.Add(comboBox, mainTableLayout.ColumnStyles.Count, 0);
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

        // -------------------------------------------------------------------
        // TreeContainsAndOr
        // -------------------------------------------------------------------

        public bool TreeContainsAndOr(NTree<List<object>> node, string i)
        {
            foreach (NTree<List<object>> child in node.GetChildren())
            {
                if (node.Data != null && node.Data.Count == 1 && (string)node.Data[0] == i) return true;
                if (TreeContainsAndOr(child, i)) return true;
            }

            return false;
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
                ((FlatButtonCondition)sender).Node.Data = dialog.Result;

                GeneratePanel();
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string previousSelecteIndex = ((ComboBoxCondition)sender).Index;
            string newSelectedIndex = ((ComboBox)sender).SelectedItem.ToString();

            if (previousSelecteIndex != newSelectedIndex)
            {
                if ((string)Tree.Data[0] == newSelectedIndex)
                {
                    Tree.AddChildData(null);
                }
                else if ((string)Tree.Data[0] == "")
                {
                    ((ComboBoxCondition)sender).ParentNode.Data = new List<object>(new object[] { newSelectedIndex });
                    ((ComboBoxCondition)sender).ParentNode.AddChildData(null);
                }
                else
                {
                    if (TreeContainsAndOr(Tree, newSelectedIndex))
                    {
                        if (((ComboBoxCondition)sender).ParentNode.Data.Count == 1 && (string)((ComboBoxCondition)sender).ParentNode.Data[0] == newSelectedIndex)
                        {
                            ((ComboBoxCondition)sender).ParentNode.AddChildData(null);
                        }
                        else
                        {
                            List<object> previousData = ((ComboBoxCondition)sender).Node.Data;
                            ((ComboBoxCondition)sender).Node.Data = new List<object>(new object[] { newSelectedIndex });
                            ((ComboBoxCondition)sender).Node.AddChildData(previousData);
                            ((ComboBoxCondition)sender).Node.AddChildData(null);
                        }
                    }
                    else
                    {
                        NTree<List<object>> previousTree = Tree;
                        Tree = new NTree<List<object>>(new List<object>(new object[] { newSelectedIndex }));
                        Tree.AddChild(previousTree);
                        Tree.AddChildData(null);
                    }
                }
                
                GeneratePanel();
            }
        }
    }
}
