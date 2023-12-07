using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    class BFS
    {
        Queue<Node> Q = new Queue<Node>();

        public void findPathWithBFS(Node start, Node finish)
        {
            Q.Clear();

            start.traced = true;
            Q.Enqueue(start);

            while(Q.Count > 0)
            {
                Node currentNode = Q.Dequeue();
                foreach(Node n in currentNode.GetAdjecent())
                {
                    if (n.traced || !n.tracable) continue;

                    Q.Enqueue(n);
                    n.traced = true;
                    n.parent = currentNode;

                    if (n == finish)
                    {
                        retracePath(start, finish);
                        return;
                    }

                    System.Threading.Thread.Sleep(10);
                }
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
