using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoD_s_Work.Tiles_Libary;
using Shard.Shard.GoDsWork.TilesLibrary;
using static Shard.GameGOD;
using static Shard.GameOfDoom.World;

namespace Shard.GameOfDoom
{
    internal class World
    {
        private static World me;
        private GameGOD gameGod;
        internal List<List<Room>> worldMap;
        internal Room currentRoom;
        public double whenLastEnterDoor = 0;

        public static World getInstance()
        {
            if (me == null)
            {
                me = new World();
            }

            return me;
        }

        private World()
        {
            WorldMap wm = new WorldMap(3, 0, (5, 7));
            worldMap = wm.getMap();
            currentRoom = wm.GetStartRoom();
            currentRoom.getRoomLayout();

        }
        internal void addRefToGameGOD(GameGOD gameGod) { this.gameGod = gameGod; }
        internal void switchRoom(Direction leavingDirection) {
            if (worldMap != null && currentRoom != null)
            {
                gameGod.switchRoom(leavingDirection);
            }
        }


        internal void update()
        {
            currentRoom.update();
        }

        internal (float,float) getAcceptibeSpawnPosition()
        {
            List<Tile> groundTiles = currentRoom.getGroundTiles();
            Random rand = new Random();
            int index = rand.Next(groundTiles.Count());
            Tile startTile = groundTiles[index];
            return (startTile.Transform.X, startTile.Transform.Y);
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
                width = mapSize.Item2;
                height = mapSize.Item1;
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
                    foreach (Room aj in adjecentNodes) { room.addDoors(aj); } //add doors for found
                }


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
            internal List<Tile> getGroundTiles()
            {
                List<Tile> groundTiles = new List<Tile>();
                foreach (List<Tile> row in roomLayout)
                {
                    foreach(Tile tile in row)
                    {
                        if (tile.checkTag(Tags.Ground.ToString())) { groundTiles.Add(tile); }
                    }
                }
                return groundTiles;
            }
            internal void setRoomType(RoomType roomType) { this.roomType = roomType; }
            internal RoomType getRoomType() { return roomType; }
            internal void addDoors(Room room)
            {
                if (this.getX() < room.getX()) { doorRight = true; room.doorLeft = true; } // this left from room
                if (this.getX() > room.getX()) { doorLeft = true; room.doorRight = true; } // this right from room
                if (this.getY() < room.getY()) { doorDown = true; room.doorUp = true; } // this down from room
                if (this.getY() > room.getY()) { doorUp = true; room.doorDown = true; } // this up from room

            }

            //internal override bool isNodeEmpty() { return !(doorUp || doorDown || doorLeft || doorRight); }
            internal bool isDeadEnd() { return (doorUp ^ doorDown ^ doorLeft ^ doorRight); }

            public void removeHitboxes() { TileHitboxesHandler.ClearHitboxes(this.roomLayout); }
            public List<List<Tile>> getRoomLayout()
            {
                if (isGenerated) { TileHitboxesHandler.addHitboxes(this.roomLayout); }
                else { generateRoom(); }
                return roomLayout;
            }
            private void generateRoom()
            {
                MakeRoom();
                List<Tile> wallNodes = buildOuterWallsAndDoors();
                List<Tile> growthNodes = Generation.chooseRandomNodes(wallNodes, rand.Next(roomWidth));
                foreach (Tile tile in growthNodes) { tile.makeFinite(rand.Next(roomHeight/2)); }
                List<Tile> allFreeWalls = Generation.growLightning(growthNodes, roomLayout);
         //       allFreeWalls.AddRange(growthNodes);
                foreach (Tile tile in allFreeWalls) 
                { 
                    tile.setImagePath(images.GetValueOrDefault(ImagePosition.FreeWall));
                    
                    tile.addTag(Tags.Destroyable.ToString());
         //           tile.MyBody.addRectCollider(8, 8, 48, 48);
                }
                addGroundAndTag();

                if (roomType == RoomType.Key) addKeyToRoom();

                TileHitboxesHandler.createHitboxes(this.roomLayout);

                isGenerated = true;
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

                current = roomLayout[0][roomWidth - 1];
                current.setImagePath(images.GetValueOrDefault(ImagePosition.TopRightCorner));

                current = roomLayout[roomHeight - 1][0];
                current.setImagePath(images.GetValueOrDefault(ImagePosition.BottomLeftCorner));

                current = roomLayout[roomHeight - 1][roomWidth - 1];
                current.setImagePath(images.GetValueOrDefault(ImagePosition.BottomRightCorner));

                // everything in between
                for (int i = 1; i < roomWidth - 1; i++)
                { // everything in between
                    topWall.Add(roomLayout[0][i]); // up
                    current = roomLayout[0][i];
                    current.setImagePath(images.GetValueOrDefault(ImagePosition.TopWall));

                    bottomWall.Add(roomLayout[roomHeight - 1][i]); // down
                    current = roomLayout[roomHeight - 1][i];
                    current.setImagePath(images.GetValueOrDefault(ImagePosition.BottomWall));
                }
                for (int i = 1; i < roomHeight - 1; i++)
                { // everything in between
                    leftWall.Add(roomLayout[i][0]); // left
                    current = roomLayout[i][0];
                    current.setImagePath(images.GetValueOrDefault(ImagePosition.LeftWall));

                    rightWall.Add(roomLayout[i][roomWidth - 1]); //right
                    current = roomLayout[i][roomWidth - 1];
                    current.setImagePath(images.GetValueOrDefault(ImagePosition.RightWall));

                }
                if (doorUp) { addDoor(topWall, images.GetValueOrDefault(ImagePosition.DoorUp), GameGOD.Direction.Up); } //door up
                if (doorDown) { addDoor(bottomWall, images.GetValueOrDefault(ImagePosition.DoorDown), GameGOD.Direction.Down); } //door down
                if (doorLeft) { addDoor(leftWall, images.GetValueOrDefault(ImagePosition.DoorLeft), GameGOD.Direction.Left); } //door left
                if (doorRight) { addDoor(rightWall, images.GetValueOrDefault(ImagePosition.DoorRight), GameGOD.Direction.Right); } //door right

                borderNodes.AddRange(topWall);
                borderNodes.AddRange(bottomWall);
                borderNodes.AddRange(leftWall);
                borderNodes.AddRange(rightWall);

                return borderNodes;
            }
            private void addDoor(List<Tile> wall, string imagePath, GameGOD.Direction dir)
            {
                int index = rand.Next(wall.Count());
                wall[index].setImagePath(imagePath);
                wall[index].addTag(dir.ToString());
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
                            string imagePath = n.getImagePath();
                            if(imagePath.Contains("door") || imagePath.Contains("Door")) 
                                { n.addTag(Tags.Door.ToString()); }
                            else { n.addTag(Tags.Wall.ToString()); }
                            
                        }
                    }
                }
            }
            private void addKeyToRoom()
            {
                List<Tile> groundTiles = getGroundTiles();
                Tile keyTile = groundTiles[rand.Next(groundTiles.Count)];
                keyTile.item = new Key((int)keyTile.Transform.X, (int)keyTile.Transform.Y);
                roomLayout[keyTile.getY()][keyTile.getX()] = keyTile;
            }
            public override void update()
            {
                foreach (List<Tile> row in roomLayout)
                    foreach (Tile tile in row)
                    {
                        
                        tile.update();
                        if (tile.item != null) tile.item.update();
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
