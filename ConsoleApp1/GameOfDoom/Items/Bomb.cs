﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard.Shard.GoDsWork.Animation;

namespace Shard.GameOfDoom
{
    class Bomb : Item
    {
        private double _placeTime = Bootstrap.TimeElapsed;
        private bool _isPlaced = false;
        private bool _shouldExplode = false;
        public Bomb(int posx, int posy, bool collectible) : base("Bomb", collectible ? "Bomb.png" : "BombPlaced.png", collectible ? 64 : 32, collectible ? 64 : 32, 1, collectible ? 2 : 1, 0, 0, posx, posy, collectible)
        {
            _placeTime = Bootstrap.TimeElapsed;
            this.addTag("Bomb");
        }

        public void setIsPlaced(bool isPlaced)
        {
            this.removeTag("Collectible");
            _isPlaced = isPlaced;
        }

        public bool getShouldExplode()
        {
            return _shouldExplode;
        }

        public override void update()
        {
            if (Bootstrap.TimeElapsed - _placeTime >= 1 & _isPlaced)
            {
                _placeTime = Bootstrap.TimeElapsed;
                _shouldExplode = true;
                this.ToBeDestroyed = true;
            }
            base.update();
        }

        public override void onCollisionExit(PhysicsBody x)
        {
            
        }
    }
}
