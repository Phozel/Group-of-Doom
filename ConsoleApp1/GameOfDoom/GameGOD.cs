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
        Enemy enemy;
        CharacterGoD player;
        World world;
        private bool gameOver = false;
        private Random rand;

        public override bool isRunning()
        {
            if (player == null || player.ToBeDestroyed == true)
            {
                return false;
            }
            return true;
            
        }
        public override void update()
        {
            world.update();

            if (isRunning() == false)
            {
                rand = new Random();
                Color col = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
                Bootstrap.getDisplay().showText("GAME OVER!", 300, 300, 128, col, null);
                if (!gameOver)
                {
                    Bootstrap.getSound().stopMusic();
                    Bootstrap.getSound().playSound("pajas.wav", SDL.SDL_MIX_MAXVOLUME);
                    Console.WriteLine("Game over");
                    gameOver = true;
                }
                return;

            }
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
            enemy = new Enemy();

        }

        public GameObject GetPlayer()
        {
            return player;
        }

        public void handleInput(InputEvent inp, string eventType)
        {


        }
    }
}
