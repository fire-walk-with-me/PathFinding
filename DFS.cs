using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    class DFS
    {
        Stack<Node> s = new Stack<Node>();

        public void findPathWithDFS(Node start, Node finish)
        {
            s.Clear();

            start.traced = true;
            s.Push(start);

            while(s.Count != 0)
            {
                Node currentNode = s.Pop();
                currentNode.traced = true;

                if(currentNode == finish) 
                {
                    retracePath(start, finish);
                    return;
                }
                foreach(Node n in currentNode.GetAdjecent())
                {
                    if (n.traced) continue;
                    if (n.tracable)
                    {
                        n.parent = currentNode;
                        s.Push(n);
                    }
                }

                System.Threading.Thread.Sleep(10);
            }
        }

        void retracePath(Node startSquare, Node goalSquare)
        {
            Node currentSquare = goalSquare;

            while (currentSquare != startSquare)
            {
                currentSquare.path = true;
                currentSquare = currentSquare.parent;
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
