using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    class Mountains
    {
        public Dictionary<int, MountainsGroup> Groups = new Dictionary<int, MountainsGroup>();

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public Mountains CreateCopy()
        {
            Mountains newMountains = new Mountains();
            foreach (int id in Groups.Keys)
            {
                newMountains.Groups[id] = Groups[id].CreateCopy();
            }

            return newMountains;
        }

        // -------------------------------------------------------------------
        // IsEmpty
        // -------------------------------------------------------------------

        public bool IsEmpty()
        {
            return Groups.Count == 0;
        }

        // -------------------------------------------------------------------
        // ContainsInGroup
        // -------------------------------------------------------------------

        public object[] ContainsInGroup(int[] coords)
        {
            foreach (int id in Groups.Keys)
            {
                if (Groups[id].Tiles.ContainsKey(coords)) return new object[] { id, Groups[id].Tiles[coords] };
            }

            return null;
        }

        // -------------------------------------------------------------------
        // Add
        // -------------------------------------------------------------------

        public void Add(int[] coords, int id, Mountain mountain, int height, bool update = true)
        {
            if (!Groups.ContainsKey(id)) Groups[id] = new MountainsGroup();
            if (!Groups[id].Tiles.ContainsKey(coords))
            {
                Groups[id].Tiles[coords] = mountain.CreatePartialCopy();
                UpdateAround(coords[0], coords[1], coords[2], coords[3], height, update);
            }
        }

        // -------------------------------------------------------------------
        // Remove
        // -------------------------------------------------------------------

        public void Remove(int[] coords, int id, int height, bool update = true)
        {
            if (Groups.ContainsKey(id) && Groups[id].Tiles.ContainsKey(coords))
            {
                Groups[id].Tiles.Remove(coords);
                UpdateAround(coords[0], coords[1], coords[2], coords[3], height, update);
            }
        }

        // -------------------------------------------------------------------
        // UpdateAround
        // -------------------------------------------------------------------

        public void UpdateAround(int x, int y1, int y2, int z, int height, bool update)
        {
            int[] portion = MapEditor.Control.GetPortion(x, z); // portion where you are setting autotile
            for (int X = x - 1; X <= x + 1; X++)
            {
                for (int Z = z - 1; Z <= z + 1; Z++)
                {
                    int[] coords = new int[] { X, y1, y2, Z };
                    Mountain mountainAround = TileOnWhatever(coords, height);
                    if (mountainAround != null)
                    {
                        if (update) mountainAround.Update(this, coords, portion, height);
                        else
                        {
                            int[] newPortion = MapEditor.Control.GetPortion(X, Z);
                            if (WANOK.IsInPortions(newPortion))
                            {
                                MapEditor.Control.AddPortionsMountainToUpdate(newPortion);
                                WANOK.AddPortionsToAddCancel(MapEditor.Control.Map.MapInfos.RealMapName, MapEditor.Control.GetGlobalPortion(newPortion));
                            }
                            else mountainAround.Update(this, coords, portion, height);
                        }
                    }
                }
            }
        }

        // -------------------------------------------------------------------
        // TILES
        // -------------------------------------------------------------------

        public Mountain TileOnLeft(int[] coords, int[] portion, int height)
        {
            return TileOnWhatever(new int[] { coords[0] - 1, coords[1], coords[2], coords[3] }, height);
        }

        public Mountain TileOnRight(int[] coords, int[] portion, int height)
        {
            return TileOnWhatever(new int[] { coords[0] + 1, coords[1], coords[2], coords[3] }, height);
        }

        public Mountain TileOnTop(int[] coords, int[] portion, int height)
        {
            return TileOnWhatever(new int[] { coords[0], coords[1], coords[2], coords[3] - 1 }, height);
        }

        public Mountain TileOnBottom(int[] coords, int[] portion, int height)
        {
            return TileOnWhatever(new int[] { coords[0], coords[1], coords[2], coords[3] + 1 }, height);
        }

        public Mountain TileOnWhatever(int[] coords, int height)
        {
            int[] portion = MapEditor.Control.GetPortion(coords[0], coords[3]);
            if (MapEditor.Control.Map.Portions.ContainsKey(portion))
            {
                if (MapEditor.Control.Map.Portions[portion] != null && MapEditor.Control.Map.Portions[portion].Mountains.ContainsKey(height))
                {
                    foreach(MountainsGroup mountains in MapEditor.Control.Map.Portions[portion].Mountains[height].Groups.Values)
                    {
                        if (mountains.Tiles.ContainsKey(coords)) return mountains.Tiles[coords];
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        // -------------------------------------------------------------------
        // GenMountains
        // -------------------------------------------------------------------

        public void GenMountains(GraphicsDevice device)
        {
            foreach (int id in Groups.Keys)
            {
                Groups[id].GenMountains(device, id);
            }
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        public void Draw(GraphicsDevice device, AlphaTestEffect effect)
        {
            foreach (int id in Groups.Keys)
            {
                Groups[id].Draw(device, effect, id);
            }
        }

        // -------------------------------------------------------------------
        // DisposeBuffers
        // -------------------------------------------------------------------

        public void DisposeBuffers(GraphicsDevice device, bool nullable = true)
        {
            foreach (int id in Groups.Keys)
            {
                Groups[id].DisposeBuffers(device, nullable);
            }
        }
    }
}