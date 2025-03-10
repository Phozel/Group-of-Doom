using SDL2;
using Shard.GameOfDoom;
using Shard.Shard.GoD_s_Work.Tiles_Libary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static Shard.GameOfDoom.World;
using Shard.Shard.GoDsWork.HUD;


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
        public HudManager hudManager;
        public HealthBar healthBar;
        public ScoreCount scoreCount;

        public CharacterGoD Player {  get; private set; }

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
            float deltaTime = 0.016f;
            world.update();

            if (Bootstrap.TimeElapsed - lastSpawnTime >= 5)
            {
                lastSpawnTime = Bootstrap.TimeElapsed;
                Enemy enemy = new Enemy();
                float offsetX = player.Transform.X + 40;
                float offsetY = player.Transform.Y + 40;


                enemy.Transform.Centre.X = new Random().Next(50, 700) + offsetX;
                enemy.Transform.Centre.Y = new Random().Next(100, 500) + offsetY;

                spawnCount++;
                Console.WriteLine($"Spawn count: {spawnCount}");
                Console.WriteLine($"Position: {enemy.Transform.Centre.X} , {enemy.Transform.Centre.Y}");
            }



            if (isRunning() == false)
            {
                Bootstrap.getDisplay().clearDisplay();
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
            hudManager.Update(deltaTime);
            hudManager.Draw();
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

            world = World.getInstance();
            world.addRefToGameGOD(this);
            

            //Key key = new Key(300, 200);
            //Weapon gun = new Weapon(64, 128);
            Armor armor1 = new Armor(64, 192, 0);
            //Armor armor2 = new Armor(128, 192, 1);
            //Armor armor3 = new Armor(192, 192, 2);
            //HealthPack helth = new HealthPack(64, 256);

            Bootstrap.getSound().playMusic("examplemusic.wav", SDL.SDL_MIX_MAXVOLUME);

           

            (float, float) t = world.getAcceptibeSpawnPosition();
            player = new CharacterGoD(t.Item1, t.Item2, hudManager);

            hudManager = new HudManager();

            HealthBar healthBar = new HealthBar(this.Player);
            scoreCount = new ScoreCount();

            hudManager.AddElement(healthBar);
            hudManager.AddElement(scoreCount);

            Random rand = new Random();
            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < 2;  i++)
            {
                float randomX = rand.Next(0, World.Room.roomWidth);
                float randomY = rand.Next(0, World.Room.roomHeight);
                


                Enemy enemy = new Enemy();
                enemy.Transform.Centre.X = player.Transform.Centre.X + randomX;
                enemy.Transform.Centre.Y = player.Transform.Centre.Y + randomY;

                enemies.Add(enemy);

                Console.WriteLine($"Spawned Enemy {i + 1} at ({enemy.Transform.Centre.X}, {enemy.Transform.Centre.Y})");
                if (enemy.Transform.X < 0 || enemy.Transform.X > Bootstrap.getDisplay().getWidth() || enemy.Transform.Y < 0 || enemy.Transform.Y > Bootstrap.getDisplay().getHeight())
                {
                    enemy.ToBeDestroyed = true;
                }
            }


            Debug();
        }

        public GameObject GetPlayer()
        {
            return player;
        }

        public void handleInput(InputEvent inp, string eventType)
        {


        }

        internal enum Direction { Left, Right, Up, Down }
        internal void switchRoom(Direction leavingDirection)
        {
            (float, float) PlayerPos = (0, 0);
            switch (leavingDirection)
            {
                case Direction.Left: PlayerPos = switchRoomLeft(); break;
                case Direction.Right: PlayerPos = switchRoomRight(); break;
                case Direction.Up: PlayerPos = switchRoomUp(); break;
                case Direction.Down: PlayerPos = switchRoomDown(); break;
                default: Console.WriteLine("Error in World: How did you get here, this is not a valid direction"); break;
            }
            Enemy enemy = new Enemy();
            enemy.initialize();
            player.changePos(PlayerPos.Item1, PlayerPos.Item2);
            Debug(); //
        }
        private (float, float) switchRoomLeft()
        {

            world.currentRoom = world.worldMap[world.currentRoom.getY()][world.currentRoom.getX() - 1];
            List<List<Tile>> layout = world.currentRoom.getRoomLayout();
            int i;
            for (i = 1; i < layout.Count - 1; i++)
            {
                if (layout[i][layout[0].Count() - 1].checkTag(Tags.Door.ToString()))
                {
                    break;
                }
                
            }
            Tile t = layout[i][layout[0].Count() - 2];
            return (t.Transform.X, t.Transform.Y);

        }
        private (float, float) switchRoomRight()
        {
            world.currentRoom = world.worldMap[world.currentRoom.getY()][world.currentRoom.getX() + 1];
            List<List<Tile>> layout = world.currentRoom.getRoomLayout();
            int i;
            for (i = 1; i < layout.Count - 1; i++)
            {
                if (layout[i][0].checkTag(Tags.Door.ToString()))
                {
                    break;
                }
            }
            Tile t = layout[i][1];
            return (t.Transform.X, t.Transform.Y);

        }
        private (float, float) switchRoomUp()
        {
            world.currentRoom = world.worldMap[world.currentRoom.getY() - 1][world.currentRoom.getX()];
            List<List<Tile>> layout = world.currentRoom.getRoomLayout();
            int i;
            for (i = 1; i < layout[0].Count - 1; i++)
            {
                if (layout[layout.Count() - 1][i].checkTag(Tags.Door.ToString()))
                {
                    break;
                }
            }
            Tile t = layout[layout.Count() - 2][i];
            return (t.Transform.X, t.Transform.Y);
        }
        private (float, float) switchRoomDown()
        {
            world.currentRoom = world.worldMap[world.currentRoom.getY() + 1][world.currentRoom.getX()];
            List<List<Tile>> layout = world.currentRoom.getRoomLayout();
            int i;
            for (i = 1; i < layout[0].Count - 1; i++)
            {
                if (layout[0][i].checkTag(Tags.Door.ToString()))
                {
                    break;
                }
            }
            Tile t = layout[1][i];
            return (t.Transform.X, t.Transform.Y);
        }

        private void Debug()
        {
            List<List<Room>> layout = world.worldMap;
            string s = "";
            foreach (List<Room> row in layout)
            {
                Console.WriteLine("\n");
                foreach (Room room in row)
                {
                    if (room == world.currentRoom) s = s + " *";
                    else if (!room.isNodeEmpty()) s = s + " +";
                    else s = s + " #";
                }
                Console.WriteLine(s);
                s = "";
            }
            Console.WriteLine();
        }
    }
}
