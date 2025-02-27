﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoDsWork.Animation;

namespace Shard.GameOfDoom
{
    internal class Bullet : GameObject, CollisionHandler
    {
        private string direction;
        int xDir = 0;
        int yDir = 0;

        SpriteSheetAnimation animation;

        public void setUpBullet(float x, float y, string dir)
        {
            this.Transform.X = x;
            this.Transform.Y = y;
            this.direction = dir;



            if (direction == "up")
            {
                xDir = 0;
                yDir = -1;
            }
            if (direction == "down")
            {
                xDir = 0;
                yDir = 1;
            }
            if (direction == "left")
            {
                xDir = -1;
                yDir = 0;
            }
            if (direction == "right")
            {
                xDir = 1;
                yDir = 0;
            }

            //setPhysicsEnabled();

            //MyBody.addRectCollider();

            animation = new SpriteSheetAnimation(this, "PlayerBullet1.png", 32, 32, 1, 1);
            animation.changeSprite(0, 0);

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

        }

        public void onCollisionExit(PhysicsBody x)
        {

        }

        public void onCollisionStay(PhysicsBody x)
        {

        }

    }
}
