using SDL2;
using Shard.GameOfDoom;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shard
{
    class GameGOD : Game, InputListener
    {
        private HUD gameHUD;


        public override bool isRunning()
        {
            return true;
            
        }
        public override void update()
        {
            gameHUD.update();
        }

        public void draw()
        {
            gameHUD.draw();
        }

        public void createObjects()
        {

        }

        public override void initialize()
        {
            Bootstrap.getInput().addListener(this);
            gameHUD = new HUD();

        }

        public void handleInput(InputEvent inp, string eventType)
        {


        }
    }
}
