using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public class GameDatas
    {
        public HeroesDatas Heroes;
        public SystemDatas System;
        public TilesetsDatas Tilesets;


        // -------------------------------------------------------------------
        // LoadDatas
        // -------------------------------------------------------------------

        public void LoadDatas()
        {
            Heroes = WANOK.LoadBinaryDatas<HeroesDatas>(WANOK.HeroesPath);
            System = WANOK.LoadBinaryDatas<SystemDatas>(WANOK.SystemPath);
            Tilesets = WANOK.LoadBinaryDatas<TilesetsDatas>(WANOK.TilesetsPath);
        }

        // -------------------------------------------------------------------
        // SaveDatas
        // -------------------------------------------------------------------

        public void SaveDatas()
        {
            WANOK.SaveBinaryDatas(Heroes, WANOK.HeroesPath);
            WANOK.SaveBinaryDatas(System, WANOK.SystemPath);
            WANOK.SaveBinaryDatas(Tilesets, WANOK.TilesetsPath);
        }
    }
}
