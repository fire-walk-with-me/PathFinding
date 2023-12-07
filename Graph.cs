using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PathFinding
{
    class Graph
    {
        private const int offset = 2;
        public const int Width = 40, Heigth = 20;
        public Node[,] graph;
        
        public Graph()
        {
            createGraph();
        }

        private void createGraph()
        {
            graph = new Node[Width, Heigth];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Heigth; j++)
                {
                    graph[i, j] = new Node(i, j);
                }
            }

            foreach (Node n in graph)
            {
                n.AdjacentNode(graph);
            }
        }
    }
}
