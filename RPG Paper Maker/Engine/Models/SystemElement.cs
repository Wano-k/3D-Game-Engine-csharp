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

        public SystemElement(int id) : this(id, WANOK.GetDefaultNames(), new SystemGraphic(GraphicKind.Icon))
        {

        }

        public SystemElement(int id, Dictionary<string, string> names, SystemGraphic icon)
        {
            Id = id;
            Names = names;
            Icon = icon;
            SetName();
        }

        public void SetName()
        {
            Name = Names[WANOK.CurrentLang];
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override SuperListItem CreateCopy()
        {
            return new SystemElement(Id, new Dictionary<string, string>(Names), Icon.CreateCopy());
        }

        // -------------------------------------------------------------------
        // GetDefaultElements
        // -------------------------------------------------------------------

        public static List<SystemElement> GetDefaultElements()
        {
            List<SystemElement> list = new List<SystemElement>();
            list.Add(new SystemElement(1, WANOK.GetDefaultNames("Fire"), new SystemGraphic("fire.png", true, GraphicKind.Icon)));
            list.Add(new SystemElement(2, WANOK.GetDefaultNames("Water"), new SystemGraphic("water.png", true, GraphicKind.Icon)));
            list.Add(new SystemElement(3, WANOK.GetDefaultNames("Grass"), new SystemGraphic("grass.png", true, GraphicKind.Icon)));
            list.Add(new SystemElement(4, WANOK.GetDefaultNames("Wind"), new SystemGraphic("wind.png", true, GraphicKind.Icon)));
            list.Add(new SystemElement(5, WANOK.GetDefaultNames("Light"), new SystemGraphic("light.png", true, GraphicKind.Icon)));
            list.Add(new SystemElement(6, WANOK.GetDefaultNames("Darkness"), new SystemGraphic("darkness.png", true, GraphicKind.Icon)));

            return list;
        }
    }
}
