using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class TilesetsDatas
    {
        public List<SystemTileset> TilesetsList = new List<SystemTileset>();
        public List<SystemAutotile> Autotiles = new List<SystemAutotile>();
        public List<SystemRelief> Reliefs = new List<SystemRelief>();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public TilesetsDatas()
        {
            TilesetsList = SystemTileset.GetDefaultTilesets();
            Autotiles = SystemAutotile.GetDefaultAutotiles();
            Reliefs = SystemRelief.GetDefaultReliefs();
        }

        // -------------------------------------------------------------------
        // GetTilesetById
        // -------------------------------------------------------------------

        public SystemTileset GetTilesetById(int id)
        {
            if (id > TilesetsList.Count) return new SystemTileset(-1);
            return TilesetsList.Find(i => i.Id == id);
        }

        // -------------------------------------------------------------------
        // GetTilesetIndexById
        // -------------------------------------------------------------------

        public int GetTilesetIndexById(int id)
        {
            return TilesetsList.IndexOf(GetTilesetById(id));
        }

        // -------------------------------------------------------------------
        // GetAutotileById
        // -------------------------------------------------------------------

        public SystemAutotile GetAutotileById(int id)
        {
            if (id == -1 || id > Autotiles.Count) return new SystemAutotile(-1);
            return Autotiles.Find(i => i.Id == id);
        }

        // -------------------------------------------------------------------
        // GetAutotileIndexById
        // -------------------------------------------------------------------

        public int GetAutotileIndexById(int id)
        {
            return Autotiles.IndexOf(GetAutotileById(id));
        }

        // -------------------------------------------------------------------
        // GetReliefById
        // -------------------------------------------------------------------

        public SystemRelief GetReliefById(int id)
        {
            if (id == -1 || id > Reliefs.Count) return new SystemRelief(-1);
            return Reliefs.Find(i => i.Id == id);
        }

        // -------------------------------------------------------------------
        // GetReliefIndexById
        // -------------------------------------------------------------------

        public int GetReliefIndexById(int id)
        {
            return Reliefs.IndexOf(GetReliefById(id));
        }
    }
}
