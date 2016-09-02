using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    [Serializable]
    public abstract class EventCommand
    {
        public EventCommandKind EventCommandKind;

        public abstract EventCommand CreateCopy();
    }

    [Serializable]
    public class EventCommandConditions : EventCommand
    {
        public NTree<List<object>> Tree;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public EventCommandConditions() : this(GetDefaultTreeConditions())
        {

        }

        public EventCommandConditions(NTree<List<object>> tree)
        {
            EventCommandKind = EventCommandKind.None;
            Tree = tree;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override EventCommand CreateCopy()
        {
            NTree<List<object>> tree = new NTree<List<object>>(new List<object>(new object[] { Tree.Data[0] }));
            CopyTreeNode(tree, Tree);

            return new EventCommandConditions(tree);
        }

        // -------------------------------------------------------------------
        // CopyTreeNode
        // -------------------------------------------------------------------

        public void CopyTreeNode(NTree<List<object>> tree, NTree<List<object>> treeToCopy)
        {
            foreach (NTree<List<object>> childToCopy in treeToCopy.Children)
            {
                List<object> command = null;
                if (childToCopy.Data != null)
                {
                    command = new List<object>();
                    for (int i = 0; i < childToCopy.Data.Count; i++)
                    {
                        command.Add(childToCopy.Data[i]);
                    }
                }
                CopyTreeNode(tree.AddChildData(command), childToCopy);
            }
        }

        // -------------------------------------------------------------------
        // GetDefaultTreeConditions
        // -------------------------------------------------------------------

        public static NTree<List<object>> GetDefaultTreeConditions()
        {
            NTree<List<object>> tree = new NTree<List<object>>(new List<object>(new object[] { "" }));
            tree.AddChildData(null);

            return tree;
        }

        // -------------------------------------------------------------------
        // ToString
        // -------------------------------------------------------------------

        public override string ToString()
        {
            return "";
        }
    }

    [Serializable]
    public class EventCommandOther : EventCommand
    {
        public List<object> Command;


        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public EventCommandOther() : this(EventCommandKind.None, null)
        {

        }

        public EventCommandOther(EventCommandKind eventCommandKind, List<object> command)
        {
            EventCommandKind = eventCommandKind;
            Command = command;
        }

        // -------------------------------------------------------------------
        // CreateCopy
        // -------------------------------------------------------------------

        public override EventCommand CreateCopy()
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

            return new EventCommandOther(EventCommandKind, command);
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
