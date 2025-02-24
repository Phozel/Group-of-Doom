using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoD_s_Work.Tiles_Libary;
using static Shard.Shard.GoD_s_Work.Tiles_Libary.Tile;
using static System.Net.Mime.MediaTypeNames;

namespace Shard.Shard.GoDsWork.TilesLibrary
{

    internal static class Generation
    {
        //doesn't make a copy of list startGrowthNodes before altering it, FIX!
        internal static List<T> growLightning<T>(List<T> startGrowthNodes, List<List<T>> area) where T : Node
        {
            Random rand = new Random();
            List<T> allGrownToNodes = new List<T>();
            // allGrownToNodes.AddRange(startGrowthNodes); //adds start nodes to allGrownToNodes
            foreach (T node in startGrowthNodes) { node.setNotEmpty(); }
            while (startGrowthNodes.Any())
            {
                //Console.WriteLine("number of grow nodes" + growthNodes.Count());
                for (int i = 0; i < startGrowthNodes.Count();)
                {
                    //Console.WriteLine("dead? " + growthNodes[i].isDead());
                    //Console.WriteLine("finite? " + growthNodes[i].isFinite());
                    T currentNode = startGrowthNodes[i];
                    List<T> growTo = whereNodeCanGrow(currentNode, area);

                    if (!growTo.Any() || currentNode.isDead())
                    {
                        //               if (startGrowthNodes.Count() == 1) { currentNode.setRoomType(Room.RoomType.End); } //if is last room on last node
                        startGrowthNodes.RemoveAt(i);
                        continue;
                    }
                    //if (currentNode.isFinite()) { currentNode.lowerIteration(); }

                    if (growTo.Count() > 1) if (rand.Next(100) > 70) // chance of split
                            startGrowthNodes.Add(currentNode); // new growthNode created

                    // grow from node
                    T newNode = growTo.ElementAt(rand.Next(growTo.Count()));
                    if (currentNode.isFinite())
                    {
                        currentNode.lowerIteration();
                        newNode.makeFinite(currentNode.getIteration());
                    }
                    //newNode.setEdgeFrom(currentNode);
                    //                currentNode.addDoors(newNode); // "add" doors.
                    startGrowthNodes[i] = newNode;
                    newNode.setNotEmpty();
                    allGrownToNodes.Add(newNode);
                    i++;
                }
            }
            return allGrownToNodes;
        }

        //choose a number (nr) of random nodes from a list of Nodes
        internal static List<T> chooseRandomNodes<T>(List<T> nodeList, int nr) where T : Node
        {
            Random rand = new Random();
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

            List<T> randomNodeList = new List<T>();
            foreach (int index in indexList)
            {
                randomNodeList.Add(nodeList.ElementAt(index));
            }
            return randomNodeList;

        }
        // gets empty nodes around given Node
        internal static List<T> whereNodeCanGrow<T>(T n, List<List<T>> area) where T : Node
        {
            List<T> adjacentNodes = new List<T>();
            if (isNodeEmpty(westOfNode(n, area)) && isNodeEmpty(westOfNode(westOfNode(n, area), area)))
                adjacentNodes.Add(westOfNode(n, area));
            if (isNodeEmpty(eastOfNode(n, area)) && isNodeEmpty(eastOfNode(eastOfNode(n, area), area)))
                adjacentNodes.Add(eastOfNode(n, area));
            if (isNodeEmpty(northOfNode(n, area)) && isNodeEmpty(northOfNode(northOfNode(n, area), area)))
                adjacentNodes.Add(northOfNode(n, area));
            if (isNodeEmpty(southOfNode(n, area)) && isNodeEmpty(southOfNode(southOfNode(n, area), area)))
                adjacentNodes.Add(southOfNode(n, area));
            return adjacentNodes;
        }
        internal static List<T> adjecentFullNodes<T>(T n, List<List<T>> area) where T : Node
        {
            List<T> adjacentNodes = new List<T>();
            if (westOfNode(n, area) != null) if (!westOfNode(n, area).isNodeEmpty())
                    adjacentNodes.Add(westOfNode(n, area));
            if (eastOfNode(n, area) != null) if (!eastOfNode(n, area).isNodeEmpty())
                    adjacentNodes.Add(eastOfNode(n, area));
            if (northOfNode(n, area) != null) if (!northOfNode(n, area).isNodeEmpty())
                    adjacentNodes.Add(northOfNode(n, area));
            if (southOfNode(n, area) != null) if (!southOfNode(n, area).isNodeEmpty())
                    adjacentNodes.Add(southOfNode(n, area));
            return adjacentNodes;

        }
        private static bool isNodeEmpty<T>(T n) where T : Node
        {
            if (n == null) return false; // no Node means it can't be filled on therefore returns false

            return n.isNodeEmpty();
        }
        private static T westOfNode<T>(T n, List<List<T>> area) where T : Node
        {
            if (n.getX() == 0)
                return null;
            return area[n.getY()][n.getX() - 1];
        }
        private static T eastOfNode<T>(T n, List<List<T>> area) where T : Node
        {
            if (n.getX() == area[0].Count - 1)
                return null;
            return area[n.getY()][n.getX() + 1];
        }
        private static T northOfNode<T>(T n, List<List<T>> area) where T : Node
        {
            if (n.getY() == 0)
                return null;
            return area[n.getY() - 1][n.getX()];
        }
        private static T southOfNode<T>(T n, List<List<T>> area) where T : Node
        {
            if (n.getY() == area.Count - 1)
                return null;
            return area[n.getY() + 1][n.getX()];
        }


    }


}
