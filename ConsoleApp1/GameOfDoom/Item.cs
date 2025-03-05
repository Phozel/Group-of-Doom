using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    abstract class Item : GameObject, CollisionHandler
    {
        public string itemTag { get; private set; }
        public string spriteName { get; private set; }
        public int sizex { get; private set; }
        public int sizey { get; private set; }

        public Item(string itemTag, string spriteName, int sizex, int sizey, int posx, int posy) : base()
        {
            this.itemTag = itemTag;
            this.spriteName = spriteName;
            this.sizex = sizex;
            this.sizey = sizey;
            this.Transform.X = posx;
            this.Transform.Y = posy;

            initializeItem();
        }

        public void initializeItem()
        {
            setPhysicsEnabled();
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(spriteName);
            
            addTag(itemTag);
            MyBody.addRectCollider(0, 0, sizex, sizey);
            MyBody.PassThrough = true;

        }

        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("God"))
            {
                // add item to inventory
                this.ToBeDestroyed = true;
            }

        }

        public void onCollisionExit(PhysicsBody x)
        {
        }

        public void onCollisionStay(PhysicsBody x)
        {
        }


    }
}
