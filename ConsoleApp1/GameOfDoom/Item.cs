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

        public Item(string itemTag, string spriteName, int sizex, int sizey, int rows, int cols, int startrow, int startcol, int posx, int posy) : base()
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

            //removes from automatically update = doesn't render when leaving the room it resides in
            //causes problem of not being able to pick up key
            //    GameObjectManager.getInstance().removeGameObject(this); 

            initializeItem();
        }

        public void initializeItem()
        {
            setPhysicsEnabled();
            //this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(spriteName);
            SpriteSheetAnimation animation = new SpriteSheetAnimation(this, spriteName, sizex, sizey, rows, cols);
            animation.changeSprite(startrow, startcol);

            addTag("Item");
            addTag(itemTag);
            MyBody.addRectCollider(0, 0, sizex*cols, sizey*rows);
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
