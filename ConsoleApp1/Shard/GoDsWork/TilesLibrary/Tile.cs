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

namespace Shard.Shard.GoD_s_Work.Tiles_Libary
{
    public class Tile : Node, CollisionHandler
    {
        internal static float width = 64;
        private string imagePath;

        internal Tile(int x, int y) : base(x, y) 
        { 
            this.Transform.X = x * width; // where to draw
            this.Transform.Y = y * width; // where to draw
        }

        internal void setImagePath(string imagePath)
        {
            this.imagePath = imagePath;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(imagePath);
        }
        internal string getImagePath() {  return this.imagePath; }
        internal override bool isNodeEmpty() { return imagePath == null; }
        public override void initialize() { }
        public override void update() { Bootstrap.getDisplay().addToDraw(this); }




        void CollisionHandler.onCollisionEnter(PhysicsBody x)
        {
            if (this.checkTag(World.Tags.Destroyable.ToString())) //destroy "self" on collision with missile
            {
                if (x.Parent.checkTag("Rocket")) 
                {
                    this.MyBody.getColliders().Clear();
                    this.MyBody = null;
                    this.clearTags();
                    this.setImagePath(World.Room.images.GetValueOrDefault(ImagePosition.Ground));
                    this.addTag(Tags.Ground.ToString());
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
