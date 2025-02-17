using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoD_s_Work.Tiles_Libary;

namespace Shard.GameOfDoom
{
    internal class Generation
    {
        
    }

    public class WorldMap
    {
        internal List<List<Room>> worldMap;
        //internal List<List<Debug>> debugMap;
        private readonly Random rand = new Random();
        private readonly int columns;
        private readonly int rows;
        private readonly int lengthToEnd;
        private readonly int leastRooms;
        public WorldMap(int lengthToEnd, int LeastRooms, Tuple<int, int> mapSize)
        {
            if (LeastRooms > mapSize.Item1 * mapSize.Item2)
            {
                Console.Error.WriteLine("Custom Error in Map: LeastRooms is larger than mapSize area.");
                return;
            }
            rows = mapSize.Item1;
            columns = mapSize.Item2;
            this.lengthToEnd = lengthToEnd;
            this.leastRooms = LeastRooms;
            

            //growDebugMap();
            MakeWorldMap();
            Room startRoom = getStartRoom();
            GrowFrom(startRoom);
            AddKey();
        }
        /**
         * 0 = no change
         */
        public void SetStaticVariables() { }

        //gen rooms
            //if only one door => save
            //if more than one door => Type = normal
            //if start, dont change type but leave spawn area space clear


        private void MakeWorldMap()
        {
            for (int x = 0; x < rows; x++)
            {

                worldMap.Add(new List<Room>());
                //maze.add(new ArrayList<>()); //make new row, fills one more space in width/column
                for (int y = 0; y < columns; y++)
                {
                    worldMap[x].Add(new Room(x, y));
                    //maze.get(x).add(new Node(x, y)); //make one "connecting pillar" in that row

                }
            }
        }
        private Room getStartRoom() 
        { 
            Room startRoom = worldMap[rand.Next(rows)][rand.Next(columns)];
            startRoom.setRoomType(Room.RoomType.Start);
            startRoom.makeFinite(lengthToEnd);
            return startRoom; 
        }
        private void GrowFrom(Room growthNode) {
            List<Room> rooms = [growthNode];
            if (rand.Next(100) > 70) // chance of split
                    rooms.Add(growthNode); // new growthNode created
            if (rand.Next(100) > 70) // chance of split
                rooms.Add(growthNode); // new growthNode created
            GrowFrom(rooms); 
        }
        private void GrowFrom(List<Room> growthNodes)
        {
            while (growthNodes.Any())
            {
                Console.WriteLine("number of grow nodes" + growthNodes.Count());
                for (int i = 0; i < growthNodes.Count();)
                {
                    //Console.WriteLine("dead? " + growthNodes[i].isDead());
                    //Console.WriteLine("finite? " + growthNodes[i].isFinite());
                    Room currentNode = growthNodes[i];
                    List<Room> growTo = whereNodeCanGrow(currentNode);

                    if (!growTo.Any() || (currentNode.isDead()))
                    {
                        if (growthNodes.Count() == 1) { currentNode.setRoomType(Room.RoomType.End); } //if is last room on last node
                        growthNodes.RemoveAt(i);
                        continue;
                    }
                    //if (currentNode.isFinite()) { currentNode.lowerIteration(); }

                    if (growTo.Count() > 1) if (rand.Next(100) > 70) // chance of split
                            growthNodes.Add(currentNode); // new growthNode created

                    // grow from node
                    Room newNode = growTo.ElementAt(rand.Next(growTo.Count()));
                    if (currentNode.isFinite())
                    {
                        currentNode.lowerIteration();
                        newNode.makeFinite(currentNode.getIteration());
                    }
                    //newNode.setEdgeFrom(currentNode);
                    currentNode.addDoors(newNode); // "add" doors.
                    growthNodes[i] = newNode;
                    i++;
                }
            }
        }
        private List<Room> whereNodeCanGrow(Room n)
        {
            List<Room> adjacentNodes = new List<Room>();
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
        private bool isNodeEmpty(Room n)
        {
            if (n == null) return false; // no Node means it can't be filled on therefore returns false
            return n.isEmpty();
        }
        private Room westOf(Node n)
        {
            if (n.getX() == 0)
                return null;
            return worldMap[n.getX() - 1][n.getY()];
        }
        private Room eastOf(Node n)
        {
            if (n.getX() == columns-1)
                return null;
            return worldMap[n.getX() + 1][n.getY()];
        }
        private Room northOf(Node n)
        {
            if (n.getY() == 0)
                return null;
            return worldMap[n.getX()][n.getY() - 1];
        }
        private Room southOf(Node n)
        {
            if (n.getY() == rows-1)
                return null;
            return worldMap[n.getX()][n.getY() + 1];
        }



        private void AddKey()
        {
            List<Room> deadEnds = new List<Room>();
            foreach (List<Room> row in worldMap)
            {
                foreach (Room room in row)
                {
                    if (room.isEmpty()) { continue; }
                    if (room.isDeadEnd())
                    {
                        if (room.getRoomType() != Room.RoomType.Start || room.getRoomType() != Room.RoomType.End)
                        { deadEnds.Add(room); }
                    }
                }
            }

            if (deadEnds.Count() >= 1)
            {
                int index = rand.Next(deadEnds.Count());
                index = rand.Next(deadEnds.Count());
                deadEnds[index].setRoomType(Room.RoomType.Key);
            }
        }
    }



