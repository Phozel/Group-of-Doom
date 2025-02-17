using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoD_s_Work.Tiles_Libary;

namespace Shard.GameOfDoom
{
    public static class TileFactory
    {
        public static void setTileWidth(float width)
        {
            Tile.width = width;
        }
        public static Tile getGroundTile() { return BasicTile.groundTile(); }
        public static Tile getWallTile() { return BasicTile.wallTile(); }
        public static Tile getTrapTile() { return new TrapTile(); }
        //public Tile getUnconnectedDoor() { return new DoorTile(); }
        //public Tile getConnectedDoor(Tile exit) { return new DoorTile(exit); }
        //public void connectDoor(Tile unconnectedDoor, Tile exit) { unconnectedDoor.connectTile(exit); }
        public static Tuple<Tile, Tile> getDoorTilePair() {
            Tile door1 = new DoorTile(); //getUnconnectedDoor();
            Tile door2 = new DoorTile(door1); //getConnectedDoor(door1);
            door1.connectTile(door2); //connectDoor(door1, door2);
            Tuple<Tile, Tile> doors = new Tuple<Tile, Tile>(door1, door2);
            return doors;
        }
    }

    internal class BasicTile : Tile
    {
        internal static Tile groundTile() { return new BasicTile(Tag.ground); }
        internal static Tile wallTile() { return new BasicTile(Tag.wall); }
        private BasicTile(Tag tag) : base(tag, -1, -1) { }
    }
    internal class TrapTile : Tile
    {
        internal TrapTile() : base(Tag.trap, -1, -1) { }
        override public void interaction() { }
    }
    internal class DoorTile : Tile
    {
        private Tile exit;
        internal DoorTile() : base(Tag.unconnectedDoor, -1, -1) { }
        internal DoorTile(Tile exit) : base(Tag.door, -1, -1) { this.exit = exit; }

        /*internal void connectDoor(Tile unconnectedDoor, Tile exit)
        {
            this.exit = exit;
            unconnectedDoor.tag = Tag.door;
        }*/

        override public void interaction() { }
        override internal void connectTile(Tile exit) { this.exit = exit; this.tag = Tag.door; }
    }
}
