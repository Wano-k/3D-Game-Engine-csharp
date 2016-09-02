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
        // 1: portion, 2: coords
        public Dictionary<int[], Dictionary<int[], SystemEvent>> CompleteList = new Dictionary<int[], Dictionary<int[], SystemEvent>>(new IntArrayComparer());


        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public Events CreateCopy()
        {
            Events events = new Events();
            foreach (KeyValuePair<int[], Dictionary<int[], SystemEvent>> entry in CompleteList)
            {
                events.CompleteList[entry.Key] = new Dictionary<int[], SystemEvent>(new IntArrayComparer());
                foreach (KeyValuePair<int[], SystemEvent> entry2 in entry.Value)
                {
                    events.CompleteList[entry.Key][entry2.Key] = entry2.Value.CreateCopy();
                }
            }

            return events;
        }

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
