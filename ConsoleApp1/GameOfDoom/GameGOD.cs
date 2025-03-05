using SDL2;
using Shard.GameOfDoom;
using Shard.Shard.GoD_s_Work.Tiles_Libary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

            //world = World.getInstance();
            //world = new World();

            Key key = new Key(300, 200);
            //Weapon gun = new Weapon(64, 128);
            Armor armor = new Armor(64, 192);
            HealthPack helth = new HealthPack(64, 256);
            Bootstrap.getSound().playMusic("examplemusic.wav", SDL.SDL_MIX_MAXVOLUME);

            

            (float, float) t = world.getAcceptibeSpawnPosition();
            player = new CharacterGoD(t.Item1, t.Item2);
            enemy = new Enemy();


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
                if (layout[i][layout.Count() - 1].checkTag(Tags.Door.ToString()))
                {
                    break;
                }
            }
            Tile t = layout[i][layout.Count() - 2];
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
                if (layout[0][i].checkTag(Tags.Door.ToString()))
                {
                    break;
                }
            }
            Tile t = layout[1][i];
            return (t.Transform.X, t.Transform.Y);
        }
        private (float, float) switchRoomDown()
        {
            world.currentRoom = world.worldMap[world.currentRoom.getY() + 1][world.currentRoom.getX()];
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
