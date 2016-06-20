using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    class SystemDatas
    {
        public string StartMapName = "MAP0001";
        public int[] StartPosition = new int[] { 12, 0, 12 };

        public void NoStart()
        {
            StartMapName = "";
            StartPosition = new int[] { 0, 0, 0 };
        }
    }
}
