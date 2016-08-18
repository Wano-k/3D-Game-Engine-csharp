using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    delegate void TreeVisitor<T>(T nodeData);

    class NTree<T>
    {
        public T Data;
        private LinkedList<NTree<T>> children;

        public NTree(T data)
        {
            Data = data;
            children = new LinkedList<NTree<T>>();
        }

        public void AddChild(T data)
        {
            children.AddFirst(new NTree<T>(data));
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
    }
}
