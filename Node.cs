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
    class Node
    {
        public int posX, posY;
        public float gCost, hCost;
        public Node parent;
        public bool tracable, traced;
        public bool path;
        public bool start, finish;
        public List<Node> adjecentNodes = new List<Node>();
        public Rectangle hitbox;

        private Color color;

        private const int offset = 2;

        public Node(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
            tracable = true;
            color = Color.White;
            hitbox = new Rectangle(posX * Game1.nodeTex.Width, posY * Game1.nodeTex.Height, Game1.nodeTex.Width, Game1.nodeTex.Height);
        }
        public void Update()
        {
            if (!tracable) color = Color.Black;
            else if (start) color = Color.Green;
            else if (finish) color = Color.Red;
            else if (path) color = Color.Yellow;
            else if (traced) color = Color.Blue;
            else color = Color.White;
        }
        public void Reset()
        {
            tracable = true;
            traced = false;
            start = false;
            finish = false;
            path = false;
        }
        public void SoftReset()
        {
            traced = false;
            path = false;
        }
        public float fCost()
        {
            return gCost + hCost;
        }
        public void ChangeStatus()
        {
            if (!start || !finish)
                tracable = !tracable;
        }
        public Node SetStart()
        {

            start = true;
            tracable = true;
            return this;
        }
        public Node SetFinish()
        {
            finish = true;
            tracable = true;
            return this;
        }
        public void resetSF()
        {
            finish = false;
            start = false;
        }
        public List<Node> GetAdjecent()
        {
            return adjecentNodes;
        }
        public void AdjacentNode(Node[,] graph)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int x = posX + i;
                    int y = posY + j;

                    if (x >= 0 && x < Graph.Width && y >= 0 && y < Graph.Heigth)
                    {
                        if (x == posX || y == posY)
                        {
                            adjecentNodes.Add(graph[x, y]);
                        }
                    }
                }
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Game1.nodeTex, new Vector2(posX * Game1.nodeTex.Width + offset * posX, posY * Game1.nodeTex.Height + offset * posY), color);
        }
    }
}
