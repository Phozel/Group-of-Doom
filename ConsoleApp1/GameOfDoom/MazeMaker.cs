using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoD_s_Work.Tiles_Libary;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shard.GameOfDoom
{
    public class Map
    {
        //internal List<List<Room>> roomMap;
        internal List<List<Debug>> debugMap;
        public Map(int lengthToEnd, int LeastRooms, Tuple<int, int> mapSize) {
            if (LeastRooms > mapSize.Item1 * mapSize.Item2)
            {
                Console.Error.WriteLine("Custom Error in Map: LeastRooms is larger than mapSize area.");
                return;
            }

            //growDebugMap();



        }
        /**
         * 0 = no change
         */
        public void SetStaticVariables()
        {

        }

        //growthNode
        //procentage loss - with everry growth // equation from room size
        //procentage split 
        //

    }
    public class MazeMaker {
        //make a 2d maze (first) that is made by using a lightning pattern from the edges of map
        //all edges between dots are made by the node containing a position which is its "root"


        /**
            * [
            *  [Node(0,0),  [Node(1,0),  [Node(2,0),
            *   Node(0,1),   Node(1,1),   Node(2,1),
            *   Node(0,2)],  Node(1,2)],  Node(2,2)]
            * ]
            * Node (x,y)
            */
        internal readonly List<List<Node>> maze;
        private readonly int mazeIndexLengthX;
        private readonly int mazeIndexLengthY;
        private readonly Random rand = new Random();

        public MazeMaker(int lengthX, int lengthY, int nrGrowthStartNodes, int nrFiniteGrowthStartNodes)
        {
            this.mazeIndexLengthX = lengthX - 1;
            this.mazeIndexLengthY = lengthY - 1;

            maze = new List<List<Node>>();
            
            makeMazeFrame();
            makeMaze(nrGrowthStartNodes, nrFiniteGrowthStartNodes);

        }

        private void makeMazeFrame()
        {
            for (int x = 0; x <= mazeIndexLengthX; x++)
            {
                
                maze.Add( new List<Node>());
                //maze.add(new ArrayList<>()); //make new row, fills one more space in width/column
                for (int y = 0; y <= mazeIndexLengthY; y++)
                {
                    maze[x].Add(new Node(x, y));
                    //maze.get(x).add(new Node(x, y)); //make one "connecting pillar" in that row
                        
                }
            }
        }

        internal List<List<Node>> getMaze()
        {
            return maze;
        }

        public int getMazeIndexLengthX() { return mazeIndexLengthX; }

        private void makeMaze(int nrStartNodes, int nrFiniteStartNodes)
        {
        makeBorder(2);// connects the starter-Nodes to themselves, and builds border
        List<Node> growthNodes = chooseRandomNodes(getAcceptableStartNodes(), nrStartNodes + nrFiniteStartNodes); //choose given nr of start-tree-nodes, add each to list
        makeAmountFinite(growthNodes, nrFiniteStartNodes);
            growMaze(growthNodes);
            fillRest();
        }

        private void fillRest()
        {
            List<Node> NewStartNodes = getAcceptableStartNodes();
        NewStartNodes.OrderBy(_ => rand.Next()).ToList();
        //Collections.shuffle(NewStartNodes);
            growMaze(NewStartNodes);
        }

        private void growMaze(List<Node> growthNodes)
        {
            while (growthNodes.Any())
            {
                for (int i = 0; i < growthNodes.Count();)
                {
                    Node currentNode = growthNodes[i];
                    List<Node> growTo = whereNodeCanGrow(currentNode);

                    if (!growTo.Any() || (currentNode.isDead()))
                    {
                        growthNodes.RemoveAt(i);
                        continue;
                    }
                    if (currentNode.isFinite())
                        currentNode.lowerIteration();
                    
                    if (growTo.Count() > 1) if (rand.Next(100) > 70) // chance of split
                            growthNodes.Add(currentNode); // new growthNode created

                    // grow from node
                    Node newNode = growTo.ElementAt(rand.Next(growTo.Count()));
                    newNode.setEdgeFrom(currentNode);
                    growthNodes[i] = newNode;
                    i++;
                }
            }
        }

        private void makeAmountFinite(List<Node> nodes, int nrFinite)
        {
            foreach (Node n in chooseRandomNodes(nodes, nrFinite)) { n.makeFinite(1); }
        }

        private List<Node> getAcceptableStartNodes()
        {
            List<Node> acceptableGrowthNodes = new List<Node>();
            foreach (List<Node> row in maze)
            {
                foreach (Node node in row)
                {
                    if (!node.isEmpty()) if (whereNodeCanGrow(node).Any())
                            acceptableGrowthNodes.Add(node);
                }
            }
            return acceptableGrowthNodes;
        }

        private List<Node> chooseRandomNodes(List<Node> nodeList, int nr)
        {
            if (nr >= nodeList.Count()) // take whole list if choose more elements than in list
                return nodeList;

            List<int> indexList = new List<int>();
            while (indexList.Count() < nr)
            {
                for (int i = 0; i < nr; i++)
                {
                    int index = rand.Next(nodeList.Count());
                    if (!indexList.Contains(index)) indexList.Add(index);
                }
            }

            List<Node> randomNodeList = new List<Node>();
            foreach (int index in indexList)
            {
                randomNodeList.Add(nodeList.ElementAt(index));
            }
            return randomNodeList;

        }

        /**
            * Makes a border by starting from the randomized nrStartNodes along the border.
            * @param nrStartNodes Must be atLeast One, and is the number of openings the maze will have
            */
        private void makeBorder(int nrStartNodes)
        {
            List<Node> borderNodes = getBorderNodes(); // get all nodes along the edges
           
            List<Node> startNodes = chooseRandomNodes(borderNodes, nrStartNodes); //Choose two nodes
 
            makeBuildBorder(borderNodes, startNodes);
        }

        private void makeBuildBorder(List<Node> borderNodes, List<Node> startNodes)
        {
            bool isClosed = !startNodes.Any();
            if (isClosed) { startNodes[0] = borderNodes[0]; }

            foreach (Node n in startNodes) { n.setEdgeFrom(n); }
            Node currentNode = startNodes[0];
            Node newNode;
            
            while (containsEmptyNode(borderNodes))
            { //build clockwise, along the edge until they encounter a not-null node, then don't build

                if (currentNode.getX() == 0 && currentNode.getY() == 0)
                {
                    newNode = maze[1][0];
                }
                else if (westOf(currentNode) == null)
                { //  west side -> go north
                    newNode = maze[0][currentNode.getY() - 1];
                }
                else if (southOf(currentNode) == null)
                { // south side -> go west
                    newNode = maze[(currentNode.getX() - 1)][mazeIndexLengthY];
                }
                else if (eastOf(currentNode) == null)
                { //  east side -> go south
                    newNode = maze[mazeIndexLengthX][currentNode.getY() + 1];
                }
                else if (northOf(currentNode) == null)
                { // north side -> go east
                    newNode = maze[currentNode.getX() + 1][0];
                }
                else
                {
                    Console.Error.WriteLine("An Error has occurred in makeBorder. \nThe else function was called.");
                    break;
                }
                Node test = newNode;
                if (newNode.isEmpty()) { newNode.setEdgeFrom(currentNode); }
                
                currentNode = newNode;
            }
            if (isClosed) // closes the edge.
                startNodes[0].setEdgeFrom(currentNode);
        }

        private List<Node> getBorderNodes()
        {
            List<Node> borderNodes = new List<Node>();

            borderNodes.AddRange(maze[0]); // left side
            borderNodes.AddRange(maze[mazeIndexLengthX]); // right side
            /*for (int i = 1; i < mazeIndexLengthY; i++)
            { // everything in between
                borderNodes.Add(maze[0][i]); // left
                borderNodes.Add(maze[mazeIndexLengthX][i]); //right
            }*/
            for (int i = 1; i < mazeIndexLengthX; i++)
            { // everything in between
                borderNodes.Add(maze[i][0]); // top
                borderNodes.Add(maze[i][mazeIndexLengthY]); //bottom
            }
            return borderNodes;
        }
        private bool containsEmptyNode(List<Node> nodes)
        {
            
            foreach (Node n in nodes) { if (n.isEmpty()) return true; }
            
            return false;
        }

        private List<Node> whereNodeCanGrow(Node n)
            {
                List<Node> adjacentNodes = new List<Node>();
                if (isNodeEmpty(westOf(n)))
                    adjacentNodes.Add(westOf(n));
                if (isNodeEmpty(eastOf(n)))
                    adjacentNodes.Add(eastOf(n));
                if (isNodeEmpty(northOf(n)))
                    adjacentNodes.Add(northOf(n));
                if (isNodeEmpty(southOf(n)))
                    adjacentNodes.Add(southOf(n));
                return adjacentNodes;
            }

            private Node westOf(Node n)
            {
                if (n.getX() == 0)
                    return null;
                return maze[n.getX() - 1][n.getY()];
            }
            private Node eastOf(Node n)
            {
                if (n.getX() == mazeIndexLengthX)
                    return null;
                return maze[n.getX() + 1][n.getY()];
            }
            private Node northOf(Node n)
            {
                if (n.getY() == 0)
                    return null;
                return maze[n.getX()][n.getY() - 1];
            }
            private Node southOf(Node n)
            {
                if (n.getY() == mazeIndexLengthY)
                    return null;
                return maze[n.getX()][n.getY() + 1];
            }

            /**
             * Checks if the Node in question is empty.
             * Null counts as a non-empty node.
             * @param n The node, can also be null.
             * @return True if there is a node, and it is empty.
             */
        private bool isNodeEmpty(Node? n)
        {
            if (n == null) return false; // no Node means it can't be filled on therefore returns false
            
            return n.isEmpty();
        }


    }
    internal class Node 
    {
        //internal bool doorUp = false;
        //internal bool doorDown = false;
        //internal bool doorLeft = false;
        //internal bool doorRight = false;

        private readonly int positionX;
        private readonly int positionY;
        private int edgeFromX;
        private int edgeFromY;
        private bool finite = false;
        private int iterations = 0;

        internal Node(int x, int y)
        {
            this.positionX = x;
            this.positionY = y;
            this.edgeFromX = -1;
            this.edgeFromY = -1;
        }

        internal int getX() { return positionX; }
        internal int getY() { return positionY; }
        internal int getEdgeX() { return edgeFromX; }
        internal int getEdgeY() { return edgeFromY; }

        internal bool isEmpty() { return (edgeFromX == -1 && edgeFromY == -1); } 
            //-1 is out of range position = null or false
        internal bool isFinite() { return this.finite; }
        internal bool isDead() { return (finite && iterations < 1); }

        internal void setEdgeFrom(Node n)
        {
            this.edgeFromX = n.getX();
            this.edgeFromY = n.getY();
        }
        internal bool edgeExist(Node n)
        {
            bool edgeToMe = edgeFromX == n.getX() && edgeFromY == n.getY();
            bool edgeFromMe = positionX == n.getEdgeX() && positionY == n.getEdgeY();
            return (edgeToMe || edgeFromMe);
        }

        internal void lowerIteration() { this.iterations--; }
        internal void makeFinite(int iterations)
        {
            this.finite = true;
            this.iterations = iterations;
        }

    }


    public class DebugView
    {
        private readonly MazeMaker mazeMaker;
        private readonly List<List<Node>> maze;
        private List<string> visualMaze;

        public DebugView(MazeMaker mazeMaker)
        {
            this.mazeMaker = mazeMaker;
            this.maze = mazeMaker.getMaze();
            this.visualMaze = new List<string>();

            makeMazeVisual();
        }
        public void draw()
        {
            foreach (string row in visualMaze)
            {
                Console.WriteLine(row);
            }
        }

        public void printEdges()
        {
            Console.WriteLine("[");
            foreach (List<Node> row in maze)
            {
                Console.WriteLine("[");
                foreach (Node n in row)
                {
                    printNode(n);
                }
                Console.WriteLine("]\n");
            }
            Console.WriteLine("]\n");
        }
        private void printNode(Node n)
        {
            Console.WriteLine("(" + n.getEdgeX() + ", " + n.getEdgeY() + ")");
        }

        private void makeMazeVisual()
        {
            int mazeIndexLength = mazeMaker.getMazeIndexLengthX();

            for (int i = 0; i <= mazeIndexLength; i++)
            {
                if (i < mazeIndexLength)
                    drawRow(maze[i], maze[i + 1]);
                else drawRow(maze[i], null);
            }
        }
        private void drawRow(List<Node> from, List<Node> to)
        {
            StringBuilder row = new StringBuilder();
            StringBuilder belowRow = new StringBuilder();

            for (int i = 0; i < from.Count(); i++)
            {
                if (to != null) if (from[i].edgeExist(to[i]))
                    {
                        belowRow.Append("|"); // Vertical Edge
                    }
                    else
                    {
                        belowRow.Append(" "); // no Vertical Edge
                    }
                belowRow.Append("    "); // Space between Vertical Edges

                row.Append("*"); // Node itself
                if (i < from.Count() - 1) if (from[i].edgeExist(from[i + 1]))
                    {
                        row.Append(" -- ");
                    }
                    else
                    {
                        row.Append("    ");
                    }
            }
            visualMaze.Add(row.ToString());
            visualMaze.Add(belowRow.ToString());
        }
    }

}
