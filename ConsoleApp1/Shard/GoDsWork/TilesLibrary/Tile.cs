using System;
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

        internal enum ImagePosition
        {
            TopLeftCorner, TopWall, TopRightCorner, RightWall,
            BottomRightCorner, BottomWall, BottomLeftCorner, LeftWall,
            FreeWall, Ground, DoorUp, DoorRight, DoorDown, DoorLeft,
        }

        private static Dictionary<ImagePosition, string> images = new Dictionary<ImagePosition, string>()
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



        internal static float width = 64;
        //internal List<Tag> tags;
        internal Tag tag;
        private string imagePath;

        internal Tile(Tag tag, int x, int y) : base(x, y) 
        { 
            this.tag = tag;
            this.Transform.X = x * width; // where
            this.Transform.Y = y * width; // where
                                               //  this.imagePath = images.GetValueOrDefault(ImagePosition.Ground);
        }

        internal void setTag(ImagePosition tag)
        {
            this.imagePath = images.GetValueOrDefault(tag);
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(imagePath);
        }
        public Tag getTag() {  return tag; }
        //public float getWidth() { return width; }
        public virtual void interaction() { }
        internal virtual void connectTile(Tile exit) { // I am assuming that override negates virtual method
            Console.Error.WriteLine("Custom Error in Tile/Tile: This Tile is non-connectable"); 
        }


        public override void initialize()
        {
            this.imagePath = images.GetValueOrDefault(ImagePosition.Ground);
   //         Console.WriteLine("getx: " + getX());
            //this.Transform.X = getX() * width; // where
            //this.Transform.Y = getY() * width; // where
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(imagePath);



           // MyBody.addRectCollider();

           // addTag("Player");


        }
        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
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
