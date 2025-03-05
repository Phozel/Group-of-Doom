using Shard.Shard.GoDsWork.Animation;
using Shard.Shard.GoDsWork.NPCAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    internal class Enemy : GameObject, CollisionHandler
    {
        private int spriteToUse;
        private int rowtouse;
        private string[] sprites;
        private int xdir;
        private GameGOD game;
        private Random rand;
        SpriteSheetAnimation animation;
        private NPC npcBehavior;
        private GameObject player;
        private string _direction;
        private float animationTimer;
        private const float ANIMATION_SPEED = 0.1f;

        public int Xdir { get => xdir; set => xdir = value; }

        public override void initialize()
        {
            

            game = (GameGOD)Bootstrap.getRunningGame();
            player = game.GetPlayer();
            animation = new SpriteSheetAnimation(this, "ATTACK.png", 81, 71, 1, 8);
            
            


            animation.changeSprite(0, 0);


            this.Transform.X = 200.0f;
            this.Transform.Y = 100.0f;
            


            setPhysicsEnabled();
            MyBody.addRectCollider();

            rand = new Random();
            Vector2 startPos = new Vector2(this.Transform.X, this.Transform.Y);
            Vector2[] patrolPoints = { new Vector2(150, 100), new Vector2(250, 100) };
            npcBehavior = new NPC(startPos, patrolPoints, player);

            this.addTag("Enemy");

            MyBody.PassThrough = true;

        }
        public void changeSprite()
        {
            spriteToUse+=1;


            if (spriteToUse >= 4)
            {

                spriteToUse = 0;
            }


            animation.changeSprite(0, spriteToUse);
      

        }

        public override void update()
        {
            float deltaTime = (float)Bootstrap.getDeltaTime();
            npcBehavior.Update(deltaTime);


            //animation logic here
            animationTimer += deltaTime;

            if (animationTimer >= ANIMATION_SPEED)
            {
                changeSprite();
                animationTimer = 0f;
            }
            Console.WriteLine($"spriteToUse: {spriteToUse}, animationTimer: {animationTimer}");

            // Apply NPC movement to enemy position
            this.Transform.X = npcBehavior.Position.X;
            this.Transform.Y = npcBehavior.Position.Y;
            Bootstrap.getDisplay().addToDraw(this);
        }
        public void fire()
        {
            Bullet b = new Bullet();

            b.setUpBullet(this.Transform.Centre.X, this.Transform.Centre.Y,_direction);
           
        }
        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("Player"))
            {
                x.Parent.ToBeDestroyed = true;
            }
            else if (x.Parent.checkTag("Bullet"))
            {
                this.ToBeDestroyed = true;
            }
            
            if (x.Parent.checkTag("Bullet"))
            {
                Console.WriteLine("Bullet hit the enemy!");
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
