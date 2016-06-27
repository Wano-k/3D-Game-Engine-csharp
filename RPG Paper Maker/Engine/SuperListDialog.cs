using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public abstract class SuperListDialog : Form
    {
        public abstract SuperListItem GetObject();
    }

    [Serializable]
    public abstract class SuperListItem
    {
        public int Id;
        public string Name = "";

        public abstract SuperListItem CreateCopy();

        public override string ToString()
        {
            return WANOK.GetStringList(Id, Name);
        }
    }
}
