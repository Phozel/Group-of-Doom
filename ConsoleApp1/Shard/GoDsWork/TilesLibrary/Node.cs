﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoD_s_Work.Tiles_Libary;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shard.Shard.GoDsWork.TilesLibrary
{
    public abstract class Node : GameObject
    {

        private readonly int positionX;
        private readonly int positionY;
        private bool finite = false;
        private int iterations = 0;
        private bool empty = true;

        internal Node(int x, int y)
        {
            positionX = x;
            positionY = y;
            GameObjectManager.getInstance().removeGameObject(this);


            // Console.WriteLine("Constructor Node: Random Image Path becuase this is a GameObject");
            //  this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("TopWall.png");
        }

        internal int getX() { return positionX; }
        internal int getY() { return positionY; }
        internal int getIteration() { return iterations; }
        internal bool isFinite() { return finite; }
        internal bool isDead() { return finite && iterations < 1; }

        internal void lowerIteration() { iterations--; }
        internal void makeFinite(int iterations)
        {
            finite = true;
            this.iterations = iterations;
        }

        //added information for when subclass nodes are counted as empty by the Generation functions 
        internal virtual bool isNodeEmpty() { return empty; }
        internal virtual void setNotEmpty() { empty = false; }
    }

}
