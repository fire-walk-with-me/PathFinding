using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Texture2D nodeTex;
        Graph NodeGraph;
        MouseState mouseState;
        MouseState previousMouseState;
        KeyboardState previousKeyState;
        KeyboardState keyState;
        Point mousePos;
        string searchAlogritm;
        bool change;
        Node startNode, goalNode;
        aStar aStar;
        Dijkstra dijkstra;
        BFS BFS;
        DFS DFS;
        Thread aStarThread;
        Thread DijkstraThread;
        Thread BFSThread;
        Thread DFSThread;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            nodeTex = Content.Load<Texture2D>("plattform");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            NodeGraph = new Graph();
            aStar = new aStar();
            dijkstra = new Dijkstra();
            BFS = new BFS();
            DFS = new DFS();
        }

        protected override void UnloadContent()
        {

        }

        private void aStarMethod()
        {
            searchAlogritm = "A*";
            foreach (Node n in NodeGraph.graph) n.SoftReset();
            aStar.findPathWithAStar(startNode, goalNode);
        }

        private void dijkstraMethod()
        {
            searchAlogritm = "Dijkstra";
            foreach (Node n in NodeGraph.graph)
            {
                n.SoftReset();
                n.gCost = 0;
                n.hCost = 0;
            }
            dijkstra.findPathWithDijkstra(startNode, goalNode);
        }

        private void BFSMethod()
        {
            searchAlogritm = "Breath-first Search";
            foreach (Node n in NodeGraph.graph) n.SoftReset();
            BFS.findPathWithBFS(startNode, goalNode);
        }

        private void DFSMethod()
        {
            searchAlogritm = "Depth-first Search";
            foreach (Node n in NodeGraph.graph) n.SoftReset();
            DFS.findPathWithDFS(startNode, goalNode);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            previousMouseState = mouseState;
            previousKeyState = keyState;
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);

            foreach (Node n in NodeGraph.graph)
            {
                n.Update();

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    n.Reset();
                    startNode = null;
                    goalNode = null;

                    if(BFSThread != null && BFSThread.IsAlive)
                        BFSThread.Abort();
                    if (DFSThread != null && DFSThread.IsAlive)
                        DFSThread.Abort();
                    if (aStarThread != null && aStarThread.IsAlive)
                        aStarThread.Abort();
                    if (DijkstraThread != null && DijkstraThread.IsAlive)
                        DijkstraThread.Abort();

                }

                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && n.hitbox.Contains(mousePos.X - 28, mousePos.Y - 15))
                {
                    n.ChangeStatus();
                }
                if (mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released && n.hitbox.Contains(mousePos.X - 28, mousePos.Y - 15))
                {
                    if (startNode != null && goalNode != null)
                    {
                        foreach(Node r in NodeGraph.graph)
                        r.resetSF();
                        startNode = null;
                        goalNode = null;
                    }

                    if (!change) startNode = n.SetStart();
                    if (change) goalNode = n.SetFinish();
                    change = !change;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                foreach (Node n in NodeGraph.graph) n.SoftReset();

                if (BFSThread != null && BFSThread.IsAlive)
                    BFSThread.Abort();
                if (DFSThread != null && DFSThread.IsAlive)
                    DFSThread.Abort();
                if (aStarThread != null && aStarThread.IsAlive)
                    aStarThread.Abort();
                if (DijkstraThread != null && DijkstraThread.IsAlive)
                    DijkstraThread.Abort();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F) && keyState != previousKeyState)
            {
                BFSThread = new Thread(BFSMethod);
                BFSThread.Start();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) && keyState != previousKeyState)
            {
                DFSThread = new Thread(DFSMethod);
                DFSThread.Start();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && keyState != previousKeyState)
            {
                aStarThread = new Thread(aStarMethod);
                aStarThread.Start(); 
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && keyState != previousKeyState)
            {
                DijkstraThread = new Thread(dijkstraMethod);
                DijkstraThread.Start();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Window.Title = "Search algoritm: " + searchAlogritm;

            foreach (Node n in NodeGraph.graph)
            {
                n.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
