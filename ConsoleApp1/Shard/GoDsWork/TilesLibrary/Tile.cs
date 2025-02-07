using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Shard.Shard.GoD_s_Work.Tiles_Libary
{
    public class TileFactory
    {
        private static TileFactory tileFactory;

        public static TileFactory getInstance()
        {
            if (tileFactory == null)
            {
                tileFactory = new TileFactory();
            }
            return tileFactory;
        }
        private TileFactory() { }
        //public static TileFactory getInstance(Tile tile) { return tileFactory; }
        //public static TileFactory getTileFactory() { return tileFactory; }

        public void setTileWidth(float width)
        {
            Tile.width = width;
        }
        public Tile getGroundTile() {  return BasicTile.groundTile(); }
        public Tile getWallTile() { return BasicTile.wallTile(); }
        public Tile getTrap() { return new TrapTile(); }
        public Tile getUnconnectedDoor() { return new DoorTile(); }
        public Tile getConnectedDoor(Tile exit) { return new DoorTile(exit); }
        public void connectDoor(Tile unconnectedDoor, Tile exit) { unconnectedDoor.connectTile(exit); }

    }
        
    public abstract class Tile
    {
        internal static float width = 10;
        //internal List<Tag> tags;
        internal Tag tag;

        internal Tile(Tag tag) { this.tag = tag; }

        public Tag getTag() {  return tag; }
        public float getWidth() { return width; }
        public virtual void interaction() { }
        internal virtual void connectTile(Tile exit) { // I am assuming that override negates virtual method
            Console.WriteLine("Custom Error in Tile/Tile: This Tile is non-connectable"); 
        }
    }
    public enum Tag
    { 
        ground,
        wall,
        trap,
        unconnectedDoor, door,
    }

    internal class BasicTile : Tile
    {
        internal static Tile groundTile() { return new BasicTile(Tag.ground); }
        internal static Tile wallTile() { return new BasicTile(Tag.wall); }
        private BasicTile(Tag tag) : base(tag) { }
    }
    internal class TrapTile : Tile
    {
        internal TrapTile() : base(Tag.trap) { }

        override public void interaction() { }
    }
    internal class DoorTile : Tile
    {
        private Tile exit;
        internal DoorTile() : base(Tag.unconnectedDoor) { }
        internal DoorTile(Tile exit) : base(Tag.door) { this.exit = exit; }

        /*internal void connectDoor(Tile unconnectedDoor, Tile exit)
        {
            this.exit = exit;
            unconnectedDoor.tag = Tag.door;
        }*/

        override public void interaction() { }
        override internal void connectTile(Tile exit) { this.exit = exit; this.tag = Tag.door; }
    }

}
