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
    public partial class DialogInputsManager : Form
    {
        protected DialogInputsManagerControl Control;
        protected BindingSource ViewModelBindingSource = new BindingSource();

        public DialogInputsManager()
        {
            InitializeComponent();

            Control = new DialogInputsManagerControl(new KeyboardAssign(WANOK.Settings.KeyboardAssign.KeyboardEditorAssign, WANOK.Settings.KeyboardAssign.KeyboardGameAssign));

            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;
            groupBox2.Paint += MainForm.PaintBorderGroupBox;

            // Layout
            int nb = Control.Model.KeyboardEditorAssign.Count;
            TableLayoutPanel TableLayoutMapEditor = new TableLayoutPanel();
            TableLayoutMapEditor.RowCount = nb;
            TableLayoutMapEditor.ColumnCount = 1;
            TableLayoutMapEditor.Dock = DockStyle.Fill;
            TableLayoutMapEditor.AutoSize = true;

            int i = 0;
            foreach (KeyValuePair<string, Microsoft.Xna.Framework.Input.Keys> entry in Control.Model.KeyboardEditorAssign)
            {
                // Label
                Label label = new Label();
                label.Text = entry.Key;
                label.AutoSize = false;
                label.Dock = DockStyle.Fill;
                label.TextAlign = ContentAlignment.MiddleLeft;
                // Ctrl
                Label ctrl = new Label();
                ctrl.Text = entry.Value.ToString();
                ctrl.AutoSize = false;
                ctrl.Dock = DockStyle.Fill;
                ctrl.TextAlign = ContentAlignment.MiddleRight;
                // Button
                Button button = new Button();
                button.Text = "...";
                button.AutoSize = false;
                button.BackColor = SystemColors.Control;
                button.Dock = DockStyle.Fill;
                button.Click += delegate (object sender, EventArgs e) {
                    DialogInputsSelect dialog = new DialogInputsSelect(entry.Key);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Control.SetKeyEditor(entry.Key.ToString(), dialog.Key);
                        ctrl.Text = dialog.Key.ToString();
                    }
                };
                // Main panel
                TableLayoutPanel panel = new TableLayoutPanel();
                panel.Dock = DockStyle.Fill;
                panel.RowCount = 1;
                panel.ColumnCount = 3;
                panel.Controls.Add(label, 0, 0);
                panel.Controls.Add(ctrl, 1, 0);
                panel.Controls.Add(button, 2, 0);
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));

                if (i % 2 == 0) panel.BackColor = Color.Silver;
                else panel.BackColor = Color.DimGray;
                panel.Dock = DockStyle.Fill;
                panel.Margin = new Padding(0);
                TableLayoutMapEditor.Controls.Add(panel, 0, i);
                i++;
            }

            // Row style
            for (i = 0; i < nb; i++)
            {
                var lol = TableLayoutMapEditor.RowCount;
                TableLayoutMapEditor.RowStyles.Add(new RowStyle(SizeType.Absolute,30));
            }
            groupBox1.Controls.Add(TableLayoutMapEditor);

            TableLayoutPanel TableLayoutGame = new TableLayoutPanel();
            TableLayoutGame.RowCount = nb;
            TableLayoutGame.ColumnCount = 1;
            TableLayoutGame.Dock = DockStyle.Fill;
            TableLayoutGame.AutoSize = true;
            groupBox2.Controls.Add(TableLayoutGame);

            tableLayoutPanel1.AutoScrollMinSize = tableLayoutPanel1.GetPreferredSize(new Size(1, 1)); // Adjust for scrollbar
            ok.Select();
        }

        // -------------------------------------------------------------------
        // ok_Click
        // -------------------------------------------------------------------

        private void ok_Click(object sender, EventArgs e)
        {
            Control.SaveDatas();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
