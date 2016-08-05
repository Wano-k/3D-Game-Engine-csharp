using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class HeroesDatas
    {
        public List<SystemHero> HeroesList = new List<SystemHero>();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public HeroesDatas()
        {
            HeroesList = SystemHero.GetDefaultHeroes();
        }

        // -------------------------------------------------------------------
        // GetHeroById
        // -------------------------------------------------------------------

        public SystemHero GetTilesetById(int id)
        {
            if (id > HeroesList.Count) return new SystemHero(-1);
            return HeroesList.Find(i => i.Id == id);
        }

        // -------------------------------------------------------------------
        // GetHeroIndexById
        // -------------------------------------------------------------------

        public int GetHeroIndexById(int id)
        {
            return HeroesList.IndexOf(GetTilesetById(id));
        }
    }
}
