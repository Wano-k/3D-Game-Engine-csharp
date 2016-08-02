﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class TilesetsDatas
    {
        public List<Tileset> Tilesets = new List<Tileset>();
        public List<SystemAutotile> Autotiles = new List<SystemAutotile>();
        public List<SystemRelief> Reliefs = new List<SystemRelief>();


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public TilesetsDatas()
        {
            Tilesets.Add(new Tileset(1, "Plains", new SystemGraphic("plains.png", true, GraphicKind.Tileset), new Collision(Collision.GetDefaultPassableCollision(8, 8)), new List<int>(new int[] { 1, 2 }), new List<int>(new int[] { 1, 2 })));
            for (int i = 1; i < 20; i++) Tilesets.Add(new Tileset(i + 1));
            Autotiles = SystemAutotile.GetDefaultAutotiles();
            Reliefs = SystemRelief.GetDefaultReliefs();
        }

        // -------------------------------------------------------------------
        // GetTilesetById
        // -------------------------------------------------------------------

        public Tileset GetTilesetById(int id)
        {
            if (id > Tilesets.Count) return new Tileset(-1);
            return Tilesets.Find(i => i.Id == id);
        }

        // -------------------------------------------------------------------
        // GetTilesetIndexById
        // -------------------------------------------------------------------

        public int GetTilesetIndexById(int id)
        {
            return Tilesets.IndexOf(GetTilesetById(id));
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
