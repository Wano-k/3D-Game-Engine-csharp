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
    public partial class MainPanel : UserControl
    {
        public MainPanel()
        {
            InitializeComponent();
            tableLayoutPanelRecentProject.RowStyles[0] = new RowStyle(SizeType.Absolute, 25);

            // Paint groupBox
            groupBox1.Paint += MainForm.PaintBorderGroupBox;
        }
    }
}
