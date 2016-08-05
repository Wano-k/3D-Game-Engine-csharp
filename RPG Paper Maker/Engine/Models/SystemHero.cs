using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemHero : SuperListItem
    {
        public static int MAX_HEROES = 9999;
        public Dictionary<string, string> Names;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public SystemHero(int id) : this(id, WANOK.GetDefaultNames())
        {

        }

        public SystemHero(int id, Dictionary<string, string> names)
        {
            Id = id;
            Names = names;
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
            return new SystemHero(Id, new Dictionary<string, string>(Names));
        }

        // -------------------------------------------------------------------
        // GetDefaultHeroes
        // -------------------------------------------------------------------

        public static List<SystemHero> GetDefaultHeroes()
        {
            List<SystemHero> list = new List<SystemHero>();
            list.Add(new SystemHero(1, WANOK.GetDefaultNames("Lucas")));
            list.Add(new SystemHero(2, WANOK.GetDefaultNames("Kate")));

            return list;
        }
    }
}