    internal static class MazeParts
    {
        
    }

    internal class RoomGeneration
    {
    }

    internal class Room : Node
    {
        private readonly Random rand = new Random();
        public static int roomWidth = 10;
        public static int roomHeight = 8;
        private bool doorUp { get; set; }
        private bool doorDown { get; set; }
        private bool doorLeft { get; set; }
        private bool doorRight { get; set; }
        private List<List<Tile>> roomLayout;
        private RoomType roomType = RoomType.Null;
        internal enum RoomType { Start, End, Key, Normal, Null }
        internal Room(int x, int y) : base(x, y) 
        {
            doorUp = false;
            doorDown = false;
            doorLeft = false;
            doorRight = false;
            roomLayout = new List<List<Tile>>();
        }
        internal void setRoomType(RoomType roomType) { this.roomType = roomType; }
        internal RoomType getRoomType() { return roomType; }
        internal void addDoors(Room room)
        {
            if (this.getX() < room.getX()) { doorRight = true; room.doorLeft = true; } // this left from room
            if (this.getX() > room.getX()) { doorLeft = true; room.doorRight = true; } // this right from room
            if (this.getY() < room.getY()) { doorUp = true; room.doorDown = true; } // this down from room
            if (this.getY() > room.getY()) { doorDown = true; room.doorUp = true; } // this up from room

        }

        internal bool isEmpty() { return !(doorUp || doorDown || doorLeft || doorRight); }
        internal bool isDeadEnd() { return (doorUp ^ doorDown ^ doorLeft ^ doorRight); }

        public void generateRoom()
        {
            MakeRoom();
            List<Tile> growthNodes = buildOuterWallsAndDoors();
            foreach (Tile tile in growthNodes) { tile.makeFinite(rand.Next(roomHeight/2)); }
            generateObstacles(growthNodes);
            //generateRoom();
        }
        private void MakeRoom()
        {
            for (int x = 0; x < roomHeight; x++)
            {
                roomLayout.Add(new List<Tile>());
                //maze.add(new ArrayList<>()); //make new row, fills one more space in width/column
                for (int y = 0; y < roomWidth; y++)
                {
                    roomLayout[x].Add(new Tile(Tag.ground, x, y));
                    //maze.get(x).add(new Node(x, y)); //make one "connecting pillar" in that row
                }
            }
        }
        
