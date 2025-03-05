using Shard.Shard.GoDsWork.Animation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    internal class Rocket : GameObject, CollisionHandler
    {
        private string direction;
        int xDir = 0;
        int yDir = 0;

        SpriteSheetAnimation animation;

        public void setUpRocket(float x, float y, string dir) 
        { 
            this.Transform.X = x;
            this.Transform.Y = y;
            this.direction = dir;

            animation = new SpriteSheetAnimation(this, "PlayerRocket-sheet.png", 32, 32, 1, 4);
            

            if (direction == "up")
            {
                xDir = 0;
                yDir = -1;
                animation.changeSprite(0, 2);
            }
            if (direction == "down")
            {
                xDir = 0;
                yDir = 1;
                animation.changeSprite(0, 3);
            }
            if (direction == "left")
            {
                xDir = -1;
                yDir = 0;
                animation.changeSprite(0, 0);
            }
            if (direction == "right")
            {
                xDir = 1;
                yDir = 0;
                animation.changeSprite(0, 1);
            }

            setPhysicsEnabled();

            MyBody.addRectCollider(8, 8, 64, 16);
            MyBody.PassThrough = true;

            

        }

        public override void initialize()
        {
            this.Transient = true;

        }

        public override void update()
        {
            
            
            this.Transform.translate(xDir * 400 * Bootstrap.getDeltaTime(), yDir * 400 * Bootstrap.getDeltaTime());

            
            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            if (!x.Parent.checkTag("God"))
            {
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
