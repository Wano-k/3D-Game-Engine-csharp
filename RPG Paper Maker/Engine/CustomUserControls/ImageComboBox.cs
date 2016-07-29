using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public class ImageComboBox : ComboBox
    {
        public ImageComboBox()
        {
            DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void FillComboBox(List<int> list, MethodGetSuperItemById getById, MethodGetIndexById getIndexById, int currentId)
        {
            for (int i = 0; i < list.Count; i++)
            {
                ComboxBoxSpecialTilesetItem item = getById(list[i]);
                Items.Add(new DropDownItem(WANOK.GetStringComboBox(item.Id, item.Name), item.Graphic.LoadImage()));
            }
            int id = getIndexById(currentId);
            if (Items.Count > 0)
            {

                if (id >= 0 && id < Items.Count) SelectedIndex = id;
                else
                {
                    SelectedIndex = 0;
                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();

            e.DrawFocusRectangle();

            if (e.Index >= 0 && e.Index < Items.Count)
            {
                DropDownItem item = (DropDownItem)Items[e.Index];

                e.Graphics.DrawImage(item.Image, e.Bounds.Left, e.Bounds.Top);

                e.Graphics.DrawString(item.Value, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + item.Image.Width, e.Bounds.Top + 2);
            }

            base.OnDrawItem(e);
        }
    }

    public sealed class DropDownItem
    {
        public string Value { get; set; }

        public Image Image { get; set; }

        public DropDownItem(string val, Image image)
        {
            Value = val;
            Image = new Bitmap(image, 16, 16);
            using (Graphics g = Graphics.FromImage(Image))
            {
                using (Brush b = new SolidBrush(Color.FromName(val)))
                {
                    g.DrawRectangle(Pens.White, 0, 0, Image.Width, Image.Height);
                    g.FillRectangle(b, 1, 1, Image.Width - 1, Image.Height - 1);
                }
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
