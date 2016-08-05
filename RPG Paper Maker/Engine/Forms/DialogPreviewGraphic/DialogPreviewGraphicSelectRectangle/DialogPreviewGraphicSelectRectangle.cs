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
    public partial class DialogPreviewGraphicSelectRectangle : DialogPreviewGraphic
    {
        public DialogPreviewGraphicSelectRectangle(SystemGraphic graphic, object[] options = null) : base(graphic) 
        {

            // Show options
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 20);
            tableLayoutPanel3.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 60);
            tableLayoutPanel3.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 20);
        }
    }
}
