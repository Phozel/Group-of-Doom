using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoDsWork.Animation;

namespace Shard.GameOfDoom
{
    abstract class Item : GameObject, CollisionHandler
    {
        public string itemTag { get; private set; }
        public string spriteName { get; private set; }
        public int sizex { get; private set; }
        public int sizey { get; private set; }
        public int rows { get; private set; }
        public int cols { get; private set; }
        public int startrow { get; private set; }
        public int startcol { get; private set; }
        public bool collectible { get; private set; }
        public SpriteSheetAnimation animation { get; protected set; }

        public Item(string itemTag, string spriteName, int sizex, int sizey, int rows, int cols, int startrow, int startcol, int posx, int posy, bool collectible) : base()
        {
            this.itemTag = itemTag;
            this.spriteName = spriteName;
            this.sizex = sizex;
            this.sizey = sizey;
            this.rows = rows;
            this.cols = cols;
            this.startrow = startrow;
            this.startcol = startcol;
            this.Transform.X = posx;
            this.Transform.Y = posy;
            this.collectible = collectible;

            //removes from automatically update = doesn't render when leaving the room it resides in
            //causes problem of not being able to pick up key
            //    GameObjectManager.getInstance().removeGameObject(this); 

            initializeItem();
        }

        public virtual void initializeItem()
        {
            setPhysicsEnabled();
            animation = new SpriteSheetAnimation(this, spriteName, sizex, sizey, rows, cols);
            animation.changeSprite(startrow, startcol);

            addTag("Item");
            addTag(itemTag);
            if (collectible == true){
                addTag("Collectible");
            }
            MyBody.addRectCollider(0, 0, sizex, sizey);
            MyBody.PassThrough = true;

        }

        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
        }

        public virtual void onCollisionEnter(PhysicsBody x)
        {
            if (collectible & x.Parent.checkTag("God"))
            {
                this.ToBeDestroyed = true;
            }

        }

        public virtual void onCollisionExit(PhysicsBody x)
        {
        }

        public virtual void onCollisionStay(PhysicsBody x)
        {
        }


    }
}
