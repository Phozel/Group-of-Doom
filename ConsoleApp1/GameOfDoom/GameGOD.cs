using SDL2;
using Shard.GameOfDoom;
using System;
using System.Collections.Generic;
using System.Drawing;
using static Shard.GameOfDoom.World;

namespace Shard
{
    class GameGOD : Game, InputListener
    {

        CharacterGoD player;
        World world;

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
            
            Bootstrap.getSound().playMusic("examplemusic.wav", SDL.SDL_MIX_MAXVOLUME);
            (float, float) t = world.getAcceptibeSpawnPosition();
            player = new CharacterGoD(t.Item1, t.Item2);

        }

        public void handleInput(InputEvent inp, string eventType)
        {


        }
    }
}
