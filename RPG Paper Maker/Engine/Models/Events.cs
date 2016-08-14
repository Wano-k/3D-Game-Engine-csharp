using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class Events
    {
        // 1: portion, 2: graphic, 3: coords
        public Dictionary<int[], Dictionary<SystemGraphic, Dictionary<int[], SystemEvent>>> Sprites = new Dictionary<int[], Dictionary<SystemGraphic, Dictionary<int[], SystemEvent>>>(new IntArrayComparer());

        public void AddSprite(int[] portion, SystemGraphic graphic, int[] coords, SystemEvent ev)
        {
            if (!Sprites.ContainsKey(portion)) Sprites[portion] = new Dictionary<SystemGraphic, Dictionary<int[], SystemEvent>>();
            if (!Sprites[portion].ContainsKey(graphic)) Sprites[portion][graphic] = new Dictionary<int[], SystemEvent>(new IntArrayComparer());
            Sprites[portion][graphic][coords] = ev;
        }
    }
}
