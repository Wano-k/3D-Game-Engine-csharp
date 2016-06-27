using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class TilesetPassage
    {
        public PassageKind PassableCollision;

        public TilesetPassage CreateCopy()
        {
            return new TilesetPassage();
        }
    }
}
