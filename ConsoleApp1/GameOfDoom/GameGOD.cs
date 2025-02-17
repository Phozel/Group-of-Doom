using SDL2;
using Shard.GameOfDoom;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shard
{
    class GameGOD : Game, InputListener
    {
        


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

        }

        public void handleInput(InputEvent inp, string eventType)
        {


        }
    }
}
