using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoDsWork.Animation;

namespace Shard.GameOfDoom
{
    class EnemyDeath : GameObject
    {
        SpriteSheetAnimation animation;
        int spriteCounter = 1;
        private float animationTimer;
        private const float ANIMATION_SPEED = 0.1f;
        private bool _isDead = false;

        public EnemyDeath(float posx, float posy)
        {
            this.Transform.X = posx;
            this.Transform.Y = posy;
        }

        public override void initialize()
        {
            animation = new SpriteSheetAnimation(this, "DEATH.png", 81, 71, 1, 7);
            animation.changeSprite(0, 0);
            base.initialize();
        }

        public override void update()
        {
            float deltaTime = (float)Bootstrap.getDeltaTime();
            animationTimer += deltaTime;

            if (animationTimer >= ANIMATION_SPEED)
            {
                if (spriteCounter < 7)
                {
                    animation.changeSprite(0, spriteCounter);
                    spriteCounter += 1;
                    animationTimer = 0f;
                }
                else
                {
                    _isDead = true;
                }
                
            }

            Bootstrap.getDisplay().addToDraw(this);
        }

        public bool getIsDead()
        {
            return _isDead;
        }
    }
}
