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
        double lastSpawnTime = 0;
        int spawnCount;

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

            if (Bootstrap.TimeElapsed - lastSpawnTime >= 5)
            {
                lastSpawnTime = Bootstrap.TimeElapsed;
                Enemy enemy = new Enemy();
                enemy.Transform.Centre.X = new Random().Next(50, 700);
                enemy.Transform.Centre.Y = new Random().Next(100, 500);

                spawnCount++;
                Console.WriteLine($"Spawn count: {spawnCount}");
            }



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

            Random rand = new Random();
            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < 10;  i++)
            {
                float randomX = rand.Next(100, 600);
                float randomY = rand.Next(100, 500);

                Enemy enemy = new Enemy();
                enemy.Transform.Centre.X = player.Transform.Centre.X + randomX;
                enemy.Transform.Centre.Y = player.Transform.Centre.Y + randomY;

                enemies.Add(enemy);

                Console.WriteLine($"Spawned Enemy {i + 1} at ({randomX}, {randomY})");
                if (enemy.Transform.X < 0 || enemy.Transform.X > Bootstrap.getDisplay().getWidth() || enemy.Transform.Y < 0 || enemy.Transform.Y > Bootstrap.getDisplay().getHeight())
                {
                    enemy.ToBeDestroyed = true;
                }
            }

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
