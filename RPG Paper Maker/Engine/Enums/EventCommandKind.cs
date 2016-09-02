using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    [Serializable]
    public class EventCommandKind
    {
        private string Name;
    
        public static EventCommandKind None = new EventCommandKind("");
        public static EventCommandKind DisplayMessage = new EventCommandKind("Show message...");
        public static EventCommandKind DisplayChoice = new EventCommandKind("Show choices...");
        public static EventCommandKind EnterNumber = new EventCommandKind("Input number...");
        public static EventCommandKind DisplayOptions = new EventCommandKind("Change window options...");
        public static EventCommandKind ChangeSwitches = new EventCommandKind("Change switches...");
        public static EventCommandKind Conditions = new EventCommandKind("Conditions...");

        private EventCommandKind(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }


        public static List<EventCommandKind> GetValues()
        {
            List<EventCommandKind> list = new List<EventCommandKind>();
            System.Reflection.FieldInfo[] fields = typeof(EventCommandKind).GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                list.Add((EventCommandKind)fields[i].GetValue(null));
            }

            return list;
        }

        public Form GetDialog(EventCommand eventCommand = null)
        {
            if (this == Conditions) return new DialogConditions(eventCommand == null ? new EventCommandConditions() : (EventCommandConditions)eventCommand);
            else if (this == ChangeSwitches) return new DialogChangeSwitches(eventCommand == null ? new EventCommandOther(ChangeSwitches, new List<object>(new object[] { 0, 1, 0 })) : (EventCommandOther)eventCommand);

            return null;
        }
    }
}
