﻿using SDL2;
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
        private string _imagePath { get; set; }

        private SpriteSheetAnimation animation;

        private bool _left, _right, _up, _down, _space, _lShift;
        private string _direction;

        public CharacterGoD() {
            
        }

        public override void initialize()
        {
            _name = "God";
            _maxHealth = 100;
            _health = 100;
            _firePower = 10;
            _armour = 0;
            _movementSpeed = 100;
            animation = new SpriteSheetAnimation(this, "Character.png", 64, 32, 1, 1);
            _direction = "left";
            //_inventory = new List<Item>();

            _imagePath = "God.png";
            this.Transform.X = 500.0f;
            this.Transform.Y = 600.0f;
            animation.changeSprite(0, 0);


            Bootstrap.getInput().addListener(this);

            setPhysicsEnabled();

            MyBody.addRectCollider();

            addTag("Player");
        }

        public void fireGun()
        {

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

        }

        public override void update()
        {
            float amount = (float)(_movementSpeed * Bootstrap.getDeltaTime());

            /*
             * Input handling
             */
            if (_left)
            {
                this.Transform.translate(-1 * amount, 0);
            }

            if (_right)
            {
                this.Transform.translate(1 * amount, 0);
            }

            if (_up)
            {
                this.Transform.translate(0, -1 * amount);
            }

            if (_down)
            {
                this.Transform.translate(0, 1 * amount);
            }

            animation.changeSprite(0, 0);
            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {

        }

        public void onCollisionExit(PhysicsBody x)
        {

            //MyBody.DebugColor = Color.Green;
        }

        public void onCollisionStay(PhysicsBody x)
        {
            //MyBody.DebugColor = Color.Blue;
        }
    }
}
