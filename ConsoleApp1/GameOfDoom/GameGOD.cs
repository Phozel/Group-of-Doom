using SDL2;
using Shard.GameOfDoom;
using Shard.Shard.GoDsWork.HUD;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using static Shard.GameOfDoom.World;

namespace Shard
{
    class GameGOD : Game, InputListener
    {

        CharacterGoD player;
        World world;
        private HudManager hudManager;

        public override bool isRunning()
        {
            return true;
            
        }
        public override void update()
        {
            world.update();
        }

        public void draw()
        {
           
        }

        public void createObjects()
        {

        }

        public override void initialize()
        {
            Bootstrap.getInput().addListener(this);

            world = new World();

            hudManager = new HudManager();
            AmmoCount ammoHUD = new AmmoCount(10, 30);
            ammoHUD.Position = new Vector2(300, 20);
            hudManager.AddElement(ammoHUD);
            
            Bootstrap.getSound().playMusic("examplemusic.wav", SDL.SDL_MIX_MAXVOLUME);
            player = new CharacterGoD();
            player.SetAmmoCounter(ammoHUD);

        }

        public void handleInput(InputEvent inp, string eventType)
        {


        }
    }
}
