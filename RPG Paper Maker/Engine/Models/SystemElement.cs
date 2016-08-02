using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemElement : SuperListItem
    {
        public static int MAX_ELEMENTS = 9999;
        public Dictionary<string, string> Names;
        public SystemGraphic Icon;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemElement(int id, string lang) : this(id, WANOK.GetDefaultNames(), new SystemGraphic(GraphicKind.Icon), lang)
        {

        }

        public SystemElement(int id, Dictionary<string, string> names, SystemGraphic icon, string lang)
        {
            Id = id;
            Names = names;
            Icon = icon;
            SetName(lang);
        }

        public void SetName(string lang)
        {
            Name = Names[lang];
        }

        public override SuperListItem CreateCopy()
        {
            throw new NotImplementedException();
        }
    }
}
