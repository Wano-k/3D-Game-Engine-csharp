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
        public LinkedList<NTree<T>> Children;


        public NTree(T data)
        {
            Data = data;
            Children = new LinkedList<NTree<T>>();
        }

        public NTree<T> AddChildData(T data)
        {
            NTree<T> child = new NTree<T>(data);
            Children.AddLast(child);

            return child;
        }

        public NTree<T> AddChild(NTree<T> child)
        {
            Children.AddLast(child);

            return child;
        }

        public bool IsLastChild(NTree<T> d)
        {
            return (d == Children.Last.Value);
        }

        public NTree<T> GetChild(int i)
        {
            foreach (NTree<T> n in Children)
                if (--i == 0)
                    return n;
            return null;
        }

        public void DeleteChild(NTree<T> child)
        {
            Children.Remove(child);
        }

        private NTree<T> GetLastNode(NTree<T> node)
        {
            NTree<T> lastNode = null;
            if (node.Children.Count > 0)
            {
                foreach (NTree<T> child in node.Children)
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

        private int GetNodesCount(NTree<T> node)
        {
            int count = 0;
            if (node.Children.Count == 0) count++;
            else
            {
                foreach (NTree<T> child in node.Children)
                {
                    count += GetNodesCount(child);
                }
            }

            return count;
        }

        public int GetNodesCount()
        {
            return GetNodesCount(this);
        }
    }
}
