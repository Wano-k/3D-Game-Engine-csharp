using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public class GameDatas
    {
        public SystemDatas System;
        public TilesetsDatas Tilesets;


        // -------------------------------------------------------------------
        // LoadDatas
        // -------------------------------------------------------------------

        public void LoadDatas()
        {
            System = WANOK.LoadBinaryDatas<SystemDatas>(WANOK.SystemPath);
            Tilesets = WANOK.LoadBinaryDatas<TilesetsDatas>(WANOK.TilesetsPath);
        }

        // -------------------------------------------------------------------
        // SaveDatas
        // -------------------------------------------------------------------

        public void SaveDatas()
        {
            WANOK.SaveBinaryDatas(System, WANOK.SystemPath);
            WANOK.SaveBinaryDatas(Tilesets, WANOK.TilesetsPath);
        }
    }
}
