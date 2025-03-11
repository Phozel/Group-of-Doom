using System;
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
        public Bomb(int posx, int posy, bool collectible) : base("Bomb", collectible ? "Bomb.png" : "BombPlaced.png", collectible ? 64 : 32, collectible ? 64 : 32, 1, collectible ? 2 : 1, 0, 0, posx, posy, collectible)
        {
            if (!collectible)
            {
                // Explode!
            }
        }

        public void setIsPlaced(bool isPlaced)
        {
            _isPlaced = isPlaced;
        }

        public override void update()
        {
            if (Bootstrap.TimeElapsed - _placeTime >= 0.5 & _isPlaced)
            {
                _placeTime = Bootstrap.TimeElapsed;
                Console.WriteLine("Boom!");
                this.ToBeDestroyed = true;
            }
            base.update();
        }

    }
}
