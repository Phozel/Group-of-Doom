using SDL2;
using Shard.GameOfDoom;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shard
{
    class GameGOD : Game, InputListener
    {

        CharacterGoD player;

        public override bool isRunning()
        {
            return true;
            
        }
        public override void update()
        {

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

            WorldMap wm = new WorldMap(4, 0, (8, 6));
            List<List<Room>> map = wm.getMap();
            Room startRoom = wm.GetStartRoom();
            // map[startRoom.getY()][startRoom.getX()].getRoomLayout();
            startRoom.getRoomLayout();
            Bootstrap.getSound().playMusic("examplemusic.wav", SDL.SDL_MIX_MAXVOLUME);
            player = new CharacterGoD();

        }

        public void handleInput(InputEvent inp, string eventType)
        {


        }
    }
}
