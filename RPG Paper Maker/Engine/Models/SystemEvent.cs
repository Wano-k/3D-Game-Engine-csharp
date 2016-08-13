using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class SystemEvent
    {
        public SystemGraphic GraphicApparence;


        // -------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------

        public SystemEvent() : this(new SystemGraphic(GraphicKind.Character, new object[] { 4, 0 }))
        {
            
        }

        public SystemEvent(SystemGraphic apparence)
        {
            GraphicApparence = apparence;
        }
    }
}
