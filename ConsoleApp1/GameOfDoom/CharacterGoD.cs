using SDL2;
using Shard.Shard.GoDsWork.Animation;
using Shard.Shard.GoDsWork.ControllableGameObject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shard.GameOfDoom
{
    internal class CharacterGoD : ControllableGameObject
    {
        private string _name { get; set; }
        private float _maxHealth { get; set; }
        private float _health { get; set; }
        private int _firePower { get; set; }
        private float _armour { get; set; }
        //private List<Item> _inventory { get; set; }
        private float _movementSpeed { get; set; }
        private double _fireTime = 1;

        private SpriteSheetAnimation animation;

        private bool _left, _right, _up, _down, _space, _lShift;

        private float _posX, _posY;
        private string _direction;
        private bool _isCollidingWithEnvironment = false;
        private int armorLevel = 0;
        private int bombs = 10;

        public CharacterGoD(float fXstart, float fYstart) {
            this.Transform.X = fXstart;
            this.Transform.Y = fYstart;
        }

        public override void initialize()
        {
            _name = "God";
            _maxHealth = 100;
            _health = 100;
            _firePower = 0;
            _armour = 0;
            _movementSpeed = 100;
            animation = new SpriteSheetAnimation(this, "Character v3-Sheet.png", 64, 64, 4, 4);
            _direction = "left";
            //_inventory = new List<Item>();
            
            this.addTag("God");
            
            animation.changeSprite(0, 0);

            
            Bootstrap.getInput().addListener(this);

            setPhysicsEnabled();
            
            MyBody.addRectCollider(16, 16, 32, 32);
            addTag("Player");
        }
        public float Health
        {
            get { return _health; }
        }

        public void fireGun()
        {
            

            if (_firePower >= 10)
            {
                Rocket rocket = new Rocket();
                rocket.setUpRocket(this.Transform.Centre.X-16, this.Transform.Centre.Y-16, _direction);
                Bootstrap.getSound().playSound("fire.wav", 16);

            }
            else if (_firePower < 10)
            {
                Bullet bullet = new Bullet();
                bullet.setUpBullet(this.Transform.Centre.X -16, this.Transform.Centre.Y-16, _direction);
                Bootstrap.getSound().playSound("fire.wav", 16);

            }
        }

        public void placeBomb()
        {
            int bx = 0, by = 0;
            if (_direction == "left")
            {
                bx = (int)this.Transform.X - 24;
                by = (int)this.Transform.Y + 16;
            }
            if (_direction == "right")
            {
                bx = (int)this.Transform.X + 56;
                by = (int)this.Transform.Y + 16;
            }
            if (_direction == "up")
            {
                bx = (int)this.Transform.X + 16;
                by = (int)this.Transform.Y - 24;
            }
            if (_direction == "down")
            {
                bx = (int)this.Transform.X + 16;
                by = (int)this.Transform.Y + 56;
            }

            Bomb bomb = new Bomb(bx, by, false);
            bomb.setIsPlaced(true);
            Console.WriteLine("Bomb placed at " + bx + " " + by + "\n");
        }

        public override void handleInput(InputEvent inp, string eventType)
        {
            if (Bootstrap.getRunningGame().isRunning() == false)
            {
                return;
            }

            if (eventType == "KeyDown")
            {

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    _left = true;
                    _direction = "left";
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    _right = true;
                    _direction = "right";
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_W)
                {
                    _up = true;
                    _direction= "up";
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_S)
                {
                    _down = true;
                    _direction = "down";
                }

            }
            else if (eventType == "KeyUp")
            {

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    _left = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    _right = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_W)
                {
                    _up = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_S)
                {
                    _down = false;
                }

            }

            if (eventType == "KeyUp")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_SPACE)
                {
                    Console.WriteLine(Bootstrap.TimeElapsed);
                    if (Bootstrap.TimeElapsed - _fireTime >= 0.2) 
                    {
                        _fireTime = Bootstrap.TimeElapsed;
                        fireGun(); 
                    }
                    
                    
                }
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_LSHIFT)
                {
                    if (_firePower >= 10)
                    { 
                        _firePower = 0;
                    }
                    else
                    {
                        _firePower = 20;
                    }
                }
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_LALT)
                {
                    if (bombs > 0)
                    {
                        placeBomb();
                        bombs -= 1;
                    }
                }
            }
        }

        public override void update()
        {
            float amount = (float)(_movementSpeed * Bootstrap.getDeltaTime());

            /*
             * Input handling
             */
            if (!_isCollidingWithEnvironment)
            {
                
                if (_left)
                {
                    if (_direction == "left")
                    {
                        animation.changeSprite(armorLevel, 0);
                    }
                    this.Transform.translate(-1 * amount, 0);
                    //_direction = "left";
                }

                if (_right)
                {
                    if (_direction == "right")
                    {
                        animation.changeSprite(armorLevel, 1);
                    }
                    this.Transform.translate(1 * amount, 0);
                    //_direction = "right";
                }

                if (_up)
                {
                    if (_direction == "up")
                    {
                        animation.changeSprite(armorLevel, 2);
                    }
                    this.Transform.translate(0, -1 * amount);
                    //_direction = "up";
                }

                if (_down)
                {
                    if (_direction == "down")
                    {
                        animation.changeSprite(armorLevel, 3);
                    }
                    this.Transform.translate(0, 1 * amount);
                    //_direction = "down";
                }
            }
            
            Bootstrap.getDisplay().addToDraw(this);
        }

        public override void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("Wall") | (x.Parent.checkTag("Item") & !x.Parent.checkTag("Collectible")))
            {
                _isCollidingWithEnvironment = true;
                
            }

            if (x.Parent.checkTag("Armor"))
            {
                armorLevel += 1;
                Console.Write("New armor level: " + armorLevel + "\n");
            }

            if (x.Parent.checkTag("Bomb") & x.Parent.checkTag("Collectible"))
            {
                bombs += 1;
                Console.Write("You picked up a bomb!" + "\n");
                Console.Write("You have " + bombs + " bombs" + "\n");
            }

            if (x.Parent.checkTag("Enemy"))
            {
                float cX = x.Parent.Transform.Centre.X;
                float cY = x.Parent.Transform.Centre.Y;

                float dX = this.Transform.Centre.X - cX;
                float dY = this.Transform.Centre.Y - cY;

                if (Math.Abs(dX) > Math.Abs(dY))
                {
                    if (dX > 20) { dX = 20; }
                    if (dX < 20) { dX = -20; }
                    this.Transform.translate(dX, 0);
                }
                else
                {
                    if (dY > 20) { dY = 20; }
                    if (dY < 20) { dY = -20; }
                    this.Transform.translate(0, dY);
                }
            }

        }

        public override void onCollisionExit(PhysicsBody x)
        {
            _isCollidingWithEnvironment = false;
        }

        public override void onCollisionStay(PhysicsBody x)
        {
            if (x.Parent.checkTag("Wall"))
            {
                float cX = x.Parent.Transform.Centre.X;
                float cY = x.Parent.Transform.Centre.Y;

                float dX = this.Transform.Centre.X - cX;
                float dY = this.Transform.Centre.Y - cY;

                this.Transform.translate(dX, dY);
            }

            if (x.Parent.checkTag("Item") & !x.Parent.checkTag("Collectible"))
            {
                float cX = x.Parent.Transform.Centre.X;
                float cY = x.Parent.Transform.Centre.Y;

                float dX = this.Transform.Centre.X - cX;
                float dY = this.Transform.Centre.Y - cY;

                if (Math.Abs(dX) > Math.Abs(dY))
                {
                    if (dX > 4) { dX = 4; }
                    if (dX < 4) { dX = -4; }
                    this.Transform.translate(dX, 0);
                }
                else
                {
                    if (dY > 4) { dY = 4; }
                    if (dY < 4) { dY = -4; }
                    this.Transform.translate(0, dY);
                }
            }
        }

        public void changePos(float nx, float ny)
        {
            this._posX = nx;
            this._posY = ny;
            this.Transform.X = this._posX;
            this.Transform.Y = this._posY;
        }

        public void changeHealth(float health)
        {
            this._health = health;
        }

    }
}
