using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace RPG_Paper_Maker
{
    public partial class GraphicControl : UserControl
    {
        public GraphicPanel panel = new GraphicPanel();
        public SystemGraphic Graphic;

        public class GraphicPanel : Panel
        {
            public Pen BorderPen = new Pen(Color.LightGray);

            public GraphicPanel()
            {
                BorderPen.DashStyle = DashStyle.Dash;
                BorderPen.Width = 3;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                if (Focused) e.Graphics.DrawRectangle(BorderPen, new Rectangle(ClientRectangle.X + 5, ClientRectangle.Y + 5, ClientRectangle.Width - 11, ClientRectangle.Height - 11));
            }
        }

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public GraphicControl()
        {
            InitializeComponent();

            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(panel, 0, 1);

            // Event
            panel.Click += panel_Click;
            panel.DoubleClick += Panel_DoubleClick;
            panel.LostFocus += Panel_LostFocus;
        }

        // -------------------------------------------------------------------
        // InitializeListParameters
        // -------------------------------------------------------------------

        public void InitializeListParameters(SystemGraphic graphic)
        {
            Graphic = graphic;
        }

        // -------------------------------------------------------------------
        // EVENTS
        // -------------------------------------------------------------------

        private void panel_Click(object sender, EventArgs e)
        {
            panel.Focus();
            panel.Refresh();
        }

        private void Panel_DoubleClick(object sender, EventArgs e)
        {
            DialogPreviewGraphicSelectFrame dialog = new DialogPreviewGraphicSelectFrame(Graphic, OptionsKind.CharacterSelection);
            if (dialog.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void Panel_LostFocus(object sender, EventArgs e)
        {
            panel.Refresh();
        }
    }
}
