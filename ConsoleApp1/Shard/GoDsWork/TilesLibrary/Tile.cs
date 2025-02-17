﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Shard.GameOfDoom;

namespace Shard.Shard.GoD_s_Work.Tiles_Libary
{   
    public class Tile : Node
    {
        internal static float width = 10;
        //internal List<Tag> tags;
        internal Tag tag;

        internal Tile(Tag tag, int x, int y) : base(x, y) { this.tag = tag; }

        public Tag getTag() {  return tag; }
        public float getWidth() { return width; }
        public virtual void interaction() { }
        internal virtual void connectTile(Tile exit) { // I am assuming that override negates virtual method
            Console.Error.WriteLine("Custom Error in Tile/Tile: This Tile is non-connectable"); 
        }
    }
    public enum Tag
    { 
        ground,
        wall,
        trap,
        unconnectedDoor, door,
    }

}
