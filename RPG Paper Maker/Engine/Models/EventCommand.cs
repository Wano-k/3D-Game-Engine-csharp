using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    public class EventCommand
    {
        public EventCommandKind Id;
        public List<object> Command;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public EventCommand() : this(EventCommandKind.None, null)
        {

        }

        public EventCommand(EventCommandKind id, List<object> command)
        {
            Id = id;
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

            return new EventCommand(Id, command);
        }

        // -------------------------------------------------------------------
        // ToString
        // -------------------------------------------------------------------

        public override string ToString()
        {
            switch (Id)
            {
                case EventCommandKind.None:
                    return "";
                default:
                    return "Error: couldn't not convert to string. Please report it to Wanok.rpm@gmail.com";
            }
        }
    }
}
