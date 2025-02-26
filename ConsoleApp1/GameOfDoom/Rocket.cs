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
        private string dir;

        SpriteSheetAnimation animation;

        public void setUpRocket(float x, float y, string dir) 
        { 
            this.Transform.X = x;
            this.Transform.Y = y;
            this.dir = dir;
            animation = new SpriteSheetAnimation(this, "PlayerRocket1.png", 32, 32, 1, 1);
            animation.changeSprite(0,0);

        }

        public override void initialize()
        {
            this.Transient = true;
        }

        public override void update()
        {
            int xDir = 0;
            int yDir = 0;

            if (dir == "up") { 
                xDir = 0;
                yDir = -1;
            }
            if (dir == "down") {
                xDir = 0;
                yDir = 1;
            }
            if (dir == "left") {
                xDir = -1;
                yDir = 0;
            }
            if (dir == "right") {
                xDir = 1;
                yDir = 0;
            }
            
            this.Transform.translate(xDir * 400, yDir * 400 * Bootstrap.getDeltaTime());

            animation.changeSprite(0, 0);
            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            
        }

        public void onCollisionExit(PhysicsBody x)
        {
            
        }

        public void onCollisionStay(PhysicsBody x)
        {
        
        }
    }
}
