using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    class aStar
    {
        public void findPathWithAStar(Node startSquare, Node goalSquare)
        {
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            openList.Add(startSquare);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];

                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost() < currentNode.fCost() || openList[i].fCost() == currentNode.fCost() && openList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == goalSquare)
                {
                    retracePath(startSquare, goalSquare);
                    return;
                }

                foreach (Node adjecent in currentNode.adjecentNodes)
                {
                    if (closedList.Contains(adjecent) || !adjecent.tracable) continue;

                    float moveCost = currentNode.gCost + getDistance(currentNode, adjecent);

                    if (moveCost < adjecent.gCost || !openList.Contains(adjecent))
                    {
                        adjecent.gCost = moveCost;
                        adjecent.hCost = getDistance(adjecent, goalSquare);

                        adjecent.parent = currentNode;

                        if (!openList.Contains(adjecent) || adjecent.tracable)
                        {
                            openList.Add(adjecent);
                            adjecent.traced = true;
                        }

                        System.Threading.Thread.Sleep(10);
                    }
                }
            }
        }

        void retracePath(Node startSquare, Node goalSquare)
        {
            List<Node> path = new List<Node>();
            Node currentSquare = goalSquare;

            while (currentSquare != startSquare)
            {
                path.Add(currentSquare);
                currentSquare.path = true;
                currentSquare = currentSquare.parent;
                System.Threading.Thread.Sleep(10);
            }
        }

        int getDistance(Node a, Node b)
        {
            int distanceX = Math.Abs(a.posX - b.posX);
            int distanceY = Math.Abs(a.posY - b.posY);

            return distanceY + distanceX;
        }

    }
}
