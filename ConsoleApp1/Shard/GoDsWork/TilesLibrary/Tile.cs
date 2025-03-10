using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Shard.GameOfDoom;
using Shard.Shard.GoDsWork.TilesLibrary;
using static Shard.GameOfDoom.World.Room;
using static Shard.GameOfDoom.World;
using static System.Net.Mime.MediaTypeNames;
using System.Timers;

namespace Shard.Shard.GoD_s_Work.Tiles_Libary
{
    public class Tile : Node, CollisionHandler
    {
        private readonly static int offsetFromCornerX = 10, offsetFromCornerY = 50;
        internal static float width = 64;
        private string imagePath;
        internal Item item { get; set; }

        internal Tile(int x, int y) : base(x, y) 
        { 
            this.Transform.X = x * width + offsetFromCornerX; // where to draw
            this.Transform.Y = y * width + offsetFromCornerY; // where to draw
        }

        internal void setImagePath(string imagePath)
        {
            this.imagePath = imagePath;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(imagePath);
        }
        internal string getImagePath() {  return this.imagePath; }
        internal override bool isNodeEmpty() { return imagePath == null; }
        public override void initialize() { }
        public override void update() {
            //if (item != null) item.update();
            Bootstrap.getDisplay().addToDraw(this); 
            
        }




        void CollisionHandler.onCollisionEnter(PhysicsBody x)
        {
            if (this.checkTag(World.Tags.Destroyable.ToString())) //"destroy" self on collision with missile
            {
                if (x.Parent.checkTag("Rocket")) 
                {
                    this.MyBody.clearColliders();
                  //  this.MyBody = null;
                   // this.clearTags();
                    this.setImagePath(World.Room.images.GetValueOrDefault(ImagePosition.Ground));
                    this.addTag(Tags.Ground.ToString());
                }
            }

            if (this.checkTag(World.Tags.Door.ToString()) && x.Parent.checkTag("God")) 
            {
                double EnterDoor = Bootstrap.TimeElapsed;
                if (EnterDoor - World.getInstance().whenLastEnterDoor > 0.5)
                {
                    World.getInstance().whenLastEnterDoor = EnterDoor;
                    World.getInstance().currentRoom.removeHitboxes(); //remove current room's hitboxes from play area

                    if (this.checkTag(GameGOD.Direction.Left.ToString()))
                    {
                       World.getInstance().switchRoom(GameGOD.Direction.Left); 
                    }
                    if (this.checkTag(GameGOD.Direction.Right.ToString()))
                    {
                        World.getInstance().switchRoom(GameGOD.Direction.Right);
                    }
                    if (this.checkTag(GameGOD.Direction.Up.ToString()))
                    {
                        World.getInstance().switchRoom(GameGOD.Direction.Up);
                    }
                    if (this.checkTag(GameGOD.Direction.Down.ToString()))
                    {
                        World.getInstance().switchRoom(GameGOD.Direction.Down);
                    }
                }
            }


      //      throw new NotImplementedException();
        }

        void CollisionHandler.onCollisionExit(PhysicsBody x)
        {
       //     throw new NotImplementedException();
        }

        void CollisionHandler.onCollisionStay(PhysicsBody x)
        {
     //       throw new NotImplementedException();
        }
    }

}
