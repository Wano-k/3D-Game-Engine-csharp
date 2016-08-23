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

    public abstract class DialogVariable : Form
    {
        public abstract object[] GetObject();
        public abstract void InitializeParameters(object[] value, List<object> others);
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

    [Serializable]
    public class SuperListItemName : SuperListItem
    {
        public Dictionary<string, string> Names;

        public SuperListItemName(int id) : this(id, WANOK.GetDefaultNames())
        {

        }

        public SuperListItemName(int id, Dictionary<string, string> names)
        {
            Id = id;
            Names = names;
            SetName();
        }

        public void SetName()
        {
            Name = Names[WANOK.CurrentLang];
        }

        public override SuperListItem CreateCopy()
        {
            return new SuperListItemName(Id, new Dictionary<string, string>(Names));
        }
    }

    [Serializable]
    public class SuperListItemNameWithoutLang : SuperListItem
    {

        public SuperListItemNameWithoutLang(int id) : this(id, "")
        {

        }

        public SuperListItemNameWithoutLang(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override SuperListItem CreateCopy()
        {
            return new SuperListItemNameWithoutLang(Id, Name);
        }
    }

    [Serializable]
    public abstract class ComboxBoxSpecialTilesetItem : SuperListItem
    {
        public SystemGraphic Graphic;
    }
}
