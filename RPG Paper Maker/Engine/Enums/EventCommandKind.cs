using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public class EventCommandKind
    {
        private string Name;
    
        public static EventCommandKind None = new EventCommandKind("");
        public static EventCommandKind DisplayMessage = new EventCommandKind("Display a message...");
        public static EventCommandKind DisplayChoice = new EventCommandKind("Display a choice...");
        public static EventCommandKind EnterNumber = new EventCommandKind("Enter a number...");
        public static EventCommandKind DisplayOptions = new EventCommandKind("Display options...");
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

        public Form GetDialog()
        {
            if (this == Conditions) return new DialogCondition();

            return null;
        }
    }
}
