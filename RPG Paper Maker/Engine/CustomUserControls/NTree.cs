using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    delegate void TreeVisitor<T>(T nodeData);

    public class NTree<T>
    {
        public T Data;
        private LinkedList<NTree<T>> children;


        public NTree(T data)
        {
            Data = data;
            children = new LinkedList<NTree<T>>();
        }

        public NTree<T> AddChildData(T data)
        {
            NTree<T> child = new NTree<T>(data);
            children.AddLast(child);

            return child;
        }

        public NTree<T> AddChild(NTree<T> child)
        {
            children.AddLast(child);

            return child;
        }

        public bool IsLastChild(NTree<T> d)
        {
            return (d == children.Last.Value);
        }

        public NTree<T> GetChild(int i)
        {
            foreach (NTree<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public LinkedList<NTree<T>> GetChildren()
        {
            return children;
        }

        private NTree<T> GetLastNode(NTree<T> node)
        {
            NTree<T> lastNode = null;
            if (node.GetChildren().Count > 0)
            {
                foreach (NTree<T> child in node.GetChildren())
                {
                    NTree<T> lastNodeChild = GetLastNode(child);
                    if (lastNodeChild != null) lastNode = lastNodeChild;
                }
            }
            else return node;

            return lastNode;
        }

        public NTree<T> GetLastNode()
        {
            return GetLastNode(this);
        }
    }
}
