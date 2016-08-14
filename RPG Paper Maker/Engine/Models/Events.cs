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
        // 1: portion, 3: coords
        public Dictionary<int[], Dictionary<int[], SystemEvent>> CompleteList = new Dictionary<int[], Dictionary<int[], SystemEvent>>(new IntArrayComparer());


        // -------------------------------------------------------------------
        // Add
        // -------------------------------------------------------------------

        public void Add(int[] portion, int[] coords, SystemEvent ev)
        {
            if (!CompleteList.ContainsKey(portion)) CompleteList[portion] = new Dictionary<int[], SystemEvent>(new IntArrayComparer());
            CompleteList[portion][coords] = ev;
        }

        // -------------------------------------------------------------------
        // Count
        // -------------------------------------------------------------------

        public int Count()
        {
            int count = 0;
            foreach (Dictionary<int[], SystemEvent> entry in CompleteList.Values)
            {
                foreach (SystemEvent entry2 in entry.Values)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
