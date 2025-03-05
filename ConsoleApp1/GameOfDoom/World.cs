using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoD_s_Work.Tiles_Libary;
using Shard.Shard.GoDsWork.TilesLibrary;
using static Shard.GameOfDoom.World;

namespace Shard.GameOfDoom
{
    internal class World
    {
        //  private static Navigate instance;
        //  public static Navigate Instance;
        //singleton?

        private List<List<Room>> worldMap;
        private Room currentRoom;

        internal World()
        {
            WorldMap wm = new WorldMap(3, 0, (5, 7));
            this.worldMap = wm.getMap();
            currentRoom = wm.GetStartRoom();
            currentRoom.getRoomLayout();

        }

        internal void update()
        {
            currentRoom.update();
        }


        public class WorldMap
        {
            internal List<List<Room>> worldMap;
            //internal List<List<Debug>> debugMap;
            private readonly Random rand = new Random();
            private readonly int width;
            private readonly int height;
            private readonly int lengthToEnd;
            private Room startRoom;
            private readonly int leastRooms; //not used

            public WorldMap(int lengthToEnd, int LeastRooms, (int, int) mapSize)
            {
                if (LeastRooms > mapSize.Item1 * mapSize.Item2)
                {
                    Console.Error.WriteLine("Custom Error in Map: LeastRooms is larger than mapSize area.");
                    return;
                }
                width = mapSize.Item1;
                height = mapSize.Item2;
                this.lengthToEnd = lengthToEnd;
                this.leastRooms = LeastRooms;
                worldMap = new List<List<Room>>();

                //growDebugMap();
                MakeWorldMap();
                Room startRoom = getStartRoom();
                GrowFrom(startRoom);
                SetKeyRoom();
            }

            internal List<List<Room>> getMap() { return worldMap; }
            internal Room GetStartRoom() { return startRoom; }

            private void MakeWorldMap()
            {
                for (int x = 0; x < height; x++)
                {

                    worldMap.Add(new List<Room>());
                    //maze.add(new ArrayList<>()); //make new row, fills one more space in width/column
                    for (int y = 0; y < width; y++)
                    {
                        worldMap[x].Add(new Room(y, x));
                        //maze.get(x).add(new Node(x, y)); //make one "connecting pillar" in that row

                    }
                }
            }
            private Room getStartRoom()
            {
                this.startRoom = worldMap[rand.Next(height)][rand.Next(width)];
                startRoom.setRoomType(Room.RoomType.Start);
                startRoom.makeFinite(lengthToEnd);
                return startRoom;
            }
            private void GrowFrom(Room startRoom)
            {
                List<Room> rooms = [startRoom];
                if (rand.Next(100) > 70) // chance of split
                    rooms.Add(startRoom); // new growthNode created
                if (rand.Next(100) > 70) // chance of split
                    rooms.Add(startRoom); // new growthNode created
                List<Room> allRooms = new List<Room> { startRoom };
                allRooms.AddRange(Generation.growLightning(rooms, worldMap));
                allRooms[allRooms.Count - 1].setRoomType(Room.RoomType.End);
                addDoors(allRooms); //fix doors

            }

            private void addDoors(List<Room> allRooms)
            {

                List<Room> adjecentNodes;
                foreach (Room room in allRooms)
                {
                    adjecentNodes = Generation.adjecentFullNodes(room, worldMap); //find adjesent rooms
                    foreach (Room aj in adjecentNodes) { room.addDoors(aj); Console.WriteLine("here"); } //add doors for found
                }


            }

            /*private Room connectRooms(List<Room> rooms, int i)
            {
                List<Room>.Enumerator r = rooms.GetEnumerator();
                r.
                for (int j = i+1; j < rooms.Count; j++)
                {
                    if (isNeighbour(rooms[i], rooms[j]))
                    {
                        List<Room> rest =;
                        Room room = connectRooms(rooms, j);
                        if (room != null)
                            rooms[i].addDoors(room);
                    }


                }
                return null;
            }*/

            private bool isNeighbour(Room room1, Room room2)
            {
                return room1.getX() + 1 == room2.getX() || room1.getX() - 1 == room2.getX() ||
                    room1.getY() + 1 == room2.getY() || room1.getY() - 1 == room2.getY();
            }


