using Shard.Shard.GoDsWork.Animation;
using Shard.Shard.GoDsWork.NPCAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shard.GameOfDoom.World;
using static Shard.GameOfDoom.CharacterGoD;


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
        private int damage;
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


           
            


            setPhysicsEnabled();
            MyBody.addRectCollider(8, 3, 49, 65);

            rand = new Random();

            float offsetX = player.Transform.X + 60;
            float offsetY = player.Transform.Y + 60;

            float randomX = rand.Next(100, 700);
            float randomY = rand.Next(100, 500);

            this.Transform.X = offsetX + World.Room.roomWidth;
            this.Transform.Y = offsetY + World.Room.roomHeight;

            
            
            Vector2 startPos = new Vector2(this.Transform.X, this.Transform.Y);
            Vector2[] patrolPoints = { new Vector2(150, 100), new Vector2(250, 100) };
            npcBehavior = new NPC(startPos, patrolPoints, player);

            this.addTag("Enemy");

            MyBody.PassThrough = true;
            Console.WriteLine($"Enemy tags: {this.getTags()}");
            damage = 5;
            


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
           // Console.WriteLine($"spriteToUse: {spriteToUse}, animationTimer: {animationTimer}");

            // Apply NPC movement to enemy position
            this.Transform.X = npcBehavior.Position.X;
            this.Transform.Y = npcBehavior.Position.Y;
            Bootstrap.getDisplay().addToDraw(this);
        }
     
        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("Player"))
            {
                CharacterGoD player = x.Parent as CharacterGoD;




                if (player != null)
                {
                    player.changeHealth(player.Health - damage);
                    Console.WriteLine($"Player hit! Health is now: {player.Health}");

                }
                if (player.Health <=0)
                {
                    x.Parent.ToBeDestroyed = true;
                }



            }
            else if (x.Parent.checkTag("Bullet") || (x.Parent.checkTag("Rocket")))
            {
                Console.WriteLine("Enemy hit!");
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
