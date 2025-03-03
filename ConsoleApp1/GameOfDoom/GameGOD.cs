using SDL2;
using Shard.GameOfDoom;
using Shard.Shard.GoDsWork.NPCAI;
using System;
using System.Collections.Generic;
using System.Drawing;
using static Shard.GameOfDoom.World;

namespace Shard
{
    class GameGOD : Game, InputListener
    {
        private NPC enemy;
        private GameObject player;
        World world;

        public override bool isRunning()
        {
            return true;
            
        }
        public override void update()
        {
            world.update();
            //enemy.Update(deltaTime);
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
            
            Bootstrap.getSound().playMusic("examplemusic.wav", SDL.SDL_MIX_MAXVOLUME);

        }

        public void handleInput(InputEvent inp, string eventType)
        {


        }
    }
}