            private void SetKeyRoom()
            {
                List<Room> deadEnds = new List<Room>();
                foreach (List<Room> row in worldMap)
                {
                    foreach (Room room in row)
                    {
                        if (room.isNodeEmpty()) { continue; }
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


        internal class Room : Node
        {
            private readonly Random rand = new Random();
            public static int roomWidth = 10;
            public static int roomHeight = 8;
            private bool doorUp = false;
            private bool doorDown = false;
            private bool doorLeft = false;
            private bool doorRight = false;

            private List<List<Tile>> roomLayout = new List<List<Tile>>();
            private bool isGenerated = false;
            private RoomType roomType = RoomType.Null;
            internal enum RoomType { Start, End, Key, Normal, Null }
            internal Room(int x, int y) : base(x, y) { }
            internal void setRoomType(RoomType roomType) { this.roomType = roomType; }
            internal RoomType getRoomType() { return roomType; }
            internal void addDoors(Room room)
            {
                if (this.getX() < room.getX()) { doorRight = true; room.doorLeft = true; } // this left from room
                if (this.getX() > room.getX()) { doorLeft = true; room.doorRight = true; } // this right from room
                if (this.getY() < room.getY()) { doorUp = true; room.doorDown = true; } // this down from room
                if (this.getY() > room.getY()) { doorDown = true; room.doorUp = true; } // this up from room

            }

            //internal override bool isNodeEmpty() { return !(doorUp || doorDown || doorLeft || doorRight); }
            internal bool isDeadEnd() { return (doorUp ^ doorDown ^ doorLeft ^ doorRight); }

            public List<List<Tile>> getRoomLayout()
            {
                if (isGenerated) return roomLayout;
                generateRoom();
                return roomLayout;
            }
            private void generateRoom()
            {
                MakeRoom();
                List<Tile> wallNodes = buildOuterWallsAndDoors();
                List<Tile> growthNodes = Generation.chooseRandomNodes(wallNodes, rand.Next(roomWidth / 2));
                foreach (Tile tile in growthNodes) { tile.makeFinite(rand.Next(roomHeight / 2)); }
                List<Tile> allFreeWalls = Generation.growLightning(growthNodes, roomLayout);
                foreach (Tile tile in allFreeWalls) 
                { 
                    tile.setImagePath(images.GetValueOrDefault(ImagePosition.FreeWall));
                    tile.setPhysicsEnabled();
                    tile.MyBody.addRectCollider(8, 8, 48, 48);
                    tile.addTag(Tags.Destroyable.ToString());
                }
                //generateRoom();
                addGroundAndTag();

                FixWallsNotMove();

            }
            private void MakeRoom()
            {
                Tile t;
                for (int x = 0; x < roomHeight; x++)
                {
                    roomLayout.Add(new List<Tile>()); //make new row
                    for (int y = 0; y < roomWidth; y++)
                    {
                        roomLayout[x].Add(new Tile(y,x)); //make one "connecting pillar" in that row
                    }
                }
            }

            private List<Tile> buildOuterWallsAndDoors()
            {
                List<Tile> borderNodes = new List<Tile>();
                Tile current;

                //  borderNodes.AddRange(roomLayout[0]); // left side
                //borderNodes.AddRange(roomLayout[rows-1]); // right side
                List<Tile> topWall = new List<Tile>();
                List<Tile> bottomWall = new List<Tile>();
                List<Tile> leftWall = new List<Tile>();
                List<Tile> rightWall = new List<Tile>();

                //corners
                current = roomLayout[0][0];
                current.setImagePath(images.GetValueOrDefault(ImagePosition.TopLeftCorner));
                current.setPhysicsEnabled();
                current.MyBody.addRectCollider(0, 0, 64, 32); //top
                current.MyBody.addRectCollider(0, 0, 32, 64); //left

                current = roomLayout[0][roomWidth - 1];
                current.setImagePath(images.GetValueOrDefault(ImagePosition.TopRightCorner));
                current.setPhysicsEnabled();
                current.MyBody.addRectCollider(0, 0, 64, 32); //top
                current.MyBody.addRectCollider(32, 0, 32, 64); //right

                current = roomLayout[roomHeight - 1][0];
                current.setImagePath(images.GetValueOrDefault(ImagePosition.BottomLeftCorner));
                current.setPhysicsEnabled();
                current.MyBody.addRectCollider(0, 32, 64, 32); //bottom
                current.MyBody.addRectCollider(0, 0, 32, 64); //left

                current = roomLayout[roomHeight - 1][roomWidth - 1];
                current.setImagePath(images.GetValueOrDefault(ImagePosition.BottomRightCorner));
                current.setPhysicsEnabled();
                current.MyBody.addRectCollider(0, 32, 64, 32); //bottom
                current.MyBody.addRectCollider(32, 0, 32, 64); //right

                // everything in between
                for (int i = 1; i < roomWidth - 1; i++)
                { // everything in between
                    topWall.Add(roomLayout[0][i]); // up
                    current = roomLayout[0][i];
                    current.setImagePath(images.GetValueOrDefault(ImagePosition.TopWall));
                    current.setPhysicsEnabled();
                    current.MyBody.addRectCollider(0, 0, 64, 32); //top

                    bottomWall.Add(roomLayout[roomHeight - 1][i]); // down
                    current = roomLayout[roomHeight - 1][i];
                    current.setImagePath(images.GetValueOrDefault(ImagePosition.BottomWall));
                    current.setPhysicsEnabled();
                    current.MyBody.addRectCollider(0, 32, 64, 32); //bottom
                }
                for (int i = 1; i < roomHeight - 1; i++)
                { // everything in between
                    leftWall.Add(roomLayout[i][0]); // left
                    current = roomLayout[i][0];
                    current.setImagePath(images.GetValueOrDefault(ImagePosition.LeftWall));
                    current.setPhysicsEnabled();
                    current.MyBody.addRectCollider(0, 0, 32, 64); //left

                    rightWall.Add(roomLayout[i][roomWidth - 1]); //right
                    current = roomLayout[i][roomWidth - 1];
                    current.setImagePath(images.GetValueOrDefault(ImagePosition.RightWall));
                    current.setPhysicsEnabled();
                    current.MyBody.addRectCollider(32, 0, 32, 64); //right

                }
                if (doorUp) { addDoor(topWall, images.GetValueOrDefault(ImagePosition.DoorUp)); } //door up
                if (doorDown) { addDoor(bottomWall, images.GetValueOrDefault(ImagePosition.DoorDown)); } //door down
                if (doorLeft) { addDoor(leftWall, images.GetValueOrDefault(ImagePosition.DoorLeft)); } //door left
                if (doorRight) { addDoor(rightWall, images.GetValueOrDefault(ImagePosition.DoorRight)); } //door right

                borderNodes.AddRange(topWall);
                borderNodes.AddRange(bottomWall);
                borderNodes.AddRange(leftWall);
                borderNodes.AddRange(rightWall);

                return borderNodes;
            }
            private void addDoor(List<Tile> wall, string imagePath)
            {
                int index = rand.Next(wall.Count());
                wall[index].setImagePath(imagePath);
                wall.RemoveAt(index);
            }

            private void addGroundAndTag()
            {
                foreach (List<Tile> row in roomLayout)
                {
                    foreach (Tile n in row)
                    {
                        if (n.isNodeEmpty())
                        {
                            n.setImagePath(images.GetValueOrDefault(ImagePosition.Ground));
                            n.addTag(Tags.Ground.ToString());
                        }
                        else //is wall or door
                        {
                   //         n.setPhysicsEnabled();
                   //         n.MyBody.addRectCollider(0, 40, 32, 32); // two first variables not working
                            string imagePath = n.getImagePath();
                            if(imagePath.Contains("door") || imagePath.Contains("Door")) 
                                { n.addTag(Tags.Door.ToString()); }
                            else { n.addTag(Tags.Wall.ToString()); }
                            
                        }
                    }
                }
            }
            private void FixWallsNotMove()
            {
                foreach (List<Tile> row in roomLayout)
                {
                    foreach (Tile tile in row)
                    {
                        if (tile.MyBody != null)
                        {
                            tile.MyBody.Kinematic = true;
                            tile.MyBody.Mass = 10;
                        }
                    }
                }
            }
            public override void update()
            {
                foreach (List<Tile> row in roomLayout)
                    foreach (Tile tile in row)
                    {
                        
                        tile.update();
                    }
            }

            internal enum ImagePosition
            {
                TopLeftCorner, TopWall, TopRightCorner, RightWall,
                BottomRightCorner, BottomWall, BottomLeftCorner, LeftWall,
                FreeWall, Ground, DoorUp, DoorRight, DoorDown, DoorLeft,
            }

            internal static Dictionary<ImagePosition, string> images = new Dictionary<ImagePosition, string>()
        { 
            //corners
            { ImagePosition.TopRightCorner, "TopRightWall.png" },
            {ImagePosition.BottomRightCorner, "BottomRightWall.png" },
            {ImagePosition.TopLeftCorner, "TopLeftWall.png" },
            {ImagePosition.BottomLeftCorner, "BottomLeftWall.png" },
            //walls
            {ImagePosition.TopWall, "TopWall.png" },
            {ImagePosition.BottomWall, "BottomWall.png" },
            {ImagePosition.LeftWall, "LeftWall.png" },
            {ImagePosition.RightWall, "RightWall.png" },
            {ImagePosition.FreeWall, "FreeWall.png" },
            //doors
            {ImagePosition.DoorDown, "DoorDown.png" },
            {ImagePosition.DoorLeft, "DoorLeft.png" },
            {ImagePosition.DoorUp, "DoorUp.png" },
            {ImagePosition.DoorRight, "DoorRight.png" },
            //ground
            {ImagePosition.Ground, "Ground.png" },
        };
        }
        internal enum Tags { Wall, Ground, Door, Destroyable }

    }
}
