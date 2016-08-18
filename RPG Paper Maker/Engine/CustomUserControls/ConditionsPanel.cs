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
            public List<object> Condition;

            public FlatButtonCondition(List<object> condition)
            {
                Condition = condition;
            }
        }

        NTree<List<object>> Tree;

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
            BrowseNode(Tree);
            mainTableLayout.ColumnCount = mainTableLayout.ColumnStyles.Count;
        }

        private void BrowseNode(NTree<List<object>> node)
        {
            AddLabel("(");

            foreach (NTree<List<object>> child in node.GetChildren())
            {
                BrowseNode(child);
            }
            if (node.GetChildren().Count == 0)
            {
                AddButton(node.Data);
            }

            AddLabel(")");
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
            mainTableLayout.Controls.Add(label, mainTableLayout.ColumnStyles.Count, 0);
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

        // -------------------------------------------------------------------
        // AddButton
        // -------------------------------------------------------------------

        public void AddButton(List<object> condition)
        {
            FlatButtonCondition button = new FlatButtonCondition(condition);
            button.AutoSize = true;
            button.Text = condition == null ? "New condition" : Condition.ToString(condition);
            button.MouseDown += Button_MouseDown;
            mainTableLayout.Controls.Add(button, mainTableLayout.ColumnStyles.Count, 0);
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            List<object> condition = ((FlatButtonCondition)sender).Condition;
            DialogCondition dialog = new DialogCondition(condition == null ? Condition.DefaultObjects() : condition);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // if adding a new condition
                if (condition == null)
                {
                    ((FlatButtonCondition)sender).Condition = dialog.Result;
                }
                // if only editing
                else
                {
                    ((FlatButtonCondition)sender).Condition = dialog.Result;
                }

                GeneratePanel();
            }
        }
    }
}