        private List<Tile> buildOuterWallsAndDoors()
        {
            List<Tile> borderNodes = new List<Tile>();

            //  borderNodes.AddRange(roomLayout[0]); // left side
            //borderNodes.AddRange(roomLayout[rows-1]); // right side
            List<Tile> topWall = new List<Tile>();
            List<Tile> bottomWall = new List<Tile>();
            List<Tile> leftWall = new List<Tile>();
            List<Tile> rightWall = new List<Tile>();
            //corners
            roomLayout[0][0].tag = Tag.wall;
            roomLayout[0][roomWidth-1].tag = Tag.wall;
            roomLayout[roomHeight-1][0].tag = Tag.wall;
            roomLayout[roomHeight-1][roomWidth-1].tag = Tag.wall;

            for (int i = 1; i < roomWidth - 1; i++)
            { // everything in between
                topWall.Add(roomLayout[0][i]); // up
                bottomWall.Add(roomLayout[roomHeight - 1][i]); // down
            }
            for (int i = 1; i < roomHeight - 1; i++)
            { // everything in between
                leftWall.Add(roomLayout[i][0]); // left
                rightWall.Add(roomLayout[i][roomWidth - 1]); //right
                
            }
            if (doorUp) { addDoor(topWall); } //door up
            if (doorDown) { addDoor(bottomWall); } //door down
            if (doorLeft) { addDoor(leftWall); } //door left
            if (doorRight) { addDoor(rightWall); } //door right

            borderNodes.AddRange(topWall);
            borderNodes.AddRange(bottomWall);
            borderNodes.AddRange(leftWall);
            borderNodes.AddRange(rightWall);

            foreach (Tile n in borderNodes) { n.tag = Tag.wall; }

            return borderNodes;
        }
        private void addDoor(List<Tile> wall)
        {
            int index = rand.Next(wall.Count());
            wall[index].tag = Tag.door;
            wall.RemoveAt(index);
        }

        private void generateObstacles(List<Tile> growthNodes)
        {
            while (growthNodes.Any())
            {
                Console.WriteLine("number of grow nodes" + growthNodes.Count());
                for (int i = 0; i < growthNodes.Count();)
                {
                    //Console.WriteLine("dead? " + growthNodes[i].isDead());
                    //Console.WriteLine("finite? " + growthNodes[i].isFinite());
                    Tile currentNode = growthNodes[i];
                    List<Tile> growTo = whereNodeCanGrow(currentNode);

                    if (!growTo.Any() || (currentNode.isDead()))
                    {
                        growthNodes.RemoveAt(i);
                        continue;
                    }
                    //if (currentNode.isFinite()) { currentNode.lowerIteration(); }

                    if (growTo.Count() > 1) if (rand.Next(100) > 70) // chance of split
                            growthNodes.Add(currentNode); // new growthNode created

                    // grow from node
                    Tile newNode = growTo.ElementAt(rand.Next(growTo.Count()));
                    if (currentNode.isFinite())
                    {
                        currentNode.lowerIteration();
                        newNode.makeFinite(currentNode.getIteration());
                    }
                    newNode.tag = Tag.wall; //.setEdgeFrom(currentNode);
                    growthNodes[i] = newNode;
                    i++;
                }
            }
        }
        private List<Tile> whereNodeCanGrow(Tile n)
        {
            List<Tile> adjacentNodes = new List<Tile>();
            if (isNodeEmpty(westOf(n)) && isNodeEmpty(westOf(westOf(n))))
                adjacentNodes.Add(westOf(n));
            if (isNodeEmpty(eastOf(n)) && isNodeEmpty(eastOf(eastOf(n))))
                adjacentNodes.Add(eastOf(n));
            if (isNodeEmpty(northOf(n)) && isNodeEmpty(northOf(northOf(n))))
                adjacentNodes.Add(northOf(n));
            if (isNodeEmpty(southOf(n)) && isNodeEmpty(southOf(southOf(n))))
                adjacentNodes.Add(southOf(n));
            return adjacentNodes;
        }
        private bool isNodeEmpty(Tile n)
        {
            if (n == null) return false; // no Node means it can't be filled on therefore returns false

            return n.tag == Tag.ground;
        }
        private Tile westOf(Tile n)
        {
            if (n.getX() == 0)
                return null;
            return roomLayout[n.getX() - 1][n.getY()];
        }
        private Tile eastOf(Tile n)
        {
            if (n.getX() == roomWidth-1)
                return null;
            return roomLayout[n.getX() + 1][n.getY()];
        }
        private Tile northOf(Tile n)
        {
            if (n.getY() == 0)
                return null;
            return roomLayout[n.getX()][n.getY() - 1];
        }
        private Tile southOf(Tile n)
        {
            if (n.getY() == roomHeight-1)
                return null;
            return roomLayout[n.getX()][n.getY() + 1];
        }






    }
}
