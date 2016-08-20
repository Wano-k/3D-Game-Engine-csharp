using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public class EventCommand
    {
        public EventCommandKind EventCommandKind;
        public List<object> Command;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public EventCommand() : this(EventCommandKind.None, null)
        {

        }

        public EventCommand(EventCommandKind eventCommandKind, List<object> command)
        {
            EventCommandKind = eventCommandKind;
            Command = command;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public EventCommand CreateCopy()
        {
            List<object> command = null;
            if (Command != null)
            {
                command = new List<object>();
                for (int i = 0; i < Command.Count; i++)
                {
                    command.Add(Command[i]);
                }
            }

            return new EventCommand(EventCommandKind, command);
        }

        // -------------------------------------------------------------------
        // ToString
        // -------------------------------------------------------------------

        public override string ToString()
        {
            if (EventCommandKind == EventCommandKind.None) return "";
            else return "Error: couldn't not convert to string. Please report it to Wanok.rpm@gmail.com";
        }
    }
}
